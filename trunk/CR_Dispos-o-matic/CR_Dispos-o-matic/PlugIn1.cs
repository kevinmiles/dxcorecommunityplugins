using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_Dispos_o_matic
{
  public partial class PlugIn1 : StandardPlugIn
  {
    private const string STR_Dispose = "Dispose";
    private TypeDeclaration _ActiveClass;
    private string _CodeForNewIfDisposingBlock;
    private SourceRange _OldIfDisposingBlockRange;
    private string _DisposableImplementationCode;
    private TextDocument _TextDocument;
    private SourcePoint _InsertionPoint;

    // DXCore-generated code...
    #region InitializePlugIn
    public override void InitializePlugIn()
    {
      base.InitializePlugIn();
      // Tie the code provider to the code issue:
      cpImplementIDisposable.CodeIssueMessage = ipClassShouldImplementIDisposable.DisplayName;
      cpDisposeFields.CodeIssueMessage = ipFieldsShouldBeDisposed.DisplayName;
    }
    #endregion
    #region FinalizePlugIn
    public override void FinalizePlugIn()
    {
      //
      // TODO: Add your finalization code here.
      //
      base.FinalizePlugIn();
    }
    #endregion

    private static PrimitiveExpression GetBooleanLiteral(bool booleanValue)
    {
      PrimitiveExpression primitive = new PrimitiveExpression("");
      primitive.PrimitiveType = PrimitiveType.Boolean;
      primitive.PrimitiveValue = booleanValue;
      return primitive;
    }
    #region AddDisposeImplementer
    private static void AddDisposeImplementer(ElementBuilder elementBuilder)
    {
      Method disposeMethod = elementBuilder.AddMethod(null, null, STR_Dispose);
      // If implicit interface implementation is supported by the language?
      disposeMethod.Visibility = MemberVisibility.Public;
      Expression newCollection = new ElementReferenceExpression("IDisposable.Dispose");
      disposeMethod.AddImplementsExpression(newCollection);
      PrimitiveExpression booleanTrue = GetBooleanLiteral(true);
      ExpressionCollection argumentsCollection = new ExpressionCollection();
      argumentsCollection.Add(booleanTrue);
      elementBuilder.AddMethodCall(disposeMethod, STR_Dispose, argumentsCollection, null);
      string thisReference = CodeRush.Language.GenerateElement(new ThisReferenceExpression());
      elementBuilder.AddMethodCall(disposeMethod, "GC.SuppressFinalize", new string[]
      { thisReference
      });
    }
    #endregion
    #region AddFieldDisposeBlock
    private static void AddFieldDisposeBlock(ElementBuilder elementBuilder, If parentIfDisposingBlock, BaseVariable field)
    {
      If ifFieldIsAssignedBlock = new If();
      ifFieldIsAssignedBlock.Expression = CodeRush.Language.GetNullCheck(field.Name).Invert();
      elementBuilder.AddMethodCall(ifFieldIsAssignedBlock, field.Name + CodeRush.Language.MemberAccessOperator + STR_Dispose);
      elementBuilder.AddAssignment(ifFieldIsAssignedBlock, field.Name, CodeRush.Language.GetNullReferenceExpression());
      parentIfDisposingBlock.AddNode(ifFieldIsAssignedBlock);
    }
    #endregion
    #region AddVirtualDisposeMethod
    private void AddVirtualDisposeMethod(ElementBuilder elementBuilder)
    {
      TypeDeclaration activeType = _ActiveClass;
      Method virtualDisposeMethod = elementBuilder.AddMethod(null, null, STR_Dispose);

      if (activeType.ElementType == LanguageElementType.Struct)
      {
        virtualDisposeMethod.Visibility = MemberVisibility.Public;
      }
      else
      {
      virtualDisposeMethod.Visibility = MemberVisibility.Protected;
      virtualDisposeMethod.IsVirtual = true;
      }

      string typeName = CodeRush.Language.GetSimpleTypeName("System.Boolean");
      virtualDisposeMethod.AddParameter(new Param(typeName, "disposing"));
      If ifDisposingBlock = new If();
      ifDisposingBlock.Expression = new ElementReferenceExpression("disposing");
      virtualDisposeMethod.AddNode(ifDisposingBlock);
      // Dispose fields individually...
      foreach (BaseVariable field in _ActiveClass.AllFields)
        if (field.Is("System.IDisposable"))
          AddFieldDisposeBlock(elementBuilder, ifDisposingBlock, field);
    }
    #endregion
    private void AddDestructorMember(ElementBuilder elementBuilder)
    {
      Method destructor = elementBuilder.AddMethod(null, null, _ActiveClass.Name);
      destructor.MethodType = MethodTypeEnum.Destructor;

      ExpressionCollection arguments = new ExpressionCollection();
      arguments.Add(GetBooleanLiteral(false));
      elementBuilder.AddMethodCall(destructor, STR_Dispose, arguments, null);
    }
    #region cpImplementIDisposable_Apply
    private void cpImplementIDisposable_Apply(object sender, ApplyContentEventArgs ea)
    {
      _ActiveClass = ea.ClassInterfaceOrStruct as TypeDeclaration;
      if (_ActiveClass == null)
        return;
      _TextDocument = ea.TextDocument;
      ElementBuilder elementBuilder = new ElementBuilder();
      AddDisposeImplementer(elementBuilder);
      AddVirtualDisposeMethod(elementBuilder);
      AddDestructorMember(elementBuilder);
      _DisposableImplementationCode = elementBuilder.GenerateCode(_TextDocument.Language);
      if (_ActiveClass.FirstChild == null)
      {
        SourcePoint startPt = SourcePoint.Empty;
        CodeRush.Language.GetCodeBlockStart(_ActiveClass, ref startPt);
        _InsertionPoint = startPt;
        GenerateIDisposableImplementationCode();
      }
      else
      {
        targetPicker1.Code = _DisposableImplementationCode;
        LanguageElement target = _ActiveClass.FirstChild;
        foreach (Method method in _ActiveClass.AllMethods)
        {
          target = method;
          break;
        }
        targetPicker1.Start(ea.TextView, target, InsertCode.UsePicker);
      }
    }
    #endregion
    #region GenerateIDisposableImplementationCode
    private void GenerateIDisposableImplementationCode()
    {
      using (_TextDocument.NewCompoundAction("Implement IDisposable"))
      {
        SourceRange insertedRange = _TextDocument.InsertText(_InsertionPoint, _DisposableImplementationCode);
        insertedRange.End.Line++;
        _TextDocument.Format(insertedRange);
        CodeRush.Source.ImplementInterface(_ActiveClass, new Interface("IDisposable"));
        CodeRush.Source.DeclareNamespaceReference("System");
      }
    }
    #endregion
    private void cpImplementIDisposable_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
    {
      Class activeClass = ea.ClassInterfaceOrStruct as Class;
      if (activeClass == null || activeClass.IsStatic || activeClass.IsFakeNode || activeClass.ElementType == LanguageElementType.Interface)
        return;
      if (ea.Caret.Line == activeClass.Range.Start.Line)
        ea.Available = !AlreadyImplementsIDisposable(activeClass);
    }
    #region targetPicker1_TargetSelected
    private void targetPicker1_TargetSelected(object sender, TargetSelectedEventArgs ea)
    {
      _InsertionPoint = ea.Location.SourcePoint;
      GenerateIDisposableImplementationCode();
    }
    #endregion

    public static bool CanAnalyseAsDisposable(IFieldElement field)
    {
      if (field == null)
        return false;
      return !field.IsAspxTag && !field.IsRunAtServer;
    }
    public static IList<IFieldElement> GetDisposableFieldsThatHaveNotBeenDisposed(ScopeResolveResult resolveResult, ISourceFile scope, IClassElement iClassElement, out IIfStatement parentIfDisposing)
    {
      parentIfDisposing = null;
      IList<IFieldElement> disposableFields = new List<IFieldElement>();
      foreach (IElement child in iClassElement.AllChildren)
      {
        if (child.FirstFile == null)
          continue;
        // fix for partial classes
        if (child.FirstFile.Name != scope.Name)
          continue;
        IFieldElement iBaseVariable = child as IFieldElement;
        if (iBaseVariable != null)
        {
          if (CanAnalyseAsDisposable(iBaseVariable) &&
            iBaseVariable.Is("System.IDisposable") && !IsDisposed(resolveResult, iClassElement, iBaseVariable))
            disposableFields.Add(iBaseVariable);
        }
        else
          if (parentIfDisposing == null)
          {
            IMethodElement iMethodElement = child as IMethodElement;
            if (iMethodElement != null && iMethodElement.Name == STR_Dispose && iMethodElement.Parameters.Count == 1)
            {
              string paramName = iMethodElement.Parameters[0].Name;
              foreach (IElement potentialStatement in iMethodElement.AllChildren)
              {
                IIfStatement iIfStatement = potentialStatement as IIfStatement;
                if (iIfStatement != null)
                {
                  IExpression condition = iIfStatement.Condition;
                  if (condition != null && (condition.Name == paramName || (condition is ILogicalOperationExpression && (condition as ILogicalOperationExpression).LeftSide.Name == paramName)))
                  {
                    // We have found the "if (disposing)" block of code!
                    parentIfDisposing = iIfStatement;
                    break;
                  }
                }
              }
            }
          }
      }
      return disposableFields;
    }
    private static bool IsDisposed(ScopeResolveResult resolveResult, IClassElement scope, IFieldElement field)
    {
      if (scope == null || field == null)
        return false;
      IElementCollection references = field.FindAllReferences(scope);
      foreach (IElement reference in references)
      {
        IElement parent = reference.Parent;
        if (parent != null)
        {
          IWithSource withSource = parent as IWithSource;
          if (withSource != null)
          {
            if (withSource.Source == reference)
            {
              if (parent.Name == STR_Dispose)
              {
                IMethodElement parentMethod = reference.ParentMethod;
                if (parentMethod != null && parentMethod.Name == STR_Dispose)
                  return true;
              }
            }
          }
          // B188310
          if (IsAddedToControls(parent))
            return true;
          if (ComponentsInstancePassedIntoConstructor(resolveResult, parent))
            return true;
          if (PassedIntoDifferentDisposeMethod(resolveResult, parent, reference))
            return true;
        }
      }
      return false;
    }

    private static bool PassedIntoDifferentDisposeMethod(ScopeResolveResult resolveResult, IElement parent, IElement reference)
    {
      if (parent == null || reference == null)
        return false;

      if (parent.ElementType != LanguageElementType.MethodCall && parent.ElementType != LanguageElementType.MethodCallExpression)
        return false;

      IWithArguments withArguments = parent as IWithArguments;
      if (withArguments == null)
        return false;

      int argumentIndex = withArguments.Args.IndexOf(reference as IExpression);
      if (argumentIndex < 0)
        return false;

      IWithParameters declaration = GetDeclaration(resolveResult, parent);
      if (declaration == null)
        return false;

      if (declaration.Parameters.Count < argumentIndex)
        return false;

      IParameterElement param = declaration.Parameters[argumentIndex];
      if (param == null)
        return false;

      IElementCollection references = param.FindAllReferences(param.ParentMethod);
      foreach (IElement paramReference in references)
      {
        IElement referenceParent = paramReference.Parent;
        IWithSource referenceParentWithSource = referenceParent as IWithSource;
        if (referenceParentWithSource == null)
          continue;

        if (referenceParentWithSource.Source == paramReference)
          if (referenceParent.Name == STR_Dispose)
            return true;
      }
      return false;
    }

    private static IWithParameters GetDeclaration(ScopeResolveResult resolveResult, IElement parent)
    {
      IWithParameters declaration = null;
      if (resolveResult != null)
      {
        ReferenceDeclarationMapping mapping = resolveResult.GetReferenceDeclarationMapping();
        if (mapping != null)
        {
          declaration = mapping[parent] as IWithParameters;
          if (declaration == null)
            declaration = parent.GetDeclaration(false) as IWithParameters;
        }
      }
      else
        declaration = parent.GetDeclaration(false) as IWithParameters;
      return declaration;
    }

    private static bool ComponentsInstancePassedIntoConstructor(ScopeResolveResult resolveResult, IElement parent)
    {
      IAssignmentStatement assignment = parent as IAssignmentStatement;
      if (assignment == null)
        return false;

      IObjectCreationExpression objectCreationExp = assignment.Expression as IObjectCreationExpression;
      if (objectCreationExp == null)
        return false;

      if (objectCreationExp.Arguments.Count != 1)
        return false;

      IElementReferenceExpression referenceExp = objectCreationExp.Arguments[0] as IElementReferenceExpression;
      if (referenceExp == null)
        return false;

      if (referenceExp.Name != "components")
        return false;

      if (resolveResult != null)
      {
        ReferenceDeclarationMapping mapping = resolveResult.GetReferenceDeclarationMapping();
        if (mapping != null)
        {
          IHasType declaration = mapping[referenceExp] as IHasType;
          if (declaration != null && declaration.Type != null)
          {
            IElement typeDeclaration = mapping[declaration.Type];
            if (typeDeclaration != null)
              return typeDeclaration.FullName == "System.ComponentModel.IContainer";
          } 
        }
      }

      // NOTE: true by default, assuming that components is the corrent reference...
      return true;
    }

    private static bool IsAddedToControls(IElement parent)
    {
      if (parent == null)
        return false;
      IMethodCallStatement methodCall = parent as IMethodCallStatement;
      if (methodCall == null || methodCall.Children.Count == 0)
        return false;
      IMethodReferenceExpression methodRef = methodCall.Children[0] as IMethodReferenceExpression;
      if (methodRef == null)
        return false;
      if (methodRef.Name != "Add")
        return false;
      IExpression source = methodRef.Source;
      if (source == null)
        return false;
      // TODO: if needed - resolve the source to check against the "ConrolCollection" type...
      return source.Name == "Controls";
    }
    private void ipFieldsShouldBeDisposed_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
    {
      FieldShouldBeDisposedSearcher searcher = new FieldShouldBeDisposedSearcher();
      searcher.CheckCodeIssues(ea);
    }

    #region AlreadyImplementsIDisposable
    public static bool AlreadyImplementsIDisposable(IClassElement iClassElement)
    {
      ITypeElement[] baseTypes = iClassElement.GetBaseTypes();
      foreach (ITypeElement item in baseTypes)
        if (item.Is("System.IDisposable"))
          return true;
      return false;
    }
    #endregion
    private void ipClassShouldImplementIDisposable_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
    {
      IEnumerable<IElement> enumerable = ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.Class));
      foreach (IElement element in enumerable)
      {
        IClassElement iClassElement = element as IClassElement;
        if (iClassElement == null)
          continue;
        if (iClassElement.IsStatic)
          continue;
        if (AlreadyImplementsIDisposable(iClassElement))
          continue;
        // We do NOT implement IDisposable! Let's see if any of the fields implement IDisposable....
        foreach (IElement child in iClassElement.AllChildren)
        {
          IFieldElement iBaseVariable = child as IFieldElement;
          if (iBaseVariable == null)
            continue;
          if (iBaseVariable.Is("System.IDisposable"))
          {
            TextRange issueRange = GetIssueRange(ea.Scope, iClassElement);
            ea.AddWarning(issueRange, ipClassShouldImplementIDisposable.DisplayName);
            break;
          }
        }
      }
    }
    private TextRange GetIssueRange(IElement scope, ITypeElement element)
    {
      int filesCount = element.Files.Count;
      if ((element.IsPartial || filesCount > 1) && scope is ISourceFile)
      {
        for (int i = 0; i < filesCount; i++)
        {
          ISourceFile typeFile = element.Files[i];
          if (typeFile.Name == scope.Name)
            if (i < element.NameRanges.Count)
              return element.NameRanges[i];
        }
      }
      return element.FirstNameRange;
    }
    private void cpDisposeFields_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
    {
      IClassElement iClassElement = ea.ClassInterfaceOrStruct as IClassElement;
      if (iClassElement == null)
        return;
      if (!AlreadyImplementsIDisposable(iClassElement))
        return;
      // We DO implement IDisposable! Let's make sure all the fields are disposed....
      IIfStatement parentIfDisposing;
      IList<IFieldElement> disposableFields = GetDisposableFieldsThatHaveNotBeenDisposed(null, ea.ClassInterfaceOrStruct.GetSourceFile(), iClassElement, out parentIfDisposing);
      if (disposableFields.Count > 0 && parentIfDisposing != null)
      {
        If ifParent = LanguageElementRestorer.ConvertToLanguageElement(parentIfDisposing) as If;
        if (ifParent != null)
        {
          ea.Available = true;
          ElementBuilder elementBuilder = new ElementBuilder();
          If newIfStatement = ifParent.Clone() as If;
          elementBuilder.AddNode(null, newIfStatement);
          foreach (BaseVariable disposableField in disposableFields)
            AddFieldDisposeBlock(elementBuilder, newIfStatement, disposableField);
          _CodeForNewIfDisposingBlock = elementBuilder.GenerateCode();
          _OldIfDisposingBlockRange = ifParent.Range;
        }
      }
    }
    private void cpDisposeFields_Apply(object sender, ApplyContentEventArgs ea)
    {
      ea.TextDocument.QueueDelete(_OldIfDisposingBlockRange);
      ea.TextDocument.QueueInsert(_OldIfDisposingBlockRange.Top, _CodeForNewIfDisposingBlock);
      ea.TextDocument.ApplyQueuedEdits("Dispose Fields", true);
    }
    private void cpDisposeFields_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
    {
      ea.AddCodePreview(_OldIfDisposingBlockRange.Top, _CodeForNewIfDisposingBlock);
      ea.AddStrikethrough(_OldIfDisposingBlockRange);
    }
  }
}
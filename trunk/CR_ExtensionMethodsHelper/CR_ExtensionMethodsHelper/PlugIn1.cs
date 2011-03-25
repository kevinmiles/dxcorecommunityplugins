using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Collections.Generic;
using System.Collections;

namespace CR_ExtensionMethodsHelper
{
  public partial class PlugIn1 : StandardPlugIn
  {
    private string _NamespaceOfExtensionMethod;

    // DXCore-generated code...
    #region InitializePlugIn
    public override void InitializePlugIn()
    {
      base.InitializePlugIn();

      //
      // TODO: Add your initialization code here.
      //
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

    
    // private methods...
    private bool IsMethodCallReference(LanguageElement element)
    {
      return element != null && (element.ElementType == LanguageElementType.MethodCall || element.ElementType == LanguageElementType.MethodCallExpression);
    }

    private LanguageElement GetActiveMethodReference(LanguageElement element)
    {
      if (element == null)
        return null;

      if (IsMethodCallReference(element))
        return element;

      if (IsMethodCallReference(element.Parent))
        return element.Parent;

      return null;
    }

    private IEnumerable GetProjectSymbols()
    {
      SolutionElement scope = CodeRush.Source.ActiveSolution;
      if (scope == null)
        return null;
      List<IElement> list = new List<IElement>();
      foreach (ProjectElement project in scope.AllProjects)
      {
        foreach (AssemblyReference reference in project.AssemblyReferences)
        {
          if (reference == null)
            continue;
          IElementCollection rootElements = reference.AssemblyModel.RootElements;
          list.AddRange(rootElements);
        }
        foreach (IElement element in project.ProjectSymbols.Values)
          list.Add(element);
      }
      return list;
    }

    private bool IsValidReferenceAndQualifier(LanguageElement activeRerence, out ITypeElement callerType, out Expression qualifier)
    {
      qualifier = null;
      callerType = null;
      if (!(activeRerence is IHasQualifier))
        return false;

      // should be undeclared....
      IElement declaration = activeRerence.GetDeclaration(false);
      if (declaration != null)
        return false;


      qualifier = (activeRerence as IHasQualifier).Qualifier;
      if (qualifier is MethodReferenceExpression)
        qualifier = (qualifier as MethodReferenceExpression).Qualifier;
      if (qualifier == null)
        return false;

      callerType = qualifier.Resolve(ParserServices.SourceTreeResolver) as ITypeElement;
      if (callerType == null)
        return false;

      return true;
    }

    private bool ExtendsTheCallerType(IMethodElement method, ITypeElement callerType,Expression qualifier)
    {
      if (method == null || callerType == null)
        return false;


      if (method.Parameters.Count == 0)
        return false;
      ISourceTreeResolver resolver = ParserServices.SourceTreeResolver;
      ExpressionCollection arguments = new ExpressionCollection();
      arguments.Add(qualifier);
      method = GenericElementActivator.ActivateMemberIfNeeded(resolver, method, arguments, null, ArgumentsHelper.ResolveArgumentTypes(resolver,arguments)) as IMethodElement;

      if (method == null)
        return false;

      IParameterElement extensionParam = method.Parameters[0] as IParameterElement;
      if (extensionParam == null)
        return false;


      ITypeReferenceExpression typeRef = extensionParam.Type;
      if (typeRef == null)
        return false;
      ITypeElement type = typeRef.GetDeclaration() as ITypeElement;
      if (type == null)
        return false;
      IArrayTypeElement arrayType = callerType as IArrayTypeElement;
      //if (arrayType != null)
      //{
      //  return true;
      //}
      //else
        return ArgumentsHelper.HasParamConversion(resolver, extensionParam, callerType, qualifier, TypeConversionMode.ImplicitConversion);
    }

    private IMethodElement FindExtensionMethodInClass(IClassElement classElement, string methodName, ITypeElement callerType,Expression qualifier)
    {
      if (classElement == null || callerType == null)
        return null;

      bool isCaseSensitiveLanguage = CodeRush.Language.IsCaseSensitive;

      IMemberElementCollection classElementMembers = classElement.Members;
      foreach (IElement member in classElementMembers)
      {
        IMethodElement method = member as IMethodElement;
        if (method == null)
          continue;

        if (String.Compare(method.Name, methodName, !isCaseSensitiveLanguage) == 0)
          if (method.IsExtensionMethod())
            if (ExtendsTheCallerType(method, callerType, qualifier))
              return method;
      }
      return null;
    }
    private IClassElement GetStaticClass(IElement element)
    {
      IClassElement classElement = element as IClassElement;
      if (classElement == null)
        return null;
      if (!classElement.IsStatic)
        return null;
      return classElement;
    }
    private IMethodElement FindExtensionMethodInNamespace(INamespaceElement namespaceElement, string methodName, ITypeElement callerType, Expression qualifier)
    {
      IClassElement classElement;
      IMethodElement extensionMethod;

      foreach (ITypeElement namespaceChild in namespaceElement.Types)
      {
        classElement = GetStaticClass(namespaceChild);
        if (classElement == null)
          continue;
        extensionMethod = FindExtensionMethodInClass(classElement, methodName, callerType, qualifier);
        if (extensionMethod != null)
          return extensionMethod;
      }
      foreach (INamespaceElement childNamespace in namespaceElement.Namespaces)
      {
        extensionMethod = FindExtensionMethodInNamespace(childNamespace, methodName, callerType,qualifier);
        if (extensionMethod != null)
          return extensionMethod;
      }
     return null;

    }
    private IMethodElement FindExtensionMethod(IEnumerable projectSymbols, string methodName, ITypeElement callerType, Expression qualifier)
    {
      if (projectSymbols == null || callerType == null)
        return null;

      foreach (IElement element in projectSymbols)
      {
        INamespaceElement namespaceElement = element as INamespaceElement;
        IClassElement classElement;
        IMethodElement extensionMethod;
        if (namespaceElement != null)
        {
          extensionMethod = FindExtensionMethodInNamespace(namespaceElement, methodName, callerType, qualifier);
          if (extensionMethod != null)
            return extensionMethod;
        }
        else
        {
          classElement = GetStaticClass(element);
          extensionMethod = FindExtensionMethodInClass(classElement, methodName, callerType, qualifier);
          if (extensionMethod != null)
            return extensionMethod;
        }
      }
      return null;
    }

    // event handlers...
    private void cpAddReference_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
    {
      LanguageElement activeMethodRerence = GetActiveMethodReference(ea.Element);
      if (activeMethodRerence == null)
        return;

      ITypeElement callerType;
      Expression qualifier;
      if (!IsValidReferenceAndQualifier(activeMethodRerence, out callerType, out qualifier))
        return;

      IEnumerable projectSymbols = GetProjectSymbols();
      if (projectSymbols == null)
        return;

      string methodNameToFind = activeMethodRerence.Name;
      IMethodElement extensionMethod = FindExtensionMethod(projectSymbols, methodNameToFind, callerType, qualifier);
      if (extensionMethod == null || extensionMethod.ParentNamespace == null)
        return;

      _NamespaceOfExtensionMethod = extensionMethod.ParentNamespace.FullName;

      ea.Available = true;
    }

    private void cpAddReference_Apply(object sender, ApplyContentEventArgs ea)
    {
      CodeRush.Source.DeclareNamespaceReference(_NamespaceOfExtensionMethod);
    }
  }
}
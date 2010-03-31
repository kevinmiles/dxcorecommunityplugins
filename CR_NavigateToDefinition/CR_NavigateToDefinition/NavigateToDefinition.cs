using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_NavigateToDefinition
{
  public partial class NavigateToDefinition : StandardPlugIn
  {
    private bool enabled = true;
    private bool dropMarker = true;
    private bool showBeacon = true;
    private bool useGoToDef = true;

    // DXCore-generated code...
    #region InitializePlugIn
    public override void InitializePlugIn()
    {
      base.InitializePlugIn();

      loadSettings();
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

    private void loadSettings()
    {
      using (DecoupledStorage storage = OptNavigateToDefinition.Storage)
      {
        enabled = storage.ReadBoolean("NavigateToDefinition", "Enabled", true);
        dropMarker = storage.ReadBoolean("NavigateToDefinition", "DropMarker", true);
        showBeacon = storage.ReadBoolean("NavigateToDefinition", "ShowBeacon", true);
        useGoToDef = storage.ReadBoolean("NavigateToDefinition", "UseGoToDef", true);
      }
    }

    private void NavigateToDefinition_OptionsChanged(OptionsChangedEventArgs ea)
    {
      loadSettings();
    }

    private void action1_Execute(ExecuteEventArgs ea)
    {
      if (enabled)
        navigateToDefinition(CodeRush.Source.Active);
    }

    private void navigationProvider1_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
    {
      if (enabled)
      {
        if (ea.Element == null)
          return;
        ea.Available = GetElementDeclaration(ea.Element) != null;
      }
    }

    private void navigationProvider1_Navigate(object sender, DevExpress.CodeRush.Library.NavigationEventArgs ea)
    {
      if (enabled)
        navigateToDefinition(ea.Element);
    }

    private static IElement GetElementDeclaration(LanguageElement element)
    {
      IElement declaration;

      if (elementIsReference(element.ElementType))
        declaration = element.GetDeclaration();
      else
        declaration = element;

      if (declaration != null && elementTypeIsSupported(declaration.ElementType))
        return declaration;

      return null;
    }

    private static bool elementIsReference(LanguageElementType elementType)
    {
      return (elementType == LanguageElementType.TypeReferenceExpression) ||
        (elementType == LanguageElementType.ElementReferenceExpression) ||
        (elementType == LanguageElementType.MethodReferenceExpression);
    }

    private static bool elementTypeIsSupported(LanguageElementType elementType)
    {
      return (elementType == LanguageElementType.Class) ||
        (elementType == LanguageElementType.Interface) ||
        (elementType == LanguageElementType.Method) ||
        (elementType == LanguageElementType.Property) ||
        (elementType == LanguageElementType.Struct) ||
        (elementType == LanguageElementType.Variable) ||
        (elementType == LanguageElementType.Parameter) ||
        (elementType == LanguageElementType.Const) ||
        (elementType == LanguageElementType.InitializedVariable) ||
        (elementType == LanguageElementType.ObjectCreationExpression) ||
        (elementType == LanguageElementType.ImplicitVariable);
    }

    private static bool elementTypeIsNested(LanguageElementType elementType)
    {
      return (elementType == LanguageElementType.Property) ||
        (elementType == LanguageElementType.Method);
    }

    private static bool checkIfParametersAreTheSame(IMethodElement methodElement, IMethodElement currMethodElement)
    {
      if ((methodElement.Parameters.Count == 0) && (currMethodElement.Parameters.Count == 0))
        return true;

      bool found = false;

      if (methodElement.Parameters.Count == currMethodElement.Parameters.Count)
      {
        for (int i = 0; i < methodElement.Parameters.Count && !found; i++)
        {
          if (methodElement.Parameters[i].Type.IsIdenticalTo(currMethodElement.Parameters[i].Type))
          {
            found = true;
          }
        }
      }

      return found;
    }

    private static IMemberElement getObjectCreationExpression_MemberElement(IObjectCreationExpression creationExp)
    {
      foreach (var child in creationExp.ObjectType.GetDeclaration().AllChildren)
      {
        IMethodElement childMethod = (child as IMethodElement);
        if ((childMethod != null) && (childMethod.IsConstructor))
        {
          if (creationExp.Arguments.Count == childMethod.Parameters.Count)
          {
            if (creationExp.Arguments.Count == 0)
              return childMethod;

            for (int i = 0; i < childMethod.Parameters.Count; i++)
            {
              if (creationExp.Arguments[i].Is(childMethod.Parameters[i].Type.GetDeclaration() as ITypeElement))
                return childMethod;
            }
          }
        }
      }

      return null;
    }

    private void navigateToDefinition(LanguageElement element)
    {
      try
      {
        IElement declaration = GetElementDeclaration(element);

        IMemberElement memberElement = null;

        if (declaration != null)
        {
          if (isImplicitVariableDeclarationType(declaration))
          {
            memberElement = ((ObjectCreationExpression)((ImplicitVariable)declaration).Expression).ObjectType.GetDeclaration() as IMemberElement;
          }
          else if (declaration.ElementType == LanguageElementType.ObjectCreationExpression) //new SomeClass(...)
          {
            IObjectCreationExpression creationExp = declaration as IObjectCreationExpression;
            memberElement = getObjectCreationExpression_MemberElement(creationExp);
            if (memberElement == null)
              memberElement = creationExp.ObjectType.GetDeclaration() as IMemberElement; //if can't find ctor, try at least to find the class
          }
          else
            memberElement = declaration as IMemberElement;
        }

        if (memberElement == null)
        {
          defaultGoToDefinition();
          return;
        }

        IMemberElement nestedElement = null;
        if (elementTypeIsNested(memberElement.ElementType))
        {
          nestedElement = memberElement;
          memberElement = nestedElement.ParentType;
        }

        if (memberElement != null)
        {
          IElement e = null;

          if (elementTypeIsLocal(memberElement.ElementType))
          {
            e = memberElement;
          }
          else
          {
            foreach (ProjectElement p in CodeRush.Source.ActiveSolution.AllProjects)
            {
              e = p.FindElementByFullName(memberElement.FullName, true);
              if (e != null)
                break;
            }
          }

          if (e != null)
          {
            SourceFile sourceFile = (e.FirstFile as SourceFile);
            TextPoint point = e.NameRanges[0].Start;

            if (nestedElement != null)
            {
              bool found = false;

              var methodElement = nestedElement as IMethodElement;

              foreach (IElement currElement in e.AllChildren)
              {

                if ((currElement.ElementType == nestedElement.ElementType) &&
                  (currElement.FullName.Equals(nestedElement.FullName)))
                {
                  var currMethodElement = currElement as IMethodElement;

                  if ((methodElement != null) && (currMethodElement != null))
                  {
                    found = checkIfParametersAreTheSame(methodElement, currMethodElement);
                    if (found)
                      point = currElement.NameRanges[0].Start;
                  }
                  else
                  {
                    point = currElement.NameRanges[0].Start;
                    found = true;
                  }

                  if (found)
                    break;
                }
              }

              if (!found)
              {
                if ((methodElement != null) && (methodElement.IsConstructor))
                {
                  //must be default constructor (with no args), so jump to class instead
                  point = e.NameRanges[0].Start;
                }
                else
                  point = e.FindChildByName(nestedElement.Name).NameRanges[0].Start;
              }

            }

            if (dropMarker)
              CodeRush.Markers.Drop(MarkerStyle.System);

            CodeRush.File.Activate(sourceFile.FilePath);

            CodeRush.Caret.MoveTo(point);

            if (showBeacon)
              locatorBeacon1.Start(CodeRush.Documents.ActiveTextView, point.Line, point.Offset);
          }
          else
          {
            defaultGoToDefinition();
          }
        }
      }
      catch (Exception ex)
      {
        ShowException(ex);
      }
    }

    private static bool isImplicitVariableDeclarationType(IElement element)
    {
      //if this is true, caret should be positioned over var keyword
      return (element.ElementType == LanguageElementType.ImplicitVariable) &&
        (((ImplicitVariable)element).Expression.ElementType == LanguageElementType.ObjectCreationExpression) &&
        (DevExpress.CodeRush.Core.CodeRush.Caret.OnDeclarationType);
    }

    public static void ShowException(Exception ex)
    {
      MessageBox.Show(ex.ToString(), "Error in CR_NavigateToDefinition plugin", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    private static bool elementTypeIsLocal(LanguageElementType elementType)
    {
      return (elementType == LanguageElementType.Variable) ||
        (elementType == LanguageElementType.InitializedVariable) ||
        (elementType == LanguageElementType.ImplicitVariable) ||
        (elementType == LanguageElementType.Parameter);
    }

    private void defaultGoToDefinition()
    {
      if (useGoToDef)
      {
        //show metadata
        CodeRush.Command.Execute("Edit.GoToDefinition");
      }
    }

  }
}
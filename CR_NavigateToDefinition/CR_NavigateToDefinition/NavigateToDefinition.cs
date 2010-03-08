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

    private IElement GetElementDeclaration(LanguageElement element)
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

    private bool elementIsReference(LanguageElementType elementType)
    {
      return (elementType == LanguageElementType.TypeReferenceExpression) ||
        (elementType == LanguageElementType.ElementReferenceExpression) ||
        (elementType == LanguageElementType.MethodReferenceExpression);
    }

    private bool elementTypeIsSupported(LanguageElementType elementType)
    {
      return (elementType == LanguageElementType.Class) ||
        (elementType == LanguageElementType.Interface) ||
        (elementType == LanguageElementType.Method) ||
        (elementType == LanguageElementType.Property) ||
        (elementType == LanguageElementType.Struct) ||
        (elementType == LanguageElementType.Variable) ||
        (elementType == LanguageElementType.InitializedVariable);
    }

    private bool elementTypeIsNested(LanguageElementType elementType)
    {
      return (elementType == LanguageElementType.Property) ||
        (elementType == LanguageElementType.Method) ||
        (elementType == LanguageElementType.Variable) ||
        (elementType == LanguageElementType.InitializedVariable);
    }

    private void navigateToDefinition(LanguageElement element)
    {
      IElement declaration = GetElementDeclaration(element);

      IMemberElement memberElement = declaration as IMemberElement;
      if (memberElement == null)
        return;

      IMemberElement nestedElement = null;
      if (elementTypeIsNested(memberElement.ElementType))
      {
        nestedElement = memberElement;
        memberElement = nestedElement.ParentType;
      }

      if (memberElement != null)
      {
        IElement e = null;

        foreach (ProjectElement p in CodeRush.Source.ActiveSolution.AllProjects)
        {
          e = p.FindElementByFullName(memberElement.FullName, true);
          if (e != null)
            break;
        }

        if (e != null)
        {
          SourceFile sourceFile = (e.FirstFile as SourceFile);
          TextPoint point = e.NameRanges[0].Start;
          //TextRange range = e.NameRanges[0];

          if (nestedElement != null)
          {
            point = e.FindChildByName(nestedElement.Name).NameRanges[0].Start;
            //range = e.FindChildByName(nestedElement.Name).NameRanges[0];
          }

          if (dropMarker)
            CodeRush.Markers.Drop(MarkerStyle.System);

          CodeRush.File.Activate(sourceFile.FilePath);
          
          CodeRush.Caret.MoveTo(point);

          if (showBeacon)
            locatorBeacon1.Start(CodeRush.Documents.ActiveTextView, point.Line, point.Offset);

          //CodeRush.Documents.ActiveTextView.Selection.Set(range);
          //if (showBeacon)
          //  locatorBeacon1.Start(CodeRush.Documents.ActiveTextView, range.Start.Line, range.Start.Offset);
        }
        else
        {
          if (useGoToDef)
          {
            //show metadata
            CodeRush.Command.Execute("Edit.GoToDefinition");
          }
        }
      }
    }

  }
}
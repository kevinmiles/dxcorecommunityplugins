using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_ReSharperCompatibility
{
  public partial class FullyImplemented : StandardPlugIn
  {
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

    private void actReSharperDuplicateLine_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("DuplicateLine");
    }

    private void actReSharperExtendSelection_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("SelectionExpand");
    }

    private void actReSharperShrinkSelection_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("SelectionReduce");
    }

    private void actReSharperHighlightUsages_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("HighlightReferences");
    }

    private void actReSharperGoToFileMember_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("QuickNav", ",Methods and Properties and Events and Fields,AllVisibilities,CurrentFile");
    }

    private void actReSharperGoToSymbol_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("QuickNav");
    }

    private void actReSharperGoToInheritor_Execute(ExecuteEventArgs ea)
    {
      if (CodeRush.Source.ActiveMember != null)
        CodeRush.Command.Execute("Navigate", "Overrides");
      else
        CodeRush.Command.Execute("Navigate", "Descendants");
    }

    private void actReSharperGoToPreviousMemberOrTag_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("Navigate", "Previous Member");
    }

    private void actReSharperGoToLastEditLocation_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("View.NavigateBackward");
    }

    private void actReSharperRename_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("Refactor", "Rename");
    }

    private void actReSharperEncapsulateField_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("Refactor", "Encapsulate Field");
    }

    private void actReSharperIntroduceVariable_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("Refactor", "Introduce Local");
    }

    private void actReSharperExtractMethod_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("Refactor", "Extract Method");
    }

    private void actReSharperRefactorThis_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("Refactor");
    }

    private void actReSharperViewRecentFiles_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("BrowseRecentFiles");
    }

    private void actReSharperGoToDeclaration_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("Edit.GoToDefinition");
    }

    private void actReSharperNextUsage_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("ReferenceNext");
    }

    private void actReSharperPreviousUsage_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("ReferencePrevious");
    }

    private void actReSharperGoToNextMemberOrTag_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("Navigate", "Next Member");
    }

    private void actReSharperGoToBase_Execute(ExecuteEventArgs ea)
    {
      if (CodeRush.Source.ActiveMember != null)
      {
        // TODO: Replace with Navigate to closets ancestor virtual member *or* override.
        CodeRush.Command.Execute("Navigate", "Virtual Member Ancestor");
      }
      else
        CodeRush.Command.Execute("Navigate", "Base Types");
    }

    private void actReSharperGoToContainingDeclaration_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("NavParent");
    }

    private void actReSharperGoToFile_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("QuickFileNav");
    }

    private void actReSharperGoToType_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("QuickNav", "AllTypes,,AllVisibilities,CurrentSolution");
    }

    private void actReSharperNavigateFromHere_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("Navigate");
    }

    private void actReSharperGenerateCode_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("Refactor");
    }

    private void actReSharperFindUsages_Execute(ExecuteEventArgs ea)
    {
      CodeRush.Command.Execute("ShowReferences");
    }

    private void actReSharper_ShowQuickFixesAndContextActions_Execute(ExecuteEventArgs ea)
    {
      // TODO: Replace with actual call to show the CI Solutions window.
      CodeRush.Command.Execute("Refactor");
    }
  }
}
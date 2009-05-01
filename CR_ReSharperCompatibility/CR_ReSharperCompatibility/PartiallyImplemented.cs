using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Collections.Generic;

using DevExpress.CodeRush.Win32;
using System.Runtime.InteropServices;

namespace CR_ReSharperCompatibility
{
  public partial class PartiallyImplemented : StandardPlugIn
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

    private void actReSharperChangeSignature_Execute(ExecuteEventArgs ea)
    {
      bool allowPersistResponse = false;
      string title = "Change Signature";
      string message = "CodeRush includes several useful refactorings dedicated to changing signatures. Select from one of the signature-changing refactorings below.";
      Redirects redirects = new Redirects();

      redirects.AddRefactoring("Add Parameter", "Place caret inside the parameter list before invoking.");
      redirects.AddRefactoring("Create Overload", "Place caret on the member declaration before invoking.");
      redirects.AddRefactoring("Decompose Parameter", "Place caret on the parameter to decompose before invoking.");
      redirects.AddRefactoring("Introduce Parameter Object", "Select the parameters to consolidate before invoking.");
      redirects.AddRefactoring("Make Extension", "Place caret on a method signature with parameters to extend before invoking.");
      redirects.AddRefactoring("Make Member non-Static", "Place caret on a static member signature before invoking.");
      redirects.AddRefactoring("Make Member Static", "Place caret on an instance member signature that can be made static before invoking.");
      redirects.AddRefactoring("Promote to Parameter", "Place caret on a local or field variable that can be promoted to a parameter before invoking.");
      redirects.AddRefactoring("Reorder Parameters", "Place caret on a parameter that can be reordered before invoking.");
      redirects.AddRefactoring("Safe Rename", "Place caret on a public member before invoking.");

      FrmResharperCompatibility frmResharperCompatibility = new FrmResharperCompatibility(title, message, redirects, allowPersistResponse);
      frmResharperCompatibility.ShowDialog(CodeRush.IDE);
      if (frmResharperCompatibility.Result == CompatibilityResult.ExecuteCommand)
        CodeRush.Command.Execute(frmResharperCompatibility.Command, frmResharperCompatibility.Parameters);
    }

    private void actReSharperSurroundWithTemplate_Execute(ExecuteEventArgs ea)
    {
      bool allowPersistResponse = false;
      string title = "Surround With Template";
      string message = "CodeRush includes several built-in features to wrap and modify selections. There are keyboard shortcuts for many of these (just select the code you want to modify and press the specified key), and you can also access these from the right-click context menu. Select from one of the options below.";
      Redirects redirects = new Redirects();

      redirects.AddSelectionEmbedding("block", "Embeds a selection inside block delimiters (e.g., {} braces in C# or C++).");
      redirects.AddSelectionEmbedding("lock", "Embeds a selection inside a lock statement.");
      redirects.AddSelectionEmbedding("region", "Embeds a selection inside a region.");
      redirects.AddSelectionEmbedding("SyncLock", "Embeds a selection inside a SyncLock statement.");
      redirects.AddSelectionEmbedding("ToString", "Converts a selection to a string, escaping characters as needed.");
      redirects.AddSelectionEmbedding("try/catch", "Embeds a selection inside a try/catch block.");
      redirects.AddSelectionEmbedding("try/catch/finally", "Embeds a selection inside a try/catch/finally block.");
      redirects.AddSelectionEmbedding("try/finally", "Embeds a selection inside a try/finally block.");
      redirects.AddSelectionEmbedding("using", "Embeds a selection inside a using statement.");
      redirects.AddSelectionEmbedding("WaitCursor", "Embeds a selection inside code that displays an hourglass cursor while the code executes.");
      redirects.AddOptionsPage("Embedding Options && Customization", "Editor\\Selections\\Embedding", "Click this button to view, change, and create custom embeddings.");

      FrmResharperCompatibility frmResharperCompatibility = new FrmResharperCompatibility(title, message, redirects, allowPersistResponse);
      frmResharperCompatibility.ShowDialog(CodeRush.IDE);
      if (frmResharperCompatibility.Result == CompatibilityResult.ExecuteCommand)
        CodeRush.Command.Execute(frmResharperCompatibility.Command, frmResharperCompatibility.Parameters);
    }
  }
}
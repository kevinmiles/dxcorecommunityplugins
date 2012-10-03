using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.IO;

namespace CR_ForceProjectConverter
{
  public partial class PlugIn1 : StandardPlugIn
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

    private void PlugIn1_SolutionOpened()
    {
      SolutionElement activeSolution = CodeRush.Source.ActiveSolution;
      if (activeSolution == null)
      	return;

      List<string> unresolvedAssemblies = new List<string>();
      foreach (ProjectElement project in activeSolution.ProjectElements)
      {
        foreach (AssemblyReference reference in project.AssemblyReferences)
        {
          if (reference.Name.StartsWith("DevExpress"))
            if (!File.Exists(reference.FilePath))
              unresolvedAssemblies.Add(reference.Name);
        }
      }

      if (unresolvedAssemblies.Count > 0)
      {
        UnresolvedReferences form = new UnresolvedReferences(unresolvedAssemblies);
        form.ShowDialog(CodeRush.IDE);
      }
    }
  }
}
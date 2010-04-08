using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_MarkerCloseOnCollect
{
  public partial class MarkerCloseOnCollect : StandardPlugIn
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

    private void action1_Execute(ExecuteEventArgs ea)
    {
      Document currentDocument = CodeRush.Documents == null ? null : CodeRush.Documents.Active;

      IMarker marker = CodeRush.Markers.Collect();

      if ((marker != null) && (currentDocument != null) && 
        !marker.FileName.Equals(currentDocument.FullName, StringComparison.OrdinalIgnoreCase))
        currentDocument.Close(EnvDTE.vsSaveChanges.vsSaveChangesPrompt);
    }
  }
}
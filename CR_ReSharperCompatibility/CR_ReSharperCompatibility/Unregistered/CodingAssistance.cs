using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_ReSharperCompatibility.Unregistered
{
  public partial class CodingAssistance : StandardPlugIn
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

    //private void actReSharperMoveCodeRight_Execute(ExecuteEventArgs ea)
    //{
    //    if (CodeRush.Command.Exists("MoveCodeRight"))
    //        CodeRush.Command.Execute("MoveCodeRight");
    //    else
    //        System.Windows.Forms.MessageBox.Show(GetWikiLinkMessage("DX_MoveCode"));
    //}
    //private void actReSharperMoveCodeLeft_Execute(ExecuteEventArgs ea)
    //{
    //    if (CodeRush.Command.Exists("MoveCodeLeft"))
    //        CodeRush.Command.Execute("MoveCodeLeft");
    //    else
    //        System.Windows.Forms.MessageBox.Show(GetWikiLinkMessage("DX_MoveCode"));
    //}
    //private void actReSharperMoveCodeDown_Execute(ExecuteEventArgs ea)
    //{
    //    if (CodeRush.Command.Exists("MoveCodeDown"))
    //        CodeRush.Command.Execute("MoveCodeDown");
    //    else
    //        System.Windows.Forms.MessageBox.Show(GetWikiLinkMessage("DX_MoveCode"));
    //}
    //private void actReSharperMoveCodeUp_Execute(ExecuteEventArgs ea)
    //{
    //    if (CodeRush.Command.Exists("MoveCodeUp"))
    //        CodeRush.Command.Execute("MoveCodeUp");
    //    else
    //        System.Windows.Forms.MessageBox.Show(GetWikiLinkMessage("DX_MoveCode"));
    //}
    //private string GetWikiLinkMessage(string WikiPage)
    //{
    //    return String.Format("This function provided by a community plugin.{0}See {1}", Environment.NewLine, GetWikiPage(WikiPage));
    //}
    //public string GetWikiPage(string WikiPage)
    //{
    //    return String.Format("http://code.google.com/p/dxcorecommunityplugins/wiki/{0}", WikiPage);
    //}
    
  }
}
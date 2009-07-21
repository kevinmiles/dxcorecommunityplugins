using System;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.Menus;
using DevExpress.CodeRush.PlugInCore;

namespace CR_CCConsole
{
    public partial class CCNetStatus : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            var thisMenuItem = CodeRush.Menus.ToolWindows.FindByCaption("CruiseControl.Net Project Status");
            CodeRush.Menus.ToolWindows.Remove(thisMenuItem);
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

        private void CCNetStatus_SolutionOpened()
        {
            CreateToolbarItem();
        }

        internal void CreateToolbarItem()
        {
            var bmp = new TransparentBitmap(ccNetAction.Image);
            bmp.TransparentColor = ccNetAction.ImageBackColor;
            var menuBar = CodeRush.Menus.Bars.Add("CCNet Bar");
            menuBar.Position = BarPosition.Top;
            var btn = menuBar.AddButton();
            btn.Caption = "Show CCNet";
            btn.Visible = true;
            btn.Enabled = true;
            btn.SetFace(bmp.Bitmap, bmp.MaskBitmap);
            btn.Click += (s, e) => CCNetStatusWindow.ShowWindow();
            menuBar.Visible = true;

        }

        private void ccNetAction_Execute(ExecuteEventArgs ea)
        {
            CCNetStatusWindow.ShowWindow();
        }
    }
}
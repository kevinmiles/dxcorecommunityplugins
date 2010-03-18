using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace DX_DirectOptions
{
    public partial class PlugIn1 : StandardPlugIn
    {
        private const int LastPosition = 9999;
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            createShowShortcutOptions();
            createShowTemplateOptions();
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
        public void createShowTemplateOptions()
        {
            if (!CodeRush.Options.IsRegistered("Editor\\Templates"))
                return;
            DevExpress.CodeRush.Core.Action ShowTemplateOptions = new DevExpress.CodeRush.Core.Action(components);
            ((System.ComponentModel.ISupportInitialize)(ShowTemplateOptions)).BeginInit();
            ShowTemplateOptions.ActionName = "ShowTemplateOptions";
            ShowTemplateOptions.ButtonText = "&Templates..."; // Used if button is placed on a menu.
            ShowTemplateOptions.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.DevExpress;
            ShowTemplateOptions.Position = LastPosition;
            ShowTemplateOptions.RegisterInCR = true;
            ShowTemplateOptions.Execute += ShowTemplateOptions_Execute;
            ((System.ComponentModel.ISupportInitialize)(ShowTemplateOptions)).EndInit();
        }
        private void ShowTemplateOptions_Execute(ExecuteEventArgs ea)
        {
            CodeRush.Options.Show("Editor", "Templates");
        }
        public void createShowShortcutOptions()
        {
            DevExpress.CodeRush.Core.Action ShowShortcutOptions = new DevExpress.CodeRush.Core.Action(components);
            ((System.ComponentModel.ISupportInitialize)(ShowShortcutOptions)).BeginInit();
            ShowShortcutOptions.ActionName = "ShowShortcutOptions";
            ShowShortcutOptions.ButtonText = "&Shortcuts..."; // Used if button is placed on a menu.
            ShowShortcutOptions.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.DevExpress;
            ShowShortcutOptions.Position = LastPosition;
            ShowShortcutOptions.RegisterInCR = true;
            ShowShortcutOptions.Execute += ShowShortcutOptions_Execute;
            ((System.ComponentModel.ISupportInitialize)(ShowShortcutOptions)).EndInit();
        }
        private void ShowShortcutOptions_Execute(ExecuteEventArgs ea)
        {
            CodeRush.Options.Show("IDE", "Shortcuts");
        }
    }
}
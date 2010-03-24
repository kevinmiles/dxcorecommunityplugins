using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Threading;
using System.Collections.Generic;
using DevExpress.CodeRush.Menus;

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
            EventNexus.DXCoreLoaded += new DXCoreLoadedEventHandler(EventNexus_DXCoreLoaded);

        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            EventNexus.DXCoreLoaded -= new DXCoreLoadedEventHandler(EventNexus_DXCoreLoaded);
            base.FinalizePlugIn();
        }
        #endregion

        #region Magic by 'The Great Wizard Skorkin'
        void EventNexus_DXCoreLoaded(DXCoreLoadedEventArgs ea)
        {
            RearrangeMenuItemsAsync();
        }

        private void RearrangeMenuItemsAsync()
        {
            SendOrPostCallback callback = delegate(object state) { this.AsyncCallBack(); };
            SynchronizationContext sync = SynchronizationContext.Current;
            if (sync != null)
                sync.Post(callback, null);
            else
                callback(null);
        }

        void AsyncCallBack()
        {
            IEnumerator<IMenuControl> e = CodeRush.Menus.DXCore.GetEnumerator();
            while (e.MoveNext())
            {
                IMenuControl current = e.Current;
                if (current.Caption == "&Options...")
                {
                    SetNextControlBeginGroup(current, false);
                }
                else if (current.Caption == "&Templates...")
                {
                    SetNextControlBeginGroup(current, false);
                }
                else if (current.Caption == "&Shortcuts...")
                {
                    SetNextControlBeginGroup(current, true);
                    break;
                }
            }
        }

        void SetNextControlBeginGroup(IMenuControl current, bool value)
        {
            IMenuControl nextControl = current.Parent[current.Index + 1];
            if (nextControl != null)
                nextControl.BeginGroup = value;
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
            ShowTemplateOptions.Position = 3;
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
            ShowShortcutOptions.Position = 4;
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
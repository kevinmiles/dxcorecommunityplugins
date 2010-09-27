using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Menus;
using System.Collections.Generic;

namespace DX_StudioSizer
{
    public partial class PlugIn1 : StandardPlugIn
    {
        #region DX Magic
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            RefreshToolbar();
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
        #endregion

        private MenuBar mMenuBar;
        private List<ScreenSize> DefaultResolutions()
        {
            List<ScreenSize> mResolutions = new List<ScreenSize>();
            mResolutions.Add(new ScreenSize(1600, 1200));
            mResolutions.Add(new ScreenSize(1600, 1050));
            mResolutions.Add(new ScreenSize(1280, 1024));
            mResolutions.Add(new ScreenSize(1280, 720));
            mResolutions.Add(new ScreenSize(1024, 768));
            mResolutions.Add(new ScreenSize(800, 600));
            return mResolutions;
        }

        private void RefreshToolbar()
        {
            if (mMenuBar != null)
            {
                mMenuBar.Delete();
                mMenuBar = null;
            }
            mMenuBar = CodeRush.Menus.Bars.Add("SizeBar");
            AddScreensToDropDown(DefaultResolutions());
        }
        private void AddScreensToDropDown(List<ScreenSize> Screens)
        {
            var DropDown = mMenuBar.CreateAndAddDropDownButton("Sizes");
            foreach (ScreenSize Screen in Screens)
            {
                var Button = DropDown.CreateAndAddButton(String.Format("({0} x {1})", Screen.Width, Screen.Height));
                Button.Click += Screen.ChangeSize_Click;
            }
        }

        //private void PlugIn1_OptionsChanged(OptionsChangedEventArgs ea)
        //{
        //    if (ea.OptionsPages.Contains(typeof(Options1)))
        //        LoadSettings();
        //}
        //private void LoadSettings()
        //{
        //    using (DecoupledStorage storage = Options1.Storage)
        //    {
        //        mResolutions = Options1.GetResolutions(storage);
        //    }
        //}
    }
}
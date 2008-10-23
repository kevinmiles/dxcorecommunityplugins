using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Collections.Generic;
using DevExpress.Refactor.Core;
using System.Diagnostics;

namespace CR_NavigationContrib
{
    public partial class NavigationContribPlugIn : StandardPlugIn
    {
        private Implementors _implementors;

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            _implementors = new Implementors();
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

        private void navigationProvider1_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            _implementors.Load(ea.Element);
            ea.Available = _implementors.Count > 0;
            if (!ea.Available)
                return;

            BuildMenu(ea);
        }

        private void navigationProvider1_Navigate(object sender, DevExpress.CodeRush.Library.NavigationEventArgs ea)
        {
            SubMenuItem selectedMenu = ea.SelectedSubMenuItem;
            if (selectedMenu == null)
                return;

            Class implementor = _implementors.Where(c => c.FullName == selectedMenu.Name).FirstOrDefault();
            if (implementor != null)
            {
                Navigator navigator = new Navigator(implementor, ea.Element);
                navigator.Navigate();
            }
        }

        private void BuildMenu(CheckContentAvailabilityEventArgs ea)
        {
            var classes = _implementors.OrderBy(c => c.Name);
            foreach (var item in classes)
            {
                ea.AddSubMenuItem(item.FullName, item.Name);
            }
        }
    }
}
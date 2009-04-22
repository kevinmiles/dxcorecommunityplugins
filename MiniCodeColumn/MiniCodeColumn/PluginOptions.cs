using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace MiniCodeColumn
{
    [UserLevel(UserLevel.NewUser)]
    public partial class PluginOptions : OptionsPage
    {
        // DXCore-generated code...
        #region Initialize
        protected override void Initialize()
        {
            base.Initialize();

            //
            // TODO: Add your initialization code here.
            //
        }
        #endregion

        #region GetCategory
        public static string GetCategory()
        {
            return @"Editor\Painting";
        }
        #endregion
        #region GetPageName
        public static string GetPageName()
        {
            return @"Mini Code Column";
        }
        #endregion

        #region chkEnabled_CheckedChanged
        private void chkEnabled_CheckedChanged(object sender, System.EventArgs e)
        {
            // UpdateUI();
        }
        #endregion
    }
}
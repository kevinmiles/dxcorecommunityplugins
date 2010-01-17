namespace Refactor_SplitStringContrib
{
    using System;
    using System.Windows.Forms;
    using DevExpress.CodeRush.Core;

    [UserLevel(UserLevel.Advanced)]
    public partial class SplitStringOptions : OptionsPage
    {
        // DXCore-generated code...
        #region GetCategory
        public static string GetCategory()
        {
            return @"Editor\Refactoring";
        }
        #endregion
        #region GetPageName
        public static string GetPageName()
        {
            return @"Split String";
        }
        #endregion

        #region Initialize
        protected override void Initialize()
        {
            base.Initialize();
        }

        #endregion

        private void SplitStringOptions_CommitChanges(object sender, CommitChangesEventArgs ea)
        {
            ea.Storage.WriteBoolean("Settings", "SmartEnterSplitString", this.cbSmartEnterSplitString.Checked);
            ea.Storage.WriteBoolean("Settings", "LeaveConcatenationOperatorAtTheEndOfLine", this.rbLeaveOperator.Checked);
            ea.Storage.WriteBoolean("Settings", "UseAmpersandInVb", this.cbUseAmpersand.Checked);
        }

        private void SplitStringOptions_PreparePage(object sender, OptionsPageStorageEventArgs ea)
        {
            this.cbUseAmpersand.Checked = ea.Storage.ReadBoolean("Settings", "UseAmpersandInVb", true);
            this.cbSmartEnterSplitString.Checked = ea.Storage.ReadBoolean("Settings", "SmartEnterSplitString", true);
            bool leaveOperatorAtTheEndOfFirstLine = ea.Storage.ReadBoolean("Settings", "LeaveConcatenationOperatorAtTheEndOfLine", false);
            if (leaveOperatorAtTheEndOfFirstLine)
            {
                this.rbLeaveOperator.Checked = true;
            }
            else
            {
                this.rbMoveOperatorToNextLine.Checked = true;
            }
            this.rbLeaveOperator.Enabled = this.cbSmartEnterSplitString.Checked;
            this.rbMoveOperatorToNextLine.Enabled = this.cbSmartEnterSplitString.Checked;
            this.label1.Enabled = this.cbSmartEnterSplitString.Checked;
        }

        private void SplitStringOptions_RestoreDefaults(object sender, OptionsPageEventArgs ea)
        {
            this.cbUseAmpersand.Checked = true;
            this.cbSmartEnterSplitString.Checked = true;
            this.rbMoveOperatorToNextLine.Checked = true;
        }

        private void SmartEnterSplitString_CheckedChanged(object sender, EventArgs e)
        {
            this.rbLeaveOperator.Enabled = this.cbSmartEnterSplitString.Checked;
            this.rbMoveOperatorToNextLine.Enabled = this.cbSmartEnterSplitString.Checked;
            this.label1.Enabled = this.cbSmartEnterSplitString.Checked;
        }
    }
}
namespace Refactor_SplitStringContrib
{
    using System;
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
        }

        private void SplitStringOptions_PreparePage(object sender, OptionsPageStorageEventArgs ea)
        {
            this.cbSmartEnterSplitString.Checked = ea.Storage.ReadBoolean("Settings", "SmartEnterSplitString", true);
        }

        private void SplitStringOptions_RestoreDefaults(object sender, OptionsPageEventArgs ea)
        {
            this.cbSmartEnterSplitString.Checked = true;
        }
    }
}
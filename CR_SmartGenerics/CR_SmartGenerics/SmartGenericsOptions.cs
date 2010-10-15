namespace CR_SmartGenerics
{
    using System;
    using DevExpress.CodeRush.Core;

    [UserLevel(UserLevel.Advanced)]
    public partial class SmartGenericsOptions : OptionsPage
    {
        public static string GetCategory()
        {
            return @"Editor\Auto Complete";
        }

        public static string GetPageName()
        {
            return @"Generics";
        }

        private void SmartGenerics_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = this.cbSmartGenerics.Checked;
            this.cbGenericsAddSpaces.Enabled = enabled && this.cbGenericsAutoComplete.Checked;
            this.cbGenericsAutoComplete.Enabled = enabled;
            this.cbGenericsEasyDelete.Enabled = enabled;
            this.cbGenericsIgnoreClosingOperator.Enabled = enabled;
            this.cbGenericsUseTextFields.Enabled = enabled && this.cbGenericsAutoComplete.Checked;
        }

        private void GenericsAutoComplete_CheckedChanged(object sender, EventArgs e)
        {
            this.cbGenericsAddSpaces.Enabled = this.cbGenericsAutoComplete.Checked;
            this.cbGenericsUseTextFields.Enabled = this.cbGenericsAutoComplete.Checked;
        }

        private void SmartGenericsOptions_CommitChanges(object sender, CommitChangesEventArgs ea)
        {
            ea.Storage.WriteBoolean("Settings", "UseSmartGenerics", this.cbSmartGenerics.Checked);
            ea.Storage.WriteBoolean("Settings", "SmartGenericsAutoComplete", this.cbGenericsAutoComplete.Checked);
            ea.Storage.WriteBoolean("Settings", "SmartGenericsUseTextFields", this.cbGenericsUseTextFields.Checked);
            ea.Storage.WriteBoolean("Settings", "SmartGenericsAddSpace", this.cbGenericsAddSpaces.Checked);
            ea.Storage.WriteBoolean("Settings", "SmartGenericsEasyDelete", this.cbGenericsEasyDelete.Checked);
            ea.Storage.WriteBoolean("Settings", "SmartGenericsIgnoreClosingOperator", this.cbGenericsIgnoreClosingOperator.Checked);
        }

        private void SmartGenericsOptions_PreparePage(object sender, OptionsPageStorageEventArgs ea)
        {
            this.cbSmartGenerics.Checked = ea.Storage.ReadBoolean("Settings", "UseSmartGenerics", true);
            this.cbGenericsAutoComplete.Checked = ea.Storage.ReadBoolean("Settings", "SmartGenericsAutoComplete", true);
            this.cbGenericsUseTextFields.Checked = ea.Storage.ReadBoolean("Settings", "SmartGenericsUseTextFields", true);
            this.cbGenericsAddSpaces.Checked = ea.Storage.ReadBoolean("Settings", "SmartGenericsAddSpace", false);
            this.cbGenericsEasyDelete.Checked = ea.Storage.ReadBoolean("Settings", "SmartGenericsEasyDelete", true);
            this.cbGenericsIgnoreClosingOperator.Checked = ea.Storage.ReadBoolean("Settings", "SmartGenericsIgnoreClosingOperator", true);
        }

        private void SmartGenericsOptions_RestoreDefaults(object sender, OptionsPageEventArgs ea)
        {
            this.cbSmartGenerics.Checked = true;
            this.cbGenericsAutoComplete.Checked = true;
            this.cbGenericsUseTextFields.Checked = true;
            this.cbGenericsAddSpaces.Checked = false;
            this.cbGenericsEasyDelete.Checked = true;
            this.cbGenericsIgnoreClosingOperator.Checked = true;
        }
    }
}
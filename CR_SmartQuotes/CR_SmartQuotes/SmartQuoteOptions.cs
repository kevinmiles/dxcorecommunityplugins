namespace CR_SmartQuotes
{
    using System;
    using DevExpress.CodeRush.Core;

    [UserLevel(UserLevel.Advanced)]
    public partial class SmartQuoteOptions : OptionsPage
    {
        #region GetCategory
        public static string GetCategory()
        {
            return @"Editor\Auto Complete";
        }
        #endregion
        #region GetPageName
        public static string GetPageName()
        {
            return @"Quotes & Double Quotes";
        }
        #endregion

        // DXCore-generated code...
        #region Initialize
        protected override void Initialize()
        {
            base.Initialize();
        }
        #endregion

        private void SmartQuoteOptions_CommitChanges(object sender, CommitChangesEventArgs ea)
        {
            ea.Storage.WriteBoolean("Settings", "UseSmartDoubleQuotes", this.cbSmartDoubleQuotes.Checked);
            ea.Storage.WriteBoolean("Settings", "DoubleQuotesAutoComplete", this.cbDoubleQuotesAutoComplete.Checked);
            ea.Storage.WriteBoolean("Settings", "DoubleQuotesUseTextFields", this.cbDoubleQuotesUseTextFields.Checked);
            ea.Storage.WriteBoolean("Settings", "DoubleQuotesEasyDelete", this.cbDoubleQuotesEasyDelete.Checked);
            ea.Storage.WriteBoolean("Settings", "DoubleQuotesIgnoreClosingQuote", this.cbDoubleQuotesIgnoreClosingQuote.Checked);
            ea.Storage.WriteBoolean("Settings", "UseSmartQuotes", this.cbSmartQuotes.Checked);
            ea.Storage.WriteBoolean("Settings", "QuotesAutoComplete", this.cbQuotesAutoComplete.Checked);
            ea.Storage.WriteBoolean("Settings", "QuotesUseTextFields", this.cbQuotesUseTextFields.Checked);
            ea.Storage.WriteBoolean("Settings", "QuotesEasyDelete", this.cbQuotesEasyDelete.Checked);
            ea.Storage.WriteBoolean("Settings", "QuotesIgnoreClosingQuote", this.cbQuotesIgnoreClosingQuote.Checked);
        }

        private void SmartQuoteOptions_PreparePage(object sender, OptionsPageStorageEventArgs ea)
        {
            this.cbSmartDoubleQuotes.Checked = ea.Storage.ReadBoolean("Settings", "UseSmartDoubleQuotes", true);
            this.cbDoubleQuotesAutoComplete.Checked = ea.Storage.ReadBoolean("Settings", "DoubleQuotesAutoComplete", true);
            this.cbDoubleQuotesUseTextFields.Checked = ea.Storage.ReadBoolean("Settings", "DoubleQuotesUseTextFields", true);
            this.cbDoubleQuotesEasyDelete.Checked = ea.Storage.ReadBoolean("Settings", "DoubleQuotesEasyDelete", true);
            this.cbDoubleQuotesIgnoreClosingQuote.Checked = ea.Storage.ReadBoolean("Settings", "DoubleQuotesIgnoreClosingQuote", true);
            this.cbSmartQuotes.Checked = ea.Storage.ReadBoolean("Settings", "UseSmartQuotes", true);
            this.cbQuotesAutoComplete.Checked = ea.Storage.ReadBoolean("Settings", "QuotesAutoComplete", true);
            this.cbQuotesUseTextFields.Checked = ea.Storage.ReadBoolean("Settings", "QuotesUseTextFields", true);
            this.cbQuotesEasyDelete.Checked = ea.Storage.ReadBoolean("Settings", "QuotesEasyDelete", true);
            this.cbQuotesIgnoreClosingQuote.Checked = ea.Storage.ReadBoolean("Settings", "QuotesIgnoreClosingQuote", true);
        }

        private void SmartQuoteOptions_RestoreDefaults(object sender, OptionsPageEventArgs ea)
        {
            this.cbSmartDoubleQuotes.Checked = true;
            this.cbDoubleQuotesAutoComplete.Checked = true;
            this.cbDoubleQuotesUseTextFields.Checked = true;
            this.cbDoubleQuotesEasyDelete.Checked = true;
            this.cbDoubleQuotesIgnoreClosingQuote.Checked = true;
            this.cbSmartQuotes.Checked = true;
            this.cbQuotesAutoComplete.Checked = true;
            this.cbQuotesUseTextFields.Checked = true;
            this.cbQuotesEasyDelete.Checked = true;
            this.cbQuotesIgnoreClosingQuote.Checked = true;
        }

        private void SmartDoubleQuotes_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = this.cbSmartDoubleQuotes.Checked;
            this.cbDoubleQuotesAutoComplete.Enabled = enabled;
            this.cbDoubleQuotesEasyDelete.Enabled = enabled;
            this.cbDoubleQuotesIgnoreClosingQuote.Enabled = enabled;
            this.cbDoubleQuotesUseTextFields.Enabled = enabled && this.cbDoubleQuotesAutoComplete.Checked;
        }

        private void DoubleQuotesAutoComplete_CheckedChanged(object sender, EventArgs e)
        {
            this.cbDoubleQuotesUseTextFields.Enabled = this.cbSmartDoubleQuotes.Checked && this.cbDoubleQuotesAutoComplete.Checked;
        }

        private void SmartQuotes_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = this.cbSmartQuotes.Checked;
            this.cbQuotesAutoComplete.Enabled = enabled;
            this.cbQuotesEasyDelete.Enabled = enabled;
            this.cbQuotesIgnoreClosingQuote.Enabled = enabled;
            this.cbQuotesUseTextFields.Enabled = enabled && this.cbQuotesAutoComplete.Checked;
        }

        private void QuotesAutoComplete_CheckedChanged(object sender, EventArgs e)
        {
            this.cbQuotesUseTextFields.Enabled = this.cbSmartQuotes.Checked && this.cbQuotesAutoComplete.Checked;
        }
    }
}
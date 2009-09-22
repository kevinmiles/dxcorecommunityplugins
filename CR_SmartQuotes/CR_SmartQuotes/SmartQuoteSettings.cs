namespace CR_SmartQuotes
{
    using System;
    using DevExpress.CodeRush.Core;

    internal class SmartQuoteSettings
    {
        public bool UseSmartDoubleQuotes { get; set; }

        public bool DoubleQuotesAutoComplete { get; set; }

        public bool DoubleQuotesUseTextFields { get; set; }

        public bool DoubleQuotesEasyDelete { get; set; }

        public bool DoubleQuotesIgnoreClosingQuote { get; set; }

        public bool UseSmartQuotes { get; set; }

        public bool QuotesAutoComplete { get; set; }

        public bool QuotesUseTextFields { get; set; }

        public bool QuotesEasyDelete { get; set; }

        public bool QuotesIgnoreClosingQuote { get; set; }

        public void Load()
        {
            using (DecoupledStorage storage = SmartQuoteOptions.Storage)
            {
                this.UseSmartDoubleQuotes = storage.ReadBoolean("Settings", "UseSmartDoubleQuotes", true);
                this.DoubleQuotesAutoComplete = storage.ReadBoolean("Settings", "DoubleQuotesAutoComplete", true);
                this.DoubleQuotesUseTextFields = storage.ReadBoolean("Settings", "DoubleQuotesUseTextFields", true);
                this.DoubleQuotesEasyDelete = storage.ReadBoolean("Settings", "DoubleQuotesEasyDelete", true);
                this.DoubleQuotesIgnoreClosingQuote = storage.ReadBoolean("Settings", "DoubleQuotesIgnoreClosingQuote", true);
                this.UseSmartQuotes = storage.ReadBoolean("Settings", "UseSmartQuotes", true);
                this.QuotesAutoComplete = storage.ReadBoolean("Settings", "QuotesAutoComplete", true);
                this.QuotesUseTextFields = storage.ReadBoolean("Settings", "QuotesUseTextFields", true);
                this.QuotesEasyDelete = storage.ReadBoolean("Settings", "QuotesEasyDelete", true);
                this.QuotesIgnoreClosingQuote = storage.ReadBoolean("Settings", "QuotesIgnoreClosingQuote", true);
            }
        }
    }
}

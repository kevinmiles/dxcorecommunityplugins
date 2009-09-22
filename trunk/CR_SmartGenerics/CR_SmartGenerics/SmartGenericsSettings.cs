namespace CR_SmartGenerics
{
    using System;
    using DevExpress.CodeRush.Core;

    internal class SmartGenericsSettings
    {
        public bool UseSmartGenerics { get; set; }

        public bool SmartGenericsAutoComplete { get; set; }

        public bool SmartGenericsUseTextFields { get; set; }

        public bool SmartGenericsAddSpace { get; set; }

        public bool SmartGenericsEasyDelete { get; set; }

        public bool SmartGenericsIgnoreClosingGeneric { get; set; }

        public void Load()
        {
            using (DecoupledStorage storage = SmartGenericsOptions.Storage)
            {
                this.UseSmartGenerics = storage.ReadBoolean("Settings", "UseSmartGenerics", true);
                this.SmartGenericsAutoComplete = storage.ReadBoolean("Settings", "SmartGenericsAutoComplete", true);
                this.SmartGenericsUseTextFields = storage.ReadBoolean("Settings", "SmartGenericsUseTextFields", true);
                this.SmartGenericsAddSpace = storage.ReadBoolean("Settings", "SmartGenericsAddSpace", false);
                this.SmartGenericsEasyDelete = storage.ReadBoolean("Settings", "SmartGenericsEasyDelete", true);
                this.SmartGenericsIgnoreClosingGeneric = storage.ReadBoolean("Settings", "SmartGenericsIgnoreClosingOperator", true);
            }
        }
    }
}

namespace Refactor_SplitStringContrib
{
    using System;
    using DevExpress.CodeRush.Core;

    internal class SplitStringSettings
    {
        public bool SmartEnterSplitString { get; set; }

        public bool LeaveConcatenationOperatorAtTheEndOfLine { get; set; }
        
        public bool UseAmpersandInVb { get; set; }

        public void Load()
        {
            using (DecoupledStorage storage = SplitStringOptions.Storage)
            {
                this.SmartEnterSplitString = storage.ReadBoolean("Settings", "SmartEnterSplitString", true);
                this.LeaveConcatenationOperatorAtTheEndOfLine = storage.ReadBoolean("Settings", "LeaveConcatenationOperatorAtTheEndOfLine", true);
                this.UseAmpersandInVb = storage.ReadBoolean("Settings", "UseAmpersandInVb", true);
            }
        }
    }
}

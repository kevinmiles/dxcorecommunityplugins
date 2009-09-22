namespace Refactor_SplitStringContrib
{
    using System;
    using DevExpress.CodeRush.Core;

    internal class SplitStringSettings
    {
        public bool SmartEnterSplitString { get; set; }

        public void Load()
        {
            using (DecoupledStorage storage = SplitStringOptions.Storage)
            {
                this.SmartEnterSplitString = storage.ReadBoolean("Settings", "SmartEnterSplitString", true);
            }
        }
    }
}

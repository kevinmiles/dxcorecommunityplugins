using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CR_BlockPainterPlus
{
    internal sealed class BlockPainterPlusSetings : IBlockPainterPlusSettings
    {
        public Dictionary<string, IBlockPaintingStrategySettings> StrategySettingCatalog
        {
            get;
            set;
        }
    }
}

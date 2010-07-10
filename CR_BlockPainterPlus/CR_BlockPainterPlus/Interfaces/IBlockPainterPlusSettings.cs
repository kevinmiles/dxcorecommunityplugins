using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CR_BlockPainterPlus
{
    public interface IBlockPainterPlusSettings
    {
        Dictionary<string, IBlockPaintingStrategySettings> StrategySettingCatalog { get; set; }
    }
}

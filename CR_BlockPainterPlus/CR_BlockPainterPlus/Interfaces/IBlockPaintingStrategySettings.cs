using System;
using System.Drawing;

namespace CR_BlockPainterPlus
{
    public interface IBlockPaintingStrategySettings
    {
        string BlockTypeName { get; }

        bool Enabled { get; set; }
        bool ShowDetailedBlockMetaData { get; set; }
        int MinimumBlockSize { get; set; }

        byte PrefixRed { get; set; }
        byte PrefixGreen { get; set; }
        byte PrefixBlue { get; set; }
        byte PrefixAlpha { get; set; }

        Color PrefixColor { get; set; }

        String PrefixText { get; set; }

        byte BlockMetaDataRed { get; set; }
        byte BlockMetaDataGreen { get; set; }
        byte BlockMetaDataBlue { get; set; }
        byte BlockMetaDataAlpha { get; set; }

        Color BlockMetaColor { get; set; }

    }
}

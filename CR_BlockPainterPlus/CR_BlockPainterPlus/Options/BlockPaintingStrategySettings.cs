using System;
using System.Drawing;

namespace CR_BlockPainterPlus
{
    internal sealed class BlockPaintingStrategySettings : IBlockPaintingStrategySettings
    {
        /// <summary>
        /// Initializes a new instance of the BlockPaintingStrategySettings class.
        /// </summary>
        public BlockPaintingStrategySettings(string blockTypeName)
        {
            BlockTypeName = blockTypeName;
        }

        public string BlockTypeName { get; private set; }

        public bool Enabled { get; set; }
        public bool ShowDetailedBlockMetaData { get; set; }
        public int MinimumBlockSize { get; set; }

        public byte PrefixRed { get; set; }
        public byte PrefixGreen { get; set; }
        public byte PrefixBlue { get; set; }
        public byte PrefixAlpha { get; set; }
        public string PrefixText { get; set; }

        public byte BlockMetaDataRed { get; set; }
        public byte BlockMetaDataGreen { get; set; }
        public byte BlockMetaDataBlue { get; set; }
        public byte BlockMetaDataAlpha { get; set; }


        public Color PrefixColor
        {
            get { return Color.FromArgb(PrefixAlpha, PrefixRed, PrefixGreen, PrefixBlue); }
            set
            {
                if (value != null)
                {
                    PrefixAlpha = value.A;
                    PrefixBlue = value.B;
                    PrefixGreen = value.G;
                    PrefixRed = value.R;
                }

            }
        }

        public Color BlockMetaColor
        {
            get { return Color.FromArgb(BlockMetaDataAlpha, BlockMetaDataRed, BlockMetaDataGreen, BlockMetaDataBlue); }
            set 
            {
                if (value != null)
                {
                    BlockMetaDataAlpha = value.A;
                    BlockMetaDataBlue = value.B;
                    BlockMetaDataGreen = value.G;
                    BlockMetaDataRed = value.R;
                }
            }
        }



    }
}

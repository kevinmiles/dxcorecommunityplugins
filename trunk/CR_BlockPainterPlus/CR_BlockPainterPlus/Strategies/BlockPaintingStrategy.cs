using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;
using Ninject;

namespace CR_BlockPainterPlus
{
    internal abstract class BlockPaintingStrategy : IBlockPaintingStrategy
    {
        [Inject()]
        public IBlockPaintingStrategySettings Settings { protected get; set; }

        [Inject()]
        public IDetailedBlockMetaData DetailedBlockMetaData { protected get; set; }

        [Inject()]
        public IKernel Kernel { protected get; set; }

        public abstract string BlockTypeName { get; }

        public virtual SourcePoint PaintPrefix(DelimiterCapableBlock block, DecorateLanguageElementEventArgs args, SourcePoint startPointToPaint)
        {
            SourcePoint result = startPointToPaint;
            if (Settings.Enabled && MeetsLengthRquirement(block)) 
            {
                args.AddForegroundAdornment( new BlockPrefixDocumentAdornment(startPointToPaint, Settings));

                result = startPointToPaint.OffsetPoint(0, Settings.PrefixText.Length + 1);
            }
            return result;
        }

        public virtual SourcePoint PaintBlock(DelimiterCapableBlock block, DecorateLanguageElementEventArgs args, SourcePoint startPointToPaint, bool multipleBlocksOnLine)
        {
            SourcePoint result = startPointToPaint;
            if (Settings.Enabled && MeetsLengthRquirement(block))
            {
                StringBuilder metaDataBuilder = new StringBuilder();
                metaDataBuilder.Append(Settings.BlockTypeName);

                if(Settings.ShowDetailedBlockMetaData)
                {
                    DetailedBlockMetaData.AppendDetailedBlockMetaData(block,multipleBlocksOnLine,metaDataBuilder);
                }

                args.AddForegroundAdornment( new BlockMetaDataDocumentAdornment(startPointToPaint, Settings, metaDataBuilder.ToString()));

                result = new SourcePoint(block.EndLine,  startPointToPaint.Offset + Settings.BlockTypeName.Length);
            }

            return result;
        }

        protected bool MeetsLengthRquirement(DelimiterCapableBlock block)
        {
            return block.EndLine - block.StartLine >= Settings.MinimumBlockSize;
        }

    }
}

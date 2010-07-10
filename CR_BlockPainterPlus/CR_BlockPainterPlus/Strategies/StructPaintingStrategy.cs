using System;
using DevExpress.CodeRush.StructuralParser;
using Ninject;
using DevExpress.CodeRush.Core;

namespace CR_BlockPainterPlus
{
    internal sealed class StructPaintingStrategy : BlockPaintingStrategy
    {
        public override string BlockTypeName
        {
            get { return typeof(Struct).Name; }
        }

        public override SourcePoint PaintBlock(DelimiterCapableBlock block, DecorateLanguageElementEventArgs args, SourcePoint startPointToPaint, bool multipleBlocksOnLine)
        {
            SourcePoint result = startPointToPaint;
            if (Settings.Enabled && MeetsLengthRquirement(block))
            {
                string customMetaString = String.Format("{0} '{1}'", Settings.BlockTypeName, (block as Struct).Name);
                
                args.AddForegroundAdornment(
                    new BlockMetaDataDocumentAdornment(startPointToPaint, Settings, customMetaString));

                result.OffsetPoint(0, customMetaString.Length);
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.StructuralParser;
using Ninject;
using DevExpress.CodeRush.Core;

namespace CR_BlockPainterPlus
{
    internal sealed class NamespacePaintingStrategy: BlockPaintingStrategy
    {
        public override string BlockTypeName
        {
            get { return typeof(Namespace).Name; }
        }

        public override SourcePoint PaintBlock(DelimiterCapableBlock block, DecorateLanguageElementEventArgs args, SourcePoint startPointToPaint, bool multipleBlocksOnLine)
        {
            SourcePoint result = startPointToPaint;
            if (Settings.Enabled && MeetsLengthRquirement(block))
            {
                string customMetaString = String.Format("{0} '{1}'", Settings.BlockTypeName, (block as Namespace).Name);

                args.AddForegroundAdornment(
                    new BlockMetaDataDocumentAdornment(startPointToPaint, Settings, customMetaString));

                result = startPointToPaint.OffsetPoint(0, customMetaString.Length);
            }
            return result;
        }
    }
}

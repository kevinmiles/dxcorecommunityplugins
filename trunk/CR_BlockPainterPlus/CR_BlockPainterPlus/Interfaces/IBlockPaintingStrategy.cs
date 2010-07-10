using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;

namespace CR_BlockPainterPlus
{
    public interface IBlockPaintingStrategy
    {
        string BlockTypeName { get; }

        SourcePoint PaintPrefix(DelimiterCapableBlock block, DecorateLanguageElementEventArgs args,SourcePoint startPointToPaint);
        SourcePoint PaintBlock(DelimiterCapableBlock block, DecorateLanguageElementEventArgs args, SourcePoint startPointToPaint,bool multipleBlocksOnLine);
    }
}

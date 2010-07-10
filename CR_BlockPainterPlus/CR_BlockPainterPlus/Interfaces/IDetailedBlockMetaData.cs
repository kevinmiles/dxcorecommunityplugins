using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.StructuralParser;

namespace CR_BlockPainterPlus
{
    public interface IDetailedBlockMetaData
    {
        void AppendDetailedBlockMetaData(DelimiterCapableBlock block, bool multipleBlocksOnLine, StringBuilder metaDataBuilder);
    }
}

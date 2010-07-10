using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core;

namespace CR_BlockPainterPlus
{
    internal class DetailedBlockMetaData : IDetailedBlockMetaData
    {
        public void AppendDetailedBlockMetaData(DelimiterCapableBlock block, bool multipleBlocksOnLine, StringBuilder metaDataBuilder)
        {
            if (!multipleBlocksOnLine && block.DetailNodeCount > 0)
            {
                LanguageElement firstDetailElement = block.DetailNodes[0] as LanguageElement;
                LanguageElement lastDetailElement = block.DetailNodes[block.DetailNodes.Count - 1] as LanguageElement;

                if (firstDetailElement != null && lastDetailElement != null)
                {
                    string detailText = CodeRush.TextViews.Active.TextDocument.GetText(
                        firstDetailElement.StartLine,
                        firstDetailElement.StartOffset,
                        lastDetailElement.EndLine,
                        lastDetailElement.EndOffset);

                    metaDataBuilder.Append(Words.OpenParen);
                    metaDataBuilder.Append(detailText);
                    metaDataBuilder.Append(Words.CloseParen);
                }
            }
        }
    }
}

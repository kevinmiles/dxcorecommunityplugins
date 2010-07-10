using System;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.DXCore.Adornments;

namespace CR_BlockPainterPlus
{
    internal sealed class BlockPrefixDocumentAdornment : TextDocumentAdornment
    {
        private readonly IBlockPaintingStrategySettings _settings;

        public BlockPrefixDocumentAdornment(SourcePoint point, IBlockPaintingStrategySettings settings)
            : base(point)
        {
            _settings = settings;
        }

        protected override TextViewAdornment NewAdornment(string feature, IElementFrame frame)
        {
            return new BlockPrefixViewAdornment(feature, frame, _settings);
        }
    }
}

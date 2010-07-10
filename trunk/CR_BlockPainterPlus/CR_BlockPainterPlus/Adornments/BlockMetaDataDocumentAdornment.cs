using System;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.DXCore.Adornments;

namespace CR_BlockPainterPlus
{
    class BlockMetaDataDocumentAdornment : TextDocumentAdornment
    {
        private readonly IBlockPaintingStrategySettings _settings;
        private readonly string _customMetaString = String.Empty;

        public BlockMetaDataDocumentAdornment(SourcePoint sourcePoint, IBlockPaintingStrategySettings settings, string customMetaString)
            : base(sourcePoint)
        {
            _settings = settings;
            _customMetaString = customMetaString;
        }

        protected override TextViewAdornment NewAdornment(string feature, IElementFrame frame)
        {
            return new BlockMetaDataViewAdornment(feature, frame, _settings, _customMetaString);
        }
    }
}

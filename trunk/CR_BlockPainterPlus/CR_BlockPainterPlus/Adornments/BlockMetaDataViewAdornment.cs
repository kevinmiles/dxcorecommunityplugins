using System;
using DevExpress.CodeRush.Core;
using DevExpress.DXCore.Adornments;
using DevExpress.DXCore.Platform.Drawing;

namespace CR_BlockPainterPlus
{
    internal sealed class BlockMetaDataViewAdornment : VisualObjectAdornment
    {
        private readonly IBlockPaintingStrategySettings _settings;
        private readonly string _customMetaString = String.Empty;

        public BlockMetaDataViewAdornment(string feature, IElementFrame frame, IBlockPaintingStrategySettings settings, string customMetaString)
            : base(feature, frame)
        {
            _settings = settings;
            _customMetaString = customMetaString;
        }

        public override void Render(IDrawingSurface context, ElementFrameGeometry geometry)
        {
            ColorProperty metaColor = new ColorProperty(
                Color.FromArgb(_settings.BlockMetaDataAlpha, _settings.BlockMetaDataRed, _settings.BlockMetaDataGreen, _settings.BlockMetaDataBlue));

            float fontSize = CodeRush.VSSettings.FontSize;

            context.DrawString (_customMetaString, CodeRush.VSSettings.FontName, fontSize , metaColor, geometry.Location.MoveDown(2));
        }
    }
}
using System;
using System.Collections.Generic;

using DevExpress.DXCore.Adornments;
using DevExpress.DXCore.Platform.Drawing;

using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;

namespace CR_BlockPainterPlus
{
    class BlockPrefixViewAdornment : VisualObjectAdornment
    {
        private IBlockPaintingStrategySettings _settings;


        public BlockPrefixViewAdornment(string feature, IElementFrame frame, IBlockPaintingStrategySettings settings)
            : base(feature,frame)
        {
            _settings = settings;
        }

        public override void Render(IDrawingSurface context, ElementFrameGeometry geometry)
        {
            ColorProperty prefixColor = new ColorProperty(
                Color.FromArgb(_settings.PrefixAlpha, _settings.PrefixRed, _settings.PrefixGreen, _settings.PrefixBlue));

            context.DrawString(_settings.PrefixText, CodeRush.VSSettings.FontName,CodeRush.VSSettings.FontSize,prefixColor,geometry.Location);
        }
    }
}
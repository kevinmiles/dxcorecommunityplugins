using System;
using System.Collections.Generic;

using DevExpress.DXCore.Adornments;
using DevExpress.DXCore.Platform.Drawing;

using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using System.Diagnostics;

namespace CR_DrawLinesBetweenMethods
{
    class HorizontalLineDocAdornment : TextDocumentAdornment
    {
        public HorizontalLineDocAdornment(SourceRange range)
            : base(range)
        {
        }

        protected override TextViewAdornment NewAdornment(string feature, IElementFrame frame)
        {
            var adornment = new HorizontalLineViewAdornment(feature, frame);
            return adornment;
        }
    }

    class HorizontalLineViewAdornment : VisualObjectAdornment
    {
        public HorizontalLineViewAdornment(string feature, IElementFrame frame)
            : base(feature, frame)
        {
        }

        public override void Render(IDrawingSurface drawingSurface, ElementFrameGeometry geometry)
        {
            //Debug.WriteLine("  HorizontalLineViewAdornment.Render");

            var settings = DrawLinesBetweenMethodsSettings.Current;

            Color lineColor = Color.ConvertFrom(settings.LineColor);
            // Top line
            if (settings.DrawLineAtStartOfMethod)
            {
                Point startPoint = geometry.StartPoint
                    .MoveUp(settings.LineSpacer);
                drawLine(drawingSurface, settings, startPoint);
            }

            // Bottom line
            if (settings.DrawLineAtEndOfMethod)
            {
                Point startPoint = geometry.EndRect.BottomLeft
                    .MoveDown(settings.LineSpacer);
                drawLine(drawingSurface, settings, startPoint);
            }

            // TODO: DrawRectangle method only draws horizontal gradients
            //const int shadowHeight = 10;
            //const int shadowAlpha = 50;
            //var shadowRect = new Rect(startPoint, new Size(lineWidth, shadowHeight));
            //Color shadowStartColor = Color.FromArgb(shadowAlpha, lineColor);
            //Color shadowEndColor = Colors.Transparent;
            //drawingSurface.DrawRectangle(shadowStartColor, shadowEndColor, Colors.Transparent, shadowRect);
            
        }

        void drawLine(IDrawingSurface drawingSurface, DrawLinesBetweenMethodsSettings settings, Point startPoint)
        {
            int lineWidth = 3000; //TODO: perhaps more scientific way of doing this?

            Point endPoint = startPoint.MoveRight(lineWidth);
            Debug.WriteLine("Line " + startPoint + " -> " + endPoint);
            drawingSurface.DrawLine(Color.ConvertFrom(settings.LineColor), startPoint, endPoint, null, settings.LineWidth);
        }
    }
}
using System;
using System.Collections.Generic;

using DevExpress.DXCore.Adornments;
using DevExpress.DXCore.Platform.Drawing;

using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using System.Diagnostics;

namespace CR_DrawLinesBetweenMethods
{
    class DrawLinesBetweenMethodsDocumentAdornment : TextDocumentAdornment
    {
        public DrawLinesBetweenMethodsDocumentAdornment(SourceRange range)
            : base(range)
        {
            //Debug.WriteLine("DrawLinesBetweenMethodsAdornmentDocumentAdornment() c'tor");
        }

        protected override TextViewAdornment NewAdornment(string feature, IElementFrame frame)
        {
            //Debug.WriteLine("  DrawLinesBetweenMethodsAdornmentDocumentAdornment.NewAdornment('" + feature + "', '" + frame + "')");
            var adornment = new DrawLinesBetweenMethodsObjectAdornment(feature, frame);
            return adornment;
        }
    }

    class DrawLinesBetweenMethodsObjectAdornment : VisualObjectAdornment
    {
        public DrawLinesBetweenMethodsObjectAdornment(string feature, IElementFrame frame)
            : base(feature, frame)
        {
            //Debug.WriteLine("DrawLinesBetweenMethodsAdornmentViewAdornment c'tor");
        }

        public override void Render(IDrawingSurface drawingSurface, ElementFrameGeometry geometry)
        {
            //Debug.WriteLine("  DrawLinesBetweenMethodsAdornmentViewAdornment.Render");

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
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Diagnostics;

namespace CR_DrawLinesBetweenMethods
{
    /// <summary>
    /// Summary description for DrawLinesBetweenMethodsPlugIn.
    /// </summary>
    public class DrawLinesBetweenMethodsPlugIn : StandardPlugIn
    {
        #region Component Designer generated code
        public DrawLinesBetweenMethodsPlugIn()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // DrawLinesBetweenMethodsPlugIn
            // 
            this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.DrawLinesBetweenMethodsPlugIn_OptionsChanged);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        // CodeRush-generated code
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            loadSettings();
        }
        #endregion
        
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion

        private void DrawLinesBetweenMethodsPlugIn_EditorPaintLanguageElement(EditorPaintLanguageElementEventArgs ea)
        {
            if ((ea.LanguageElement is Class) && _enableOnClass
                || (ea.LanguageElement is Property) && _enableOnProperty
                || (ea.LanguageElement is Method) && _enableOnMethod
                || (ea.LanguageElement is Enumeration) && _enableOnEnum)
            {
                decorate(ea);
            }
        }

        void decorate(EditorPaintLanguageElementEventArgs ea)
        {
            //Debug.WriteLine("Decorating " + ea.LanguageElement.ElementType + ": " + ea.LanguageElement);

            //Top of code block
            int leftPos = _fullWidth ? 0 : ea.PaintArgs.GetPoint(ea.LanguageElement.Range.Start).X;

            //Separate out start and end of method for independent control...
            if (_drawLineAtStartOfMethod)
            {
                int line = ea.LanguageElement.Range.Start.Line;

                // Skip up over Comment, AttributeSection, XmlDocComment
                LanguageElement prev = ea.LanguageElement.PreviousSibling;
                while (prev != null &&
                    (prev.ElementType == LanguageElementType.Comment
                    || prev.ElementType == LanguageElementType.XmlDocComment
                    || prev.ElementType == LanguageElementType.AttributeSection))
                {
                    //Debug.WriteLine("Skipping " + prev.ElementType + "  \"" + prev.ToString() + "\"");
                    line = prev.Range.Start.Line;
                    prev = prev.PreviousSibling;
                }

                Point leftPoint = new Point(leftPos, ea.PaintArgs.GetPoint(line, 1).Y - _lineSpacer);
                Point rightPoint = new Point(ea.PaintArgs.TextView.Width, leftPoint.Y);
                drawLine(ea.PaintArgs.Graphics, _lineColor, leftPoint, rightPoint, _lineWidth, _lineDashStyle, _drawShadow, _shadowHeight);
            }

            if (_drawLineAtEndOfMethod)
            {
                int line = ea.LanguageElement.Range.End.Line + 1;
                Point leftPoint = new Point(leftPos, ea.PaintArgs.GetPoint(line, 1).Y + _lineSpacer);
                Point rightPoint = new Point(ea.PaintArgs.TextView.Width, leftPoint.Y);
                drawLine(ea.PaintArgs.Graphics, _lineColor, leftPoint, rightPoint, _lineWidth, _lineDashStyle, _drawShadow, _shadowHeight);
            }
        }

        void drawLine(Graphics graphics, Color lineColor, Point startPoint, Point endPoint, int lineWidth, DashStyle lineStyle, bool shadow, int shadowHeight)
        {
            if (shadow)
            {
                Point bottomRight = endPoint;
                bottomRight.Y = endPoint.Y + shadowHeight;
                drawShadow(graphics, lineColor, startPoint, bottomRight);
            }

            using (Pen pen = new Pen(lineColor, lineWidth) { DashStyle = lineStyle })
            {
                graphics.DrawLine(pen, startPoint, endPoint);
            }
        }

        void drawShadow(Graphics graphics, Color lineColor, Point topLeft, Point bottomRight)
        {
            Color shadowColor = Color.FromArgb(0x33, lineColor);
            Rectangle rect = new Rectangle(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
            using (LinearGradientBrush brush = new LinearGradientBrush(rect, shadowColor, Color.Transparent, 90, true))
            {
                graphics.FillRectangle(brush, rect);
            }
        }


        bool _fullWidth;
        DashStyle _lineDashStyle = DashStyle.Solid;
        int _lineWidth = 1;
        Color _lineColor = Color.Silver;
        bool _drawLineAtStartOfMethod = true;
        bool _drawLineAtEndOfMethod;
        bool _drawShadow = true;
        bool _enabled = true;
        bool _enableOnClass = true;
        bool _enableOnProperty = true;
        bool _enableOnMethod = true;
        bool _enableOnEnum = true;
        int _lineSpacer;
        int _shadowHeight = 5;

        private void DrawLinesBetweenMethodsPlugIn_OptionsChanged(OptionsChangedEventArgs ea)
        {
            loadSettings();
        }

        void loadSettings()
        {
            using (DecoupledStorage storage = OptDrawLinesBetweenMethods.Storage)
            {
                _fullWidth = storage.ReadBoolean("DrawLinesBetweenMethods", "FullWidth", _fullWidth);
                _lineDashStyle = (DashStyle)storage.ReadEnum("DrawLinesBetweenMethods", "LineDashStyle", typeof(DashStyle), _lineDashStyle);
                _lineWidth = storage.ReadInt32("DrawLinesBetweenMethods", "LineWidth", _lineWidth);
                _lineColor = storage.ReadColor("DrawLinesBetweenMethods", "LineColor", _lineColor);
                _drawLineAtStartOfMethod = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawLineAtStartOfMethod", _drawLineAtStartOfMethod);
                _drawLineAtEndOfMethod = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawLineAtEndOfMethod", _drawLineAtEndOfMethod);
                _drawShadow = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawShadow", _drawShadow);
                _enableOnClass = storage.ReadBoolean("DrawLinesBetweenMethods", "EnableOnClass", _enableOnClass);
                _enableOnProperty = storage.ReadBoolean("DrawLinesBetweenMethods", "EnableOnProperty", _enableOnProperty);
                _enableOnMethod = storage.ReadBoolean("DrawLinesBetweenMethods", "EnableOnMethod", _enableOnMethod);
                _enableOnEnum = storage.ReadBoolean("DrawLinesBetweenMethods", "EnableOnEnum", _enableOnEnum);
                _enabled = storage.ReadBoolean("DrawLinesBetweenMethods", "Enabled", _enabled);
                _lineSpacer = storage.ReadInt32("DrawLinesBetweenMethods", "LineSpacer", _lineSpacer);
                _shadowHeight = storage.ReadInt32("DrawLinesBetweenMethods", "ShadowHeight", _shadowHeight);

                EditorPaintLanguageElement -= DrawLinesBetweenMethodsPlugIn_EditorPaintLanguageElement;
                if (_enabled)
                    EditorPaintLanguageElement += DrawLinesBetweenMethodsPlugIn_EditorPaintLanguageElement;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using DevExpress.CodeRush.Core;

namespace CR_DrawLinesBetweenMethods
{
    class DrawLinesBetweenMethodsSettings
    {
        public bool FullWidth { get; set; }
        public DashStyle LineDashStyle { get; set; }
        public int LineWidth { get; set; }
        public Color LineColor { get; set; }
        public bool DrawLineAtEndOfMethod { get; set; }
        public bool DrawLineAtStartOfMethod { get; set; }
        public bool EnableOnClass { get; set; }
        public bool EnableOnProperty { get; set; }
        public bool EnableOnMethod { get; set; }
        public bool EnableOnEnum { get; set; }
        public int LineSpacer { get; set; }
        public int ShadowHeight { get; set; }
        public bool Enabled { get; set; }
        public bool DrawShadow { get; set; }

        public DrawLinesBetweenMethodsSettings(DecoupledStorage storage)
        {
            FullWidth = storage.ReadBoolean("DrawLinesBetweenMethods", "FullWidth", FullWidth);
            LineDashStyle = (DashStyle)storage.ReadEnum("DrawLinesBetweenMethods", "LineDashStyle", typeof(DashStyle), LineDashStyle);
            LineWidth = storage.ReadInt32("DrawLinesBetweenMethods", "LineWidth", LineWidth);
            LineColor = storage.ReadColor("DrawLinesBetweenMethods", "LineColor", LineColor);
            DrawLineAtStartOfMethod = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawLineAtStartOfMethod", DrawLineAtStartOfMethod);
            DrawLineAtEndOfMethod = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawLineAtEndOfMethod", DrawLineAtEndOfMethod);
            DrawShadow = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawShadow", DrawShadow);
            EnableOnClass = storage.ReadBoolean("DrawLinesBetweenMethods", "EnableOnClass", EnableOnClass);
            EnableOnProperty = storage.ReadBoolean("DrawLinesBetweenMethods", "EnableOnProperty", EnableOnProperty);
            EnableOnMethod = storage.ReadBoolean("DrawLinesBetweenMethods", "EnableOnMethod", EnableOnMethod);
            EnableOnEnum = storage.ReadBoolean("DrawLinesBetweenMethods", "EnableOnEnum", EnableOnEnum);
            LineSpacer = storage.ReadInt32("DrawLinesBetweenMethods", "LineSpacer", LineSpacer);
            ShadowHeight = storage.ReadInt32("DrawLinesBetweenMethods", "ShadowHeight", ShadowHeight);
            Enabled = storage.ReadBoolean("DrawLinesBetweenMethods", "Enabled", Enabled);
        }

        static DrawLinesBetweenMethodsSettings()
        {
            EventNexus.OptionsChanged += args =>
            {
                _current = null;
            };
        }

        static DrawLinesBetweenMethodsSettings _current;
        public static DrawLinesBetweenMethodsSettings Current
        {
            get
            {
                if (_current == null)
                    _current = new DrawLinesBetweenMethodsSettings(OptDrawLinesBetweenMethods.Storage);
                return _current;
            }
        }

    }
}

using System;
using System.Drawing;
using DevExpress.CodeRush.Core;

namespace RedGreen
{
    class TestPopupMenuColors : SmartTagPopupMenuColors
    {

        public override Color BackgroundColor
        {
            get { return Color.FromArgb(0xFF, 0xF8, 0xFA, 0xF8); }
        }

        public override Color BorderBottomColor
        {
            get { return Color.FromArgb(0xFF, 0x8A, 0xB7, 0x56); }
        }

        public override Color BorderLeftInnerColor
        {
            get { return Color.FromArgb(0xFF, 0xAA, 0xDB, 0x6F); }
        }

        public override Color BorderLeftOuterColor
        {
            get { return Color.FromArgb(0xFF, 0xB5, 0xDB, 0x87); }
        }

        public override Color BorderRightInnerColor
        {
            get { return Color.FromArgb(0xFF, 0xA3, 0xD8, 0x65); }
        }

        public override Color BorderRightOuterColor
        {
            get { return Color.FromArgb(0xFF, 0x8A, 0xB7, 0x56); }
        }

        public override Color BorderTopColor
        {
            get { return Color.FromArgb(0xFF, 0xAA, 0xDB, 0x6F); }
        }

        public override Color SelectedBackgroundColor
        {
            get { return Color.FromArgb(0xFF, 0x74, 0xC1, 0x75); }
        }

        public override Color SelectedBorderColor
        {
            get { return Color.FromArgb(0xFF, 0x2F, 0x97, 0x1E); }
        }

        public override Color SelectedTextColor
        {
            get { return Color.White; }
        }

        public override Color TextColor
        {
            get { return Color.Black; }
        }

        public override Color TitleActiveBackgroundColor
        {
            get { return Color.FromArgb(0xFF, 0xEC, 0xFF, 0xEA); }
        }

        public override Color TitleActiveTextColor
        {
            get { return Color.FromArgb(0xFF, 0x74, 0xC1, 0x75); }
        }

        public override Color TitleBackgroundColor
        {
            get { return Color.FromArgb(0xFF, 0xF1, 0xFF, 0xF0); }
        }

        public override Color TitleTextColor
        {
            get { return Color.FromArgb(0xFF, 0x95, 0xB5, 0x69); }
        }
    }
}

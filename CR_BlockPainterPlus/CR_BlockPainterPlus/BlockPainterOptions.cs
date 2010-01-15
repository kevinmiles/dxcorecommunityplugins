using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_BlockPainterPlus
{
    /// <summary>
    /// 
    /// </summary>
    [UserLevel(UserLevel.NewUser)]
    public partial class BlockPainterOptions : OptionsPage
    {
        public const int DefaultOpacity = 125;
        public static readonly Color DefaultArrowColor = Color.Red;
        public static readonly Color DefaultFontColor = Color.LightBlue;

        public const string SectionName = "BlockPainter";
        // DXCore-generated code...
        public const string ArrowColor = "arrowColor";
        public const string FontColor = "fontColor";
        public const string ArrowOpacity = "arrowOpacity";
        public const string FontOpacity = "fontOpacity";
        public const string MinimumLines = "minimumLines";
        public const string MinimumLineCount = "minimumLineCount";
        public const string ShowDetailOnBlocks = "showDetailOnBlocks";

        #region Initialize
        protected override void Initialize()
        {
            base.Initialize();

            //
            // TODO: Add your initialization code here.
            //
        }
        #endregion

        #region GetCategory
        public static string GetCategory()
        {
            return @"Editor\Painting";
        }
        #endregion
        #region GetPageName
        public static string GetPageName()
        {
            return @"Block Painter";
        }
        #endregion

        private void fontColorPicture_DoubleClick(object sender, EventArgs e)
        {
            Color selectedColor = GetColor();
            if (Color.Equals(selectedColor, Color.Empty))
                return;
            SetImage(selectedColor, fontColorPicture);
        }

        private void arrowColorPicture_DoubleClick(object sender, EventArgs e)
        {
            Color selectedColor = GetColor();
            if (Color.Equals(selectedColor, Color.Empty))
                return;
            SetImage(selectedColor, arrowColorPicture);
        }

        private Color GetColor()
        {
            using (ColorDialog colorDialog = new ColorDialog())
            {
                DialogResult dialogResult = colorDialog.ShowDialog(this as IWin32Window);
                if (dialogResult == DialogResult.OK)
                    return colorDialog.Color;
                else
                    return Color.Empty;
            }
        }

        private void SetImage(Color fillColor, PictureBox pictureControl)
        {
            Bitmap bitmap = new Bitmap(153,16);
            using(Graphics graphics = System.Drawing.Graphics.FromImage(bitmap))
            using(SolidBrush brush = new SolidBrush(fillColor))
            {
                graphics.FillRectangle(brush,0,0,(float)bitmap.Width,(float)bitmap.Height);
            }

            pictureControl.Image = bitmap;
            pictureControl.Tag = fillColor;
        }

        private void BlockPainterOptions_Load(object sender, EventArgs e)
        {
            SetImage(Storage.ReadColor(SectionName, ArrowColor,DefaultArrowColor), arrowColorPicture);
            SetImage(Storage.ReadColor(SectionName, FontColor,DefaultFontColor), fontColorPicture);

            arrowTrackBar.Value = Storage.ReadInt32(SectionName, ArrowOpacity,DefaultOpacity);
            fontTrackBar.Value = Storage.ReadInt32(SectionName, FontOpacity,DefaultOpacity);
            minimumLinesCheckBox.Checked = Storage.ReadBoolean(SectionName, MinimumLines);
            lineCountSpinner.Value = Storage.ReadInt32(SectionName, MinimumLineCount,10);
            showDetailOnBlocksCheckBox.Checked = Storage.ReadBoolean(SectionName, ShowDetailOnBlocks);   
        }

        private void BlockPainterOptions_CommitChanges(object sender, CommitChangesEventArgs ea)
        {
            ea.Storage.WriteColor(SectionName, ArrowColor, (Color)arrowColorPicture.Tag);
            ea.Storage.WriteColor(SectionName, FontColor, (Color)fontColorPicture.Tag);
            ea.Storage.WriteInt32(SectionName, ArrowOpacity, arrowTrackBar.Value);
            ea.Storage.WriteInt32(SectionName, FontOpacity, fontTrackBar.Value);
            ea.Storage.WriteBoolean(SectionName, MinimumLines, minimumLinesCheckBox.Checked);
            ea.Storage.WriteInt32(SectionName, MinimumLineCount, Convert.ToInt32(lineCountSpinner.Value));
            ea.Storage.WriteBoolean(SectionName, ShowDetailOnBlocks, showDetailOnBlocksCheckBox.Checked);
        }
    }
}
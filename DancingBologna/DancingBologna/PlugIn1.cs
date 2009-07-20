using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Collections;
using System.Collections.Generic;

namespace DancingBologna
{
    public partial class PlugIn1 : StandardPlugIn
    {
        private Brush _background;
        private ImageFrames _frames;
        private TimeSpan _lastChage = TimeSpan.FromMilliseconds(0);
        private Graphics _editorGraphics;
        private Image _last;
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            _frames = new ImageFrames(this.imageList1);
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


        private void action1_Execute(ExecuteEventArgs ea)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                timer1.Stop();
                _editorGraphics.FillRectangle(_background, GetRectangle(TextView.Active));
            }
            else
            {
                timer1.Enabled = true;
                timer1.Start();
            }
        }
        private Rectangle GetRectangle(TextView activeDocument)
        {
            var top = new Point(activeDocument.Bounds.Width - imageList1.ImageSize.Width, activeDocument.Bounds.Top + 20);
            var r = new Rectangle(top, new Size(imageList1.ImageSize.Width, imageList1.ImageSize.Height));
            return r;
        }

        private void PlugIn1_EditorPaint(EditorPaintEventArgs ea)
        {
            _background = ea.BackgroundBrush;
            _editorGraphics = ea.Graphics;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var r = GetRectangle(TextView.Active);
            _editorGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            _editorGraphics.FillRectangle(_background,r);
            _editorGraphics.DrawImage(_frames.Next, r);
        }
    }

    public class ImageFrames
    {
        private ImageList _images;
        int currentIndex = 0;
        public ImageFrames(ImageList images)
        {
            _images = images;
        }

        public Image Next
        {
            get
            {
                if (currentIndex == _images.Images.Count)
                    currentIndex = 0;
                return _images.Images[currentIndex++];
            }
        }
    }

}
/*
 * Software License Agreement for RedGreen
 * 
 * Copyright (c) 2010 Renaissance Learning, Inc. and James Argeropoulos
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
using System;

using DevExpress.DXCore.Adornments;
using DevExpress.DXCore.Platform.Drawing;

using DevExpress.CodeRush.Core;
using System.Drawing;

using PlatformImage = DevExpress.DXCore.Platform.Drawing.Image;


namespace UnitTestErrorVisualizer
{
    class FailedTestInspectorAdornment : TileVisual
    {
		private static RangeArrow _ArrowToAssert;
        private ArrowDescription ErrorInformation;
        private static Bitmap _Bitmap = new Bitmap(new MyClass().GetType(), "FailedTestInspector.png");

        public FailedTestInspectorAdornment(IElementFrame binding)
            : base(binding)
        {
        }
        public override void Render(IDrawingSurface context, ElementFrameGeometry geometry)
        {
			ErrorInformation = base.Object as ArrowDescription;
			context.DrawImage(PlatformImage.ConvertFrom(_Bitmap), geometry.Bounds, 0.5);
        }
		public override void OnMouseEnter(EditorMouseEventArgs ea)
        {
            base.OnMouseEnter(ea);
			_ArrowToAssert = new ErrorDetailsRangeArrow(ErrorInformation.Start, ErrorInformation.End, ErrorInformation);
			_ArrowToAssert.Add(TextView.Active);
		}
        public override void OnMouseLeave(EditorMouseEventArgs ea)
        {
            base.OnMouseLeave(ea);
			_ArrowToAssert.Remove();
        }
    }
}
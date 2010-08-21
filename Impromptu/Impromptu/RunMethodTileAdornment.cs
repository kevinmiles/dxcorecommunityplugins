using System;/*
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

using DevExpress.DXCore.Adornments;
using DevExpress.DXCore.Platform.Drawing;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;

namespace Impromptu
{
    class RunMethodTileAdornment : TileVisual
    {
        private static System.Drawing.Bitmap _Bitmap = new System.Drawing.Bitmap(new MyClass(). GetType(), "RunMethod.png");
        public RunMethodTileAdornment(IElementFrame binding)
            : base(binding)
        {
        }

        public override void Render(IDrawingSurface context, ElementFrameGeometry geometry)
        {
            context.DrawImage(Image.ConvertFrom(_Bitmap), geometry.Bounds, 0.2);
        }
        public override void OnMouseDown(EditorMouseEventArgs ea)
        {
            Method target = Object as Method;
            if (target != null)
            {
                PlugIn1.RunMethod(target);
                ea.Cancel = true;
            }
        }
    }
}

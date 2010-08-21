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
using DevExpress.CodeRush.StructuralParser;

namespace Impromptu
{
    class RunMethodTileDocumentAdornment : TextDocumentTile
    {
        Method target;
        public RunMethodTileDocumentAdornment(DocPoint start, DocPoint end, CoreEventHub master, Method target)
            : base(start, end, master, target)
        {
            this.target = target;
        }

        protected override TextViewAdornment NewAdornment(string feature, IElementFrame binding)
        {
            return new RunMethodTileAdornment(binding) { Cursor = Cursor.Arrow };
        }
        bool IsVisibleInView(TextView view)
        {
            if (view is IGdiTextView)
                return true;
            if (target == null)
                return false;
            return !target.InCollapsedRange(view);
        }
        protected override IElementFrame CreateBinding(TextView view, DocPoint start, DocPoint end)
        {
            if (!IsVisibleInView(view))
                return null;

            return new TileElementFrame(base.CreateBinding(view, start, start)) { XOffset = -30, TileWidth = 11, TileHeight = 11 };
        }

    }
}
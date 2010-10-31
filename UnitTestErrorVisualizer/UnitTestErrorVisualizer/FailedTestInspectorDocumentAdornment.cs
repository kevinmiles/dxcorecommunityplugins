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

namespace UnitTestErrorVisualizer
{
    class FailedTestInspectorDocumentAdornment : TextDocumentTile
    {
        public FailedTestInspectorDocumentAdornment(DocPoint start, DocPoint end, CoreEventHub master, ArrowDescription arrow)
            : base(start, end, master, arrow)
        {
        }
        bool IsVisibleInView(TextView view)
        {
            if (view is IGdiTextView)
                return true;
			ArrowDescription arrow = this.Object as ArrowDescription;
			if (arrow == null)
                return false;
			return !arrow.Test.InCollapsedRange(view);
        }

		protected override TextViewAdornment NewAdornment(string feature, IElementFrame binding)
		{
			return new FailedTestInspectorAdornment(binding) { Cursor = Cursor.Arrow };
		}
		protected override IElementFrame CreateBinding(TextView view, DocPoint start, DocPoint end)
		{
			if (!IsVisibleInView(view))
				return null;

			return new TileElementFrame(base.CreateBinding(view, start, start)) { XOffset = -41, TileWidth = 16, TileHeight = 16 };
		}
    }
}

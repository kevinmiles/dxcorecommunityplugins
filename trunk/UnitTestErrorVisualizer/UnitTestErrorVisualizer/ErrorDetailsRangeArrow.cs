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
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.DXCore.Adornments;

namespace UnitTestErrorVisualizer
{
	public class ErrorDetailsRangeArrow : RangeArrow
	{
		ArrowDescription whatToDraw;
		public ErrorDetailsRangeArrow(SourceRange start, SourceRange end, ArrowDescription whatToDraw)
			: base (start, end)
		{
			this.whatToDraw = whatToDraw;
		}

		protected override CodeViewAdornment CreateCodeViewAdornment(TextView textView)
		{
			ErrorDetailsRangeArrowDocAdornment adornment = new ErrorDetailsRangeArrowDocAdornment(StartRange, EndRange, Color, whatToDraw);
			return (adornment.CreateTextViewAdornment(textView) as CodeViewAdornment);
		}
	}
}

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
using Platform = DevExpress.DXCore.Platform.Drawing;

namespace UnitTestErrorVisualizer
{
	public class EnhancedRangeArrow : RangeArrow
	{
		Action<Platform.IDrawingSurface, Platform.Rect> extraDrawing;
		public EnhancedRangeArrow(SourceRange start, SourceRange end, Action<Platform.IDrawingSurface, Platform.Rect> extraDrawing)
			: base(start, end)
		{
			this.extraDrawing = extraDrawing;
		}

		protected override CodeViewAdornment CreateCodeViewAdornment(TextView textView)
		{
			EnhancedRangeArrowDocAdornment adornment = new EnhancedRangeArrowDocAdornment(StartRange, EndRange, Color, extraDrawing);
			return (adornment.CreateTextViewAdornment(textView) as CodeViewAdornment);
		}
	}
	public class EnhancedRangeArrowDocAdornment : RangeArrowDocAdornment
	{
		Action<Platform.IDrawingSurface, Platform.Rect> extraDrawing;
		Platform.Color localCopyColor;	//The color should probably be changed to protected so I don't need this hack
		public EnhancedRangeArrowDocAdornment(SourceRange startRange, SourceRange endRange, Platform.Color color, Action<Platform.IDrawingSurface, Platform.Rect> extraDrawing)
			: base(startRange, endRange, color)
		{
			this.extraDrawing = extraDrawing;
			localCopyColor = color;
		}
		protected override TextViewAdornment NewAdornment(string feature, IElementFrame binding)
		{
			EnhancedRangeArrowAdornment adornment = new EnhancedRangeArrowAdornment(binding, extraDrawing);
			adornment.Color = localCopyColor;
			return adornment;
		}
	}
	public class EnhancedRangeArrowAdornment : RangeArrowAdornment
	{
		Action<Platform.IDrawingSurface, Platform.Rect> extraDrawing;
		public EnhancedRangeArrowAdornment(IElementFrame binding, Action<Platform.IDrawingSurface, Platform.Rect> extraDrawing)
			: base(binding)
		{
			this.extraDrawing = extraDrawing;
		}
		public override void Render(Platform.IDrawingSurface context)
		{
			base.Render(context);
			extraDrawing(context, GetViewElementFrameInfo().ElementFrameGeometry.ToLocation().Bounds);
		}
	}
}

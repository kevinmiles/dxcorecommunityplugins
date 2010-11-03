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
using System.Collections.Generic;

using DevExpress.DXCore.Adornments;
using DevExpress.DXCore.Platform.Drawing;

using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.Core.Testing;
using DevExpress.CodeRush.StructuralParser;
using Platform = DevExpress.DXCore.Platform.Drawing;

namespace UnitTestErrorVisualizer
{
	public class ShadeColors
	{
		public Color PassedColor { get; set; }
		public Color FailedColor { get; set; }
		public Color SkippedColor { get; set; }
	}
	class TestAttributeShaderAdornmentDocumentAdornment : TextDocumentAdornment
	{
		TestStatus status;
		ShadeColors colors;
		public TestAttributeShaderAdornmentDocumentAdornment(SourceRange range, TestStatus status, ShadeColors colors)
			: base(range)
		{
			this.status = status;
			this.colors = colors;
		}

		protected override TextViewAdornment NewAdornment(string feature, IElementFrame frame)
		{
			return new TestAttributeShaderAdornmentViewAdornment(feature, frame, GetColor(status, colors));
		}
		private static Platform.Color GetColor(TestStatus status, ShadeColors colors)
		{
			switch (status)
			{
				//case TestStatus.Pending:
				default:
					return Platform.Color.ConvertFrom(CodeRush.VSSettings.BackgroundColor);
				case TestStatus.Ignored:
					return colors.SkippedColor;
				case TestStatus.Passed:
					//case TestStatus.PassedWithChanges:
					return colors.PassedColor;
				case TestStatus.Failure:
					//case TestStatus.FailedWithChanges:
					return colors.FailedColor;
			}
		}
	}

	class TestAttributeShaderAdornmentViewAdornment : VisualObjectAdornment
	{
		Platform.Color shade;
		public TestAttributeShaderAdornmentViewAdornment(string feature, IElementFrame frame, Platform.Color shade)
			: base(feature, frame)
		{
			this.shade = shade;
		}
				
		public override void Render(IDrawingSurface context, ElementFrameGeometry geometry)
		{
			context.DrawRectangle(shade, shade, geometry.Bounds);
		}
	}
}
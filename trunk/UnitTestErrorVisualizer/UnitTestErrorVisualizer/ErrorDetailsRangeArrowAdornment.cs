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
using DevExpress.DXCore.Adornments;
using System.Drawing;
using Platform = DevExpress.DXCore.Platform.Drawing;

namespace UnitTestErrorVisualizer
{
	public class ErrorDetailsRangeArrowAdornment : RangeArrowAdornment
	{
		private const string kExpectedLabel = "Expected: ";
		private const string kActualLabel = "Actual:   ";
		ArrowDescription whatToDraw;
		public ErrorDetailsRangeArrowAdornment(IElementFrame binding, ArrowDescription whatToDraw)
			: base(binding)
		{
			this.whatToDraw = whatToDraw;
		}
		public override void Render(Platform.IDrawingSurface context)
		{
			base.Render(context);
			PaintParsedMessageOverArrow(context, whatToDraw, ComputeArrowHeight(whatToDraw));
		}
		private int ComputeArrowHeight(ArrowDescription whatToDraw)
		{
			TextView view = TextView.Active;
			System.Drawing.Point end = view.ToScreenPoint(view.GetPoint(whatToDraw.End.End));
			System.Drawing.Point start = view.ToScreenPoint(view.GetPoint(whatToDraw.Start.Start));
			return end.Y - start.Y;
		}

		/// <summary>
		/// Parse the message from the failed test and paint it in a way that empahsizes the important points
		/// </summary>
		private void PaintParsedMessageOverArrow(Platform.IDrawingSurface context, ArrowDescription whatToDraw, int arrowHeight)
		{
			TextView activeView = TextView.Active;
			using (Font consolas = new Font("Consolas", 9))
			{// Paint the details
				SizeF expectedLabelSize = activeView.MeasureString(kExpectedLabel, consolas);
				int correctWidth;

				Rectangle textRect = ComputeTextDimensions(arrowHeight, expectedLabelSize, whatToDraw, consolas, out correctWidth);
				PaintMessageBackground(context, textRect);
				PaintMessageText(context, textRect, expectedLabelSize, whatToDraw, consolas, correctWidth);
			}
		}
		/// <summary>
		/// Figure out what area the parsed error text will consume.
		/// </summary>
		private Rectangle ComputeTextDimensions(
			int arrowHeight,
			SizeF expectedLabelSize,
			ArrowDescription whatToDraw,
			Font font,
			out int correctWidth)
		{
			TextView activeView = TextView.Active;
			correctWidth = (int)(activeView.MeasureString(whatToDraw.Correct, font).Width);
			int expectedWidth = (int)(activeView.MeasureString(whatToDraw.Expected, font).Width);
			int incorrectWidth = (int)(activeView.MeasureString(whatToDraw.Incorrect, font).Width);
			int textWidth = (int)(expectedLabelSize.Width) + Math.Max(expectedWidth, correctWidth + incorrectWidth);
			int textHeight = (int)(expectedLabelSize.Height * 2);
			int textOffset = (arrowHeight - textHeight) / (arrowHeight > textHeight ? 2 : 1);

			ElementFrameGeometry geometry = GetViewElementFrameInfo().ElementFrameGeometry.ToLocation();
			System.Drawing.Point arrowStart = new System.Drawing.Point((int)geometry.StartPoint.X.Value, (int)geometry.StartPoint.Y.Value);
			Rectangle textRect = new Rectangle(
				arrowStart.X,
				arrowStart.Y + textOffset,
				textWidth,
				textHeight);
			return textRect;
		}
		private static void PaintMessageBackground(Platform.IDrawingSurface context, Rectangle textRect)
		{
			Platform.Rect backgroundRect = new Platform.Rect(textRect.X - 2, textRect.Y - 2, textRect.Width + 4, textRect.Height + 4);
			Platform.Rect shadowRect = new Platform.Rect(backgroundRect.X + 6, backgroundRect.Y + 6, backgroundRect.Width - 2, backgroundRect.Height - 2);
			context.DrawRectangle(Platform.Colors.Black, .4, Platform.Colors.Black, .4, 0, shadowRect);
			context.DrawRectangle(Platform.Colors.White, 1, Platform.Colors.Black, 1, 1, backgroundRect);
		}
		private static void PaintMessageText(
			Platform.IDrawingSurface context,
			Rectangle textRect,
			SizeF expectedLabelSize,
			ArrowDescription whatToDraw,
			Font consolas,
			int correctWidth)
		{
			TextView activeView = TextView.Active;
			const string kFontName = "Consolas";
			const double kFontSize = 9;
			context.DrawString(kExpectedLabel, kFontName, kFontSize, Platform.Colors.DarkGray, new Platform.Point(textRect.X, textRect.Y));
			context.DrawString(whatToDraw.Expected, kFontName, kFontSize, Platform.Colors.LightSalmon, new Platform.Point(textRect.X + expectedLabelSize.Width, textRect.Y));
			context.DrawString(kActualLabel, kFontName, kFontSize, Platform.Colors.DarkGray, new Platform.Point(textRect.X, textRect.Y + expectedLabelSize.Height));
			context.DrawString(whatToDraw.Correct, kFontName, kFontSize, Platform.Colors.LightSalmon, new Platform.Point(textRect.X + expectedLabelSize.Width, textRect.Y + expectedLabelSize.Height));
			float incorrectStartX = textRect.X + expectedLabelSize.Width + correctWidth;
			context.DrawString(whatToDraw.Incorrect, kFontName, kFontSize, Platform.Colors.Red, new Platform.Point(incorrectStartX - 2, textRect.Y + expectedLabelSize.Height));
			context.DrawLine(Platform.Colors.Red, new Platform.Point(incorrectStartX, textRect.Y + 2), new Platform.Point(incorrectStartX, textRect.Y + expectedLabelSize.Height + expectedLabelSize.Height - 2));
		}
	}
}

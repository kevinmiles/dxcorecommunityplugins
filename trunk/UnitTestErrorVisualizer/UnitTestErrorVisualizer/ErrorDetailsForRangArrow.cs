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
	public class ErrorDetailsForRangArrow 
	{
		private const string kExpectedLabel = "Expected: ";
		private const string kActualLabel = "Actual:   ";
		ArrowDescription whatToDraw;
		public ErrorDetailsForRangArrow(ArrowDescription whatToDraw)
		{
			this.whatToDraw = whatToDraw;
		}
		public void Render(Platform.IDrawingSurface context, Platform.Rect baseDrawingRect)
		{
			PaintParsedMessageOverArrow(context, baseDrawingRect, whatToDraw);
		}

		/// <summary>
		/// Parse the message from the failed test and paint it in a way that empahsizes the important points
		/// </summary>
		private static void PaintParsedMessageOverArrow(Platform.IDrawingSurface context, Platform.Rect baseDrawingRect, ArrowDescription whatToDraw)//, int arrowHeight)
		{
			TextView activeView = TextView.Active;
			using (Font consolas = new Font("Consolas", 9))
			{// Paint the details
				SizeF expectedLabelSize = activeView.MeasureString(kExpectedLabel, consolas);
				double correctWidth;

				Platform.Rect textRect = ComputeTextDimensions(baseDrawingRect, expectedLabelSize, whatToDraw, consolas, out correctWidth);
				PaintMessageBackground(context, textRect);
				PaintMessageText(context, textRect, expectedLabelSize, whatToDraw, correctWidth);
			}
		}
		/// <summary>
		/// Figure out what area the parsed error text will consume.
		/// </summary>
		private static Platform.Rect ComputeTextDimensions(
			Platform.Rect baseDrawingRect,
			SizeF expectedLabelSize,
			ArrowDescription whatToDraw,
			Font font,
			out double correctWidth)
		{
			TextView activeView = TextView.Active;
			correctWidth = activeView.MeasureString(whatToDraw.Correct, font).Width;
			double expectedWidth = activeView.MeasureString(whatToDraw.Expected, font).Width;
			double incorrectWidth = activeView.MeasureString(whatToDraw.Incorrect, font).Width;
			double textWidth = expectedLabelSize.Width + Math.Max(expectedWidth, correctWidth + incorrectWidth);
			double textHeight = expectedLabelSize.Height * 2;
			double textOffset = (baseDrawingRect.Height - textHeight) / (baseDrawingRect.Height > textHeight ? 2 : 1);

			Platform.Rect textRect = new Platform.Rect(
				baseDrawingRect.X,
				baseDrawingRect.Y + textOffset,
				textWidth,
				textHeight);
			return textRect;
		}
		private static void PaintMessageBackground(Platform.IDrawingSurface context, Platform.Rect textRect)
		{
			Platform.Rect backgroundRect = new Platform.Rect(textRect.X - 2, textRect.Y - 2, textRect.Width + 4, textRect.Height + 4);
			Platform.Rect shadowRect = new Platform.Rect(backgroundRect.X + 6, backgroundRect.Y + 6, backgroundRect.Width - 2, backgroundRect.Height - 2);
			context.DrawRectangle(Platform.Colors.Black, .4, Platform.Colors.Black, .4, 0, shadowRect);
			context.DrawRectangle(Platform.Colors.White, 1, Platform.Colors.Black, 1, 1, backgroundRect);
		}
		private static void PaintMessageText(
			Platform.IDrawingSurface context, Platform.Rect textRect, SizeF expectedLabelSize, ArrowDescription whatToDraw, double correctWidth)
		{
			const string kFontName = "Consolas";
			const double kFontSize = 9;
			context.DrawString(kExpectedLabel, kFontName, kFontSize, Platform.Colors.DarkGray, new Platform.Point(textRect.X, textRect.Y));
			context.DrawString(whatToDraw.Expected, kFontName, kFontSize, Platform.Colors.LightSalmon, new Platform.Point(textRect.X + expectedLabelSize.Width, textRect.Y));
			context.DrawString(kActualLabel, kFontName, kFontSize, Platform.Colors.DarkGray, new Platform.Point(textRect.X, textRect.Y + expectedLabelSize.Height));
			context.DrawString(whatToDraw.Correct, kFontName, kFontSize, Platform.Colors.LightSalmon, new Platform.Point(textRect.X + expectedLabelSize.Width, textRect.Y + expectedLabelSize.Height));
			double incorrectStartX = textRect.X + expectedLabelSize.Width + correctWidth;
			context.DrawString(whatToDraw.Incorrect, kFontName, kFontSize, Platform.Colors.Red, new Platform.Point(incorrectStartX - 2, textRect.Y + expectedLabelSize.Height));
			context.DrawLine(Platform.Colors.Red, new Platform.Point(incorrectStartX, textRect.Y + 2), new Platform.Point(incorrectStartX, textRect.Y + expectedLabelSize.Height + expectedLabelSize.Height - 2));
		}
	}
}

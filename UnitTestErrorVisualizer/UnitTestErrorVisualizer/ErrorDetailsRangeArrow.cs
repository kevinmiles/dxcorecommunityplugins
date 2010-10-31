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
using System.Drawing;

namespace UnitTestErrorVisualizer
{
	public class ErrorDetailsRangeArrow : RangeArrow
	{
		private const string kExpectedLabel = "Expected: ";
		private const string kActualLabel = "Actual:   ";
		ArrowDescription data;
		public ErrorDetailsRangeArrow(SourceRange start, SourceRange end, ArrowDescription whatToDraw)
			: base (start, end)
		{
			data = whatToDraw;
		}
//Okay, I actually need to derive from the docadornment and the visual adornment to make this happen. More work to come... now to bed.

		private void DrawArrow(TextView view, ArrowDescription whatToDraw)
		{
			int arrowHeight = PaintArrow(view, whatToDraw);
			PaintParsedMessageOverArrow(whatToDraw, arrowHeight);
		}
		/// <summary>
		/// Draw the arrow from the tile to the failed assert.
		/// </summary>
		private static int PaintArrow(TextView view, ArrowDescription whatToDraw)
		{
			//_ArrowToAssert = new RangeArrow(whatToDraw.Start, whatToDraw.End);
			//_ArrowToAssert.Add(TextView.Active);
			//return _ArrowToAssert.Range.Height;
			return 0;
		}

		/// <summary>
		/// Parse the message from the failed test and paint it in a way that empahsizes the important points
		/// </summary>
		private void PaintParsedMessageOverArrow(ArrowDescription whatToDraw, int arrowHeight)
		{
			TextView activeView = TextView.Active;
			using (Font consolas = new Font("Consolas", 9))
			{// Paint the details
				SizeF expectedLabelSize = activeView.MeasureString(kExpectedLabel, consolas);
				int correctWidth ;

				Rectangle textRect = ComputeTextDimensions(arrowHeight, expectedLabelSize, whatToDraw, consolas, out correctWidth);
				PaintMessageBackground(textRect);
				PaintMessageText(textRect, expectedLabelSize, whatToDraw, consolas, correctWidth);
			}
		}
		/// <summary>
		/// Figure out what area the parsed error text will consume.
		/// </summary>
		private static Rectangle ComputeTextDimensions(
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

			SourcePoint whatToDrawTestRangeStart = whatToDraw.Test.Range.Start;
			System.Drawing.Point arrowStart = activeView.ToScreenPoint(activeView.GetPoint(whatToDrawTestRangeStart));
			Rectangle textRect = new Rectangle(
				arrowStart.X,
				arrowStart.Y + textOffset,
				textWidth,
				textHeight);
			return textRect;
		}
		private static void PaintMessageBackground(Rectangle textRect)
		{
			TextView activeView = TextView.Active;
			Rectangle backgroundRect = new Rectangle(textRect.X - 2, textRect.Y - 2, textRect.Width + 4, textRect.Height + 4);
			//graphics.FillRectangle(Brushes.LightGray, backgroundRect.X + 2, backgroundRect.Y + 2, backgroundRect.Width + 2, backgroundRect.Height + 2);
			//graphics.FillRectangle(Brushes.White, backgroundRect);
			//graphics.DrawRectangle(Pens.Black, backgroundRect);
		}
		private static void PaintMessageText(
			Rectangle textRect,
			SizeF expectedLabelSize,
			ArrowDescription whatToDraw,
			Font consolas,
			int correctWidth)
		{
			TextView activeView = TextView.Active;
			//graphics.DrawString(kExpectedLabel, consolas, Brushes.DarkGray, textRect.X, textRect.Y);
			//graphics.DrawString(whatToDraw.Expected, consolas, Brushes.LightSalmon, textRect.X + expectedLabelSize.Width, textRect.Y);
			//graphics.DrawString(kActualLabel, consolas, Brushes.DarkGray, textRect.X, textRect.Y + expectedLabelSize.Height);
			//graphics.DrawString(whatToDraw.Correct, consolas, Brushes.LightSalmon, textRect.X + expectedLabelWidth, textRect.Y + lineHeight);
			//float incorrectStartX = textRect.X + expectedLabelWidth + correctWidth;
			//graphics.DrawString(whatToDraw.Incorrect, consolas, Brushes.Red, incorrectStartX - 2, textRect.Y + lineHeight);
			//graphics.DrawLine(Pens.Red, incorrectStartX, textRect.Y + 2, incorrectStartX, textRect.Y + lineHeight + lineHeight - 2);
		}

	}
}

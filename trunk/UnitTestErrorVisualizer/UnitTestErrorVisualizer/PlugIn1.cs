using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core.Testing;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace UnitTestErrorVisualizer
{
	public partial class PlugIn1 : StandardPlugIn
	{
		public class ArrowDescription
		{
			public Rectangle Start { get; set; } // There is no source range under the tile, so we use a rect for the start
			public SourceRange StartRange { get; set; }
			public SourceRange End { get; set; } // converted to a rect later
		}
        private ArrowDescription visibleArrow = null;
        // DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();

			LoadSettings();
		}

		private void LoadSettings()
		{
			using (DecoupledStorage storage = OptUnitTestVisualizer.Storage)
			{
				ShadeAttribute = OptUnitTestVisualizer.ReadShadeAttribute(storage);
				DrawArrowToAssert = OptUnitTestVisualizer.ReadDrawArrow(storage);
				OverlayError = OptUnitTestVisualizer.ReadOverlayError(storage);
				PassedColor = OptUnitTestVisualizer.ReadTestPassColor(storage);
				FailedColor = OptUnitTestVisualizer.ReadTestFailColor(storage);
				SkippedColor = OptUnitTestVisualizer.ReadTestSkipColor(storage);
			}
		}
		public bool ShadeAttribute { get; set; }
		public bool DrawArrowToAssert { get; set; }
		public bool OverlayError { get; set; }
		public Color PassedColor { get; set; }
		public Color FailedColor { get; set; }
		public Color SkippedColor { get; set; }

		private void PlugIn1_OptionsChanged(OptionsChangedEventArgs ea)
		{
			LoadSettings();
		}
		#endregion
		#region FinalizePlugIn
		public override void FinalizePlugIn()
		{
			//
			// TODO: Add your finalization code here.
			//

			base.FinalizePlugIn();
		}
		#endregion

		private void PlugIn1_EditorPaintLanguageElement(EditorPaintLanguageElementEventArgs ea)
		{
			LanguageElement element = ea.LanguageElement;
			if (element.ElementType == LanguageElementType.Attribute)
			{
				DevExpress.CodeRush.StructuralParser.Attribute attribute = (DevExpress.CodeRush.StructuralParser.Attribute)element;
				if (attribute.TargetNode.ElementType == LanguageElementType.Method)
				{
					Method target = (Method)attribute.TargetNode;
					TestMethodCollection tests = CodeRush.UnitTests.Tests;
					foreach (TestMethod test in tests)
					{
						if (target.Location == test.FullName)
						{
							PointToFailedAssert(ea, element, test);
							ShadeTestAttribute(ea.PaintArgs, attribute, test);
						}
					}
				}
			}
		}

		#region ShadeAttribute
		private void ShadeTestAttribute(EditorPaintEventArgs paintArgs, DevExpress.CodeRush.StructuralParser.Attribute attribute, TestMethod testMethod)
		{
			if (paintArgs.LineInView(attribute.Range.Start.Line) && ShadeAttribute == true)
			{
				if (testMethod.Status != TestStatus.Pending && testMethod.Status != TestStatus.FailedWithChanges && testMethod.Status != TestStatus.PassedWithChanges)
				{
					Point topLeft = paintArgs.TextView.GetPoint(attribute.Parent.Range.Start);
					int methodStartLine = attribute.TargetNode.Range.Start.Line;
					Point bottomRight = paintArgs.TextView.GetPoint(methodStartLine, paintArgs.TextView.LengthOfLine(methodStartLine));
					Rectangle area = new Rectangle(topLeft.X, topLeft.Y, bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
					using (Brush b = new SolidBrush(GetBackgroundColor(testMethod.Status)))
					{
						paintArgs.Graphics.FillRectangle(b, area);
					}
				}
			}
		}
		///// <summary>
		///// Draw all the test attributes the same and put the status color in the background
		///// </summary>
		//private void RedrawTestAttribute(EditorPaintEventArgs paintArgs, UnitTestDetail testData)
		//{
		//    if (paintArgs.LineInView(testData.Attribute.StartLine) && testData.Result.Status != TestStatus.Unknown)
		//    {
		//        Point topLeft = paintArgs.TextView.GetPoint(testData.Attribute.StartLine, testData.Attribute.Parent.StartOffset);
		//        Point lowerRight = paintArgs.TextView.GetPoint(testData.Attribute.StartLine + 1, paintArgs.TextView.LengthOfLine(testData.Method.StartLine) + 1);
		//        Rectangle area = new Rectangle(topLeft.X, topLeft.Y, lowerRight.X - topLeft.X, lowerRight.Y - topLeft.Y);
		//        using (Brush b = new SolidBrush(GetBackgroundColor(testData.Result.Status)))
		//        {
		//            paintArgs.Graphics.FillRectangle(b, area);
		//        }
		//    }
		//}

		/// <summary>
		/// Look up the method location in the test results. If found use the test results to pick the background color
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns> 
		private Color GetBackgroundColor(TestStatus status)
		{
			switch (status)
			{
				//case TestStatus.Pending:
				default:
					return CodeRush.VSSettings.BackgroundColor;
				case TestStatus.Ignored:
					return SkippedColor;
				case TestStatus.Passed:
				//case TestStatus.PassedWithChanges:
					return PassedColor;
				case TestStatus.Failure:
				//case TestStatus.FailedWithChanges:
					return FailedColor;
			}
		}

		///// <summary>
		///// Draw the parsed error text at the end of the method causing the test failure 
		///// </summary>
		//private void DrawError(EditorPaintLanguageElementEventArgs ea, TestResult testResult)
		//{
		//    if (testResult.Status == TestStatus.Failed)
		//    {
		//        FailureData failure = testResult.Failure;
		//        if (failure.FailingStatement != null)
		//        {
		//            int errorTextStartCol = failure.FailingStatement.EndOffset + 5;
		//            if (string.IsNullOrEmpty(failure.Expected))
		//            {// not an equal comparison
		//                ea.PaintArgs.OverlayText("<------- Test failed here",
		//                    failure.FailingStatement.StartLine,
		//                    errorTextStartCol,
		//                    FailedColor);

		//            }
		//            else if (failure.ActualDiffersAt < 0)
		//            {
		//                ea.PaintArgs.OverlayText(string.Format("Expected: {0} Actual: {1}", failure.Expected, failure.Actual),
		//                    failure.FailingStatement.StartLine,
		//                    errorTextStartCol,
		//                    FailedColor);
		//            }
		//            else
		//            {
		//                int start = errorTextStartCol;
		//                string correctPortion = string.Format("Expected: {0} Actual: {1}", failure.Expected, failure.Actual.Substring(0, failure.ActualDiffersAt));
		//                ea.PaintArgs.OverlayText(correctPortion,
		//                    failure.FailingStatement.StartLine,
		//                    start,
		//                    FailedColor);
		//                ea.PaintArgs.OverlayText(failure.Actual.Substring(failure.ActualDiffersAt),
		//                    failure.FailingStatement.StartLine,
		//                    start + correctPortion.Length,
		//                    Color.Red);
		//            }
		//        }
		//    }
		//}
		#endregion
		#region Draw Arrow To Assert
		private void PointToFailedAssert(EditorPaintLanguageElementEventArgs ea, LanguageElement element, TestMethod test)
		{
			if (test.Status == TestStatus.Failure && DrawArrowToAssert == true)
			{
				LanguageElement attrib = element.PreviousNode;
				string[] testAttributes = new string[] { "Test", "TestMethod", "Fact", "Theory" };
				while (attrib.ElementType != LanguageElementType.AttributeSection && Array.Exists(testAttributes, a => a == attrib.FirstDetail.Name))
				{
					attrib = attrib.PreviousNode;
					if (attrib.ElementType == LanguageElementType.Method)
					{
						break;
					}
				}
				if (Array.Exists(testAttributes, a => a == attrib.FirstDetail.Name))
				{
					Point topLeft = ea.PaintArgs.TextView.GetPoint(attrib.Range.Start.Line, attrib.Range.Start.Offset);
					ea.PaintArgs.TextView.AddTile(NewTile(new Rectangle(topLeft.X - 24, topLeft.Y + 2, 16, 16), test.TestResult));
				}
			}
		}

		private void PlugIn1_TileMouseEnter(object sender, TileEventArgs ea)
		{
			int startLine;
			int startColumn;
			Point topLeft = ea.Tile.Bounds.Location;
			topLeft.Y += ea.Tile.Bounds.Height;
			ea.Tile.TextView.GetLineAndColumn(topLeft, out startLine, out startColumn);
			visibleArrow = new ArrowDescription();
			visibleArrow.Start = ea.Tile.Bounds;
			visibleArrow.StartRange = new SourceRange(startLine, startColumn);

			TestResult r = (TestResult)ea.Tile.Object;
			string location = ExtractLineAndColumnData(r.StackTrace);
			visibleArrow.End = new SourceRange(ParseLineNumber(location), ParseColumnNumber(location));

			DrawArrow(ea.Tile.TextView, visibleArrow);
		}
		private int ParseColumnNumber(string location)
		{
			int startColData = location.IndexOf(',') + 2;
			int endColData = location.LastIndexOf(')');
			return int.Parse(location.Substring(startColData, endColData - startColData));
		}

		private static int ParseLineNumber(string location)
		{
			return int.Parse(location.Substring(1, location.IndexOf(',') - 1));
		}

		private string ExtractLineAndColumnData(string stackTrace)
		{
			//Xunit implementation
			return Regex.Match(stackTrace, @"\(\d+, \d+\)").Value;
		}

		private static void DrawArrow(TextView textView, ArrowDescription arrowDescription)
		{
			RangeArrow arrowToAssert = new RangeArrow();
			//arrowToAssert.StartRange.Set(arrowDescription.StartRange);
			//arrowToAssert.EndRange.Set(arrowDescription.End);
			arrowToAssert.StartRect = arrowDescription.Start;
			arrowToAssert.EndRect = textView.GetRectangleFromRange(arrowDescription.End);
			arrowToAssert.Color = Color.Red;
			arrowToAssert.Paint(textView.Graphics);
		}

		private void PlugIn1_EditorPaintForeground(EditorPaintEventArgs ea)
		{
			if (visibleArrow != null)
				DrawArrow(ea.TextView, visibleArrow);
		}

		private void PlugIn1_TileMouseLeave(object sender, TileEventArgs ea)
		{
			Rectangle end = ea.Tile.TextView.GetRectangleFromRange(visibleArrow.End);
			Rectangle arrowRect = new Rectangle(visibleArrow.Start.X, visibleArrow.Start.Y, end.X - visibleArrow.Start.X, end.Y - visibleArrow.Start.Y);
			SourceRange r = new SourceRange(visibleArrow.StartRange.Top, visibleArrow.End.Bottom);
			visibleArrow = null;
			//ea.Tile.TextView.Invalidate(arrowRect);
			ea.Tile.TextView.Invalidate(r);
		}
		#endregion
	}
}
using System;
using System.Drawing;
using DevExpress.CodeRush.Common;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core.Testing;
using System.Text.RegularExpressions;
using DevExpress.DXCore.Loader;

namespace UnitTestErrorVisualizer
{
    public partial class PlugIn1 : StandardPlugIn
	{
		public class ArrowDescription
		{
			public Rectangle Start { get; set; } // There is no source range under the tile, so we use a rect for the start
			public SourceRange StartRange { get; set; }
			public SourceRange End { get; set; } // converted to a rect later
			public string Message { get; set; }
		}
        private ArrowDescription visibleArrow = null;
		private static bool Attached { get; set; }
		// DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();
			LoadSettings();
			Attached = false;
		}

		private void LoadSettings()
		{
			using (DecoupledStorage storage = OptUnitTestVisualizer.Storage)
			{
				ShadeAttribute = OptUnitTestVisualizer.ReadShadeAttribute(storage);
				DrawArrowToAssert = OptUnitTestVisualizer.ReadDrawArrow(storage);
				ShortenLongStrings = OptUnitTestVisualizer.ReadShortenLongStrings(storage);
				MaxContextLength = Convert.ToInt32(OptUnitTestVisualizer.ReadMaxContextLength(storage));
				ConvertEscapeCharacters = OptUnitTestVisualizer.ReadConvertEscapeCharacters(storage);
				OverlayError = OptUnitTestVisualizer.ReadOverlayError(storage);
				PassedColor = OptUnitTestVisualizer.ReadTestPassColor(storage);
				FailedColor = OptUnitTestVisualizer.ReadTestFailColor(storage);
				SkippedColor = OptUnitTestVisualizer.ReadTestSkipColor(storage);
			}
		}
		public bool ConvertEscapeCharacters { get; set; }
		public int MaxContextLength { get; set; }
		public bool ShortenLongStrings { get; set; }
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

		//private static void AttachToRedGreenEvents()
		//{
		//    if (Attached == false)
		//    {
		//        PlugInAssembly pluginAssembly = (PlugInAssembly)GetPluginAssemblyByName("RedGreen");
		//        pluginAssembly.Assembly.GetType("RedGreen.ITestRunner");
		//        //ILoaderEngine loader = CodeRush.LoaderEngine;
		//        //foreach (IPlugInAssembly pa in loader.CommunityPlugIns)
		//        //{
		//        //    PlugInAssembly pluginAssembly = pa as PlugInAssembly;
		//        //    if (!pluginAssembly.Loaded)
		//        //        return;

		//        //    IPlugIn[] plugIns = pluginAssembly.PlugIns;

		//        //    if (plugIns == null)
		//        //        return;

		//        //    foreach (IPlugIn item in plugIns)
		//        //    {
		//        //        if (item.ToString() == "RedGreen.Plugin1")
		//        //        {
		//        //            Attached = true;
		//        //        }
		//        //        //StandardPlugIn instance = item as StandardPlugIn; //use your plug-in type here, e.g. MyPlugInType instance = item as MyPlugInType;
		//        //        //if (instance != null)
		//        //        //{
		//        //        //    MessageBox.Show("PlugIn instance is found!");
		//        //        //    break;
		//        //        //}
		//        //    }
		//        //}
		//    }
		//}
		////static PlugInAssembly GetPluginAssemblyByName(string plugInName)
		////{
		////    IPlugInAssembly pluginAssembly = GetPlugInAssemblyByName(plugInName, CodeRush.LoaderEngine.CommunityPlugIns);
		////    if (pluginAssembly != null)
		////        return (PlugInAssembly)pluginAssembly;
		////    pluginAssembly = GetPlugInAssemblyByName(plugInName, CodeRush.LoaderEngine.CommunitySystemAssemblies);
		////    if (pluginAssembly != null)
		////        return (PlugInAssembly)pluginAssembly;
		////    pluginAssembly = GetPlugInAssemblyByName(plugInName, CodeRush.LoaderEngine.SystemPlugIns);
		////    if (pluginAssembly != null)
		////        return (PlugInAssembly)pluginAssembly;
		////    pluginAssembly = GetPlugInAssemblyByName(plugInName, CodeRush.LoaderEngine.PlugIns);
		////    return (PlugInAssembly)pluginAssembly;
		////}
		//static IPlugInAssembly GetPlugInAssemblyByName(string plugInName, IPlugInAssemblyCollection collection)
		//{
		//    if (String.IsNullOrEmpty(plugInName))
		//        return null;
		//    foreach (IPlugInAssembly item in collection)
		//        if (plugInName.Equals(item.Name, StringComparison.OrdinalIgnoreCase))//You can use plugIn.FileName to find your PlugIn.
		//            return item;
		//    return null;
		//}

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
					//AttachToRedGreenEvents();

					Method target = (Method)attribute.TargetNode;
					TestMethodCollection tests = CodeRush.UnitTests.Tests;
					foreach (TestMethod test in tests)
					{
						if (target.Location == test.FullName)
						{
							PointToFailedAssert(ea, element, test);
							ShadeTestAttribute(ea.PaintArgs, attribute, test);
							OverlayErrorNextToAssert(ea, test);
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
					if (attribute == GetFirstVisible(paintArgs, attribute.Parent.FirstDetail))
					{// Only paint the background color once 
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
		}
		private LanguageElement GetFirstVisible(EditorPaintEventArgs paintArgs, LanguageElement element)
		{
			while (paintArgs.LineInView(element.Range.Start.Line) == false)
			{
				element = element.NextSibling;
			}
			return element;
		}

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

		/// <summary>
		/// Draw the parsed error text at the end of the method causing the test failure 
		/// </summary>
		private void OverlayErrorNextToAssert(EditorPaintLanguageElementEventArgs ea, TestMethod test)
		{
			if (OverlayError == true && test.TestResult != null && test.TestResult.Status == TestStatus.Failure)
			{
				int line = TestResultParser.LineNumber(test.TestResult.StackTrace);
				LanguageElement statement = GetStatement(test.FullName, line);
				if (statement != null)
				{
					int startColumn = statement.EndOffset + 5;
					string expected = TestResultParser.Expected(test.TestResult.Message);
					string actual = TestResultParser.Actual(test.TestResult.Message);
					int differAt;
					differAt = TestResultParser.DifferAt(test.TestResult.Message, expected, actual);
					string correct;
					string incorrect;
					MessageLimiter limiter = new MessageLimiter(ShortenLongStrings, MaxContextLength, ConvertEscapeCharacters);
					limiter.AdjustExpectedActualLengths(ref expected, ref actual, differAt, out correct, out incorrect);

					if (string.IsNullOrEmpty(expected))
					{
						ea.PaintArgs.OverlayText("<------- Test failed here",
							line,
							startColumn,
							FailedColor);
					}
					else if (differAt < 0)
					{
						ea.PaintArgs.OverlayText(string.Format("Expected: {0} Actual: {1}",
							expected,
							actual),
							line,
							startColumn,
							FailedColor);
					}
					else
					{
						string correctPortion = string.Format("Expected: {0} Actual: {1}", expected, correct);
						ea.PaintArgs.OverlayText(correctPortion,
							line,
							startColumn,
							FailedColor);
						ea.PaintArgs.OverlayText(incorrect,
							line,
							startColumn + correctPortion.Length,
							Color.Red);
					}
				}
			}
		}
	
		/// <summary>
		/// Get a statement language element that matches the location and the line number
		/// </summary>
		/// <param name="location">What location to look for</param>
		/// <param name="failAtLine">The line that the location should be on</param>
		/// <returns></returns>
		/// <remarks>Assumes that the location is in the active file.</remarks>
		internal static LanguageElement GetStatement(string location, int failAtLine)
		{
			try
			{
				if (CodeRush.Source.ActiveSourceFile == null)
				{
					return null;
				}
				LanguageElement node = CodeRush.Source.ActiveSourceFile.GetNodeAt(new SourcePoint(failAtLine, 0));
				if (node != null)
				{
					if (location == node.RootNamespaceLocation || location.StartsWith(node.RootNamespaceLocation))
					{
						LanguageElement statement = node.FirstChild;
						while (statement != null)
						{
							if (statement.StartLine == failAtLine)
							{
								return statement;
							}
							statement = statement.NextCodeSibling;
						}
					}
				}
			}
			catch
			{// eat exceptions
			}
			return null;
		}
		#endregion
		#region Draw Arrow To Assert
		private void PointToFailedAssert(EditorPaintLanguageElementEventArgs ea, LanguageElement element, TestMethod test)
		{
			if (test.Status == TestStatus.Failure && DrawArrowToAssert == true)
			{
				LanguageElement attrib = element.PreviousNode;
				if (attrib.FirstDetail != null)
				{
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
						Rectangle tileLocation = new Rectangle(topLeft.X - 32, topLeft.Y + 2, 16, 16);
						ea.PaintArgs.TextView.AddTile(NewTile(tileLocation, test.TestResult));
						try
						{
							ea.PaintArgs.TextView.Graphics.DrawIcon(new Icon(GetType(), "Invisible.ico"), tileLocation);
						}
						catch
						{// fail silently if icon is missing from the project.
						}

					}
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
			visibleArrow.Message = r.Message;
			string location = ExtractLineAndColumnData(r.StackTrace);
			visibleArrow.End = new SourceRange(ParseLineNumber(location), ParseColumnNumber(location));

			DrawArrow(ea.Tile.TextView, visibleArrow);
		}
		private static int ParseColumnNumber(string location)
		{
			int startColData = location.IndexOf(',') + 2;
			int endColData = location.LastIndexOf(')');
			return int.Parse(location.Substring(startColData, endColData - startColData));
		}

		private static int ParseLineNumber(string location)
		{
			return int.Parse(location.Substring(1, location.IndexOf(',') - 1));
		}

		private static string ExtractLineAndColumnData(string stackTrace)
		{
			//Xunit implementation
			return Regex.Match(stackTrace, @"\(\d+, \d+\)").Value;
		}

		private void DrawArrow(TextView textView, ArrowDescription arrowDescription)
		{
			int arrowHeight = PaintArrow(textView, arrowDescription);
			PaintParsedMessageOverArrow(textView, arrowDescription, arrowHeight);
		}
	
		/// <summary>
		/// Parse the message from the failed test and paint it in a way that empahsizes the important points
		/// </summary>
		private void PaintParsedMessageOverArrow(TextView textView, ArrowDescription arrowDescription, int arrowHeight)
		{
			// Parse the message
			string expected = TestResultParser.Expected(arrowDescription.Message);
			string actual = TestResultParser.Actual(arrowDescription.Message);
			int differAt = TestResultParser.DifferAt(arrowDescription.Message, expected, actual);
			string correct;
			string incorrect;
			MessageLimiter limiter = new MessageLimiter(ShortenLongStrings, MaxContextLength, ConvertEscapeCharacters);
			limiter.AdjustExpectedActualLengths(ref expected, ref actual, differAt, out correct, out incorrect);
			const string expectedLabel = "Expected: ";
			const string actualLabel = "Actual:   ";

			using (Font consolas = new Font("Consolas", 9))
			{// Paint the details
				SizeF expectedLabelSize = textView.Graphics.MeasureString(expectedLabel, consolas);
				int expectedWidth = (int)(textView.Graphics.MeasureString(expected, consolas).Width);
				int correctWidth = (int)(textView.Graphics.MeasureString(correct, consolas).Width);
				int incorrectWidth = (int)(textView.Graphics.MeasureString(incorrect, consolas).Width);
				Rectangle textRect = ComputeTextDimensions(arrowDescription.Start, arrowHeight, expectedLabelSize, expectedWidth, correctWidth, incorrectWidth);
				PaintMessageBackground(textView.Graphics, textRect);
				PaintMessageText(textView.Graphics, textRect, expectedLabel, expectedLabelSize.Width, expectedLabelSize.Height, expected, actualLabel, correct, incorrect, consolas, correctWidth);
			}
		}

		/// <summary>
		/// Draw the parsed error message on two lines. 
		/// </summary>
		private static void PaintMessageText(Graphics graphics,
			Rectangle textRect,
			string expectedLabel,
			float expectedLabelWidth,
			float lineHeight,
			string expected,
			string actualLabel,
			string correct,
			string incorrect,
			Font consolas,
			int correctWidth)
		{
			graphics.DrawString(expectedLabel, consolas, Brushes.DarkGray, textRect.X, textRect.Y);
			graphics.DrawString(expected, consolas, Brushes.LightSalmon, textRect.X + expectedLabelWidth, textRect.Y);
			graphics.DrawString(actualLabel, consolas, Brushes.DarkGray, textRect.X, textRect.Y + lineHeight);
			graphics.DrawString(correct, consolas, Brushes.LightSalmon, textRect.X + expectedLabelWidth, textRect.Y + lineHeight);
			float incorrectStartX = textRect.X + expectedLabelWidth + correctWidth;
			graphics.DrawString(incorrect, consolas, Brushes.Red, incorrectStartX - 2, textRect.Y + lineHeight);
			graphics.DrawLine(Pens.Red, incorrectStartX, textRect.Y + 2, incorrectStartX, textRect.Y + lineHeight + lineHeight - 2);
		}

		/// <summary>
		/// Draw a background rectangle slightly larger than the text will consume. Give it a black border and a drop shadow.
		/// </summary>
		private static void PaintMessageBackground(Graphics graphics, Rectangle textRect)
		{
			Rectangle backgroundRect = new Rectangle(textRect.X - 2, textRect.Y - 2, textRect.Width + 4, textRect.Height + 4);
			graphics.FillRectangle(Brushes.LightGray, backgroundRect.X + 2, backgroundRect.Y + 2, backgroundRect.Width + 2, backgroundRect.Height + 2);
			graphics.FillRectangle(Brushes.White, backgroundRect);
			graphics.DrawRectangle(Pens.Black, backgroundRect);
		}

		/// <summary>
		/// Draw the arrow from the tile to the failed assert.
		/// </summary>
		private static int PaintArrow(TextView textView, ArrowDescription arrowDescription)
		{
			RangeArrow arrowToAssert = new RangeArrow();
			//arrowToAssert.StartRange.Set(arrowDescription.StartRange);
			//arrowToAssert.EndRange.Set(arrowDescription.End);
			arrowToAssert.StartRect = arrowDescription.Start;
			arrowToAssert.EndRect = textView.GetRectangleFromRange(arrowDescription.End);
			arrowToAssert.Color = Color.LightCoral;
			arrowToAssert.Paint(textView.Graphics);
			int arrowHeight = arrowToAssert.EndRect.Y + arrowToAssert.EndRect.Height - arrowToAssert.StartRect.Y;
			return arrowHeight;
		}

		/// <summary>
		/// Figure out what area the parsed error text will consume.
		/// </summary>
		private static Rectangle ComputeTextDimensions(Rectangle arrowStart,
			int arrowHeight,
			SizeF expectedLabelSize,
			int expectedWidth,
			int correctWidth,
			int incorrectWidth)
		{
			int textWidth = (int)(expectedLabelSize.Width) + Math.Max(expectedWidth, correctWidth + incorrectWidth);
			int textHeight = (int)(expectedLabelSize.Height * 2);
			int textOffset = 0;
			if (arrowHeight > textHeight)
			{
				textOffset = (arrowHeight - textHeight) / 2;
			}
			else
			{
				textOffset = arrowHeight - textHeight;
			}
			Rectangle textRect = new Rectangle(
				arrowStart.X,
				arrowStart.Y + textOffset,
				textWidth,
				textHeight);
			return textRect;
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
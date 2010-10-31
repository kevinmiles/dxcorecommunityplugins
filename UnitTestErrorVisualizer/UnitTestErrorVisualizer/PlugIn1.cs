using System;
using System.Drawing;
using DevExpress.CodeRush.Common;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core.Testing;
using System.Text.RegularExpressions;

namespace UnitTestErrorVisualizer
{
	public partial class PlugIn1 : StandardPlugIn
	{
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
		private static readonly string[] testAttributes = new string[] { "Test", "TestMethod", "Fact", "Theory" };
		private void CreateErrorArrowAdornment(DecorateLanguageElementEventArgs args,
			DevExpress.CodeRush.StructuralParser.Attribute attribute,
			Method target,
			TestMethod test)
		{
			if (test.Status == TestStatus.Failure)
			{
				//string assertLocation = ExtractLineAndColumnData(test.TestResult.StackTrace);
				//ArrowDescription visibleArrow = new ArrowDescription(
				//    new SourceRange(attribute.StartLine, attribute.StartOffset),
				//    new SourceRange(ParseLineNumber(assertLocation), ParseColumnNumber(assertLocation)),
				//    test.TestResult.Message,
				//    target,
				//    new MessageLimiter(ShortenLongStrings, MaxContextLength, ConvertEscapeCharacters));
				ArrowDescription visibleArrow = new ArrowDescription(
													attribute,
													target,
													test.TestResult,
													new MessageLimiter(ShortenLongStrings, MaxContextLength, ConvertEscapeCharacters));
				args.AddAdornment(
					new FailedTestInspectorDocumentAdornment(
						new DocPoint(attribute.StartLine, attribute.StartOffset),
						new DocPoint(visibleArrow.End.Start.Line, visibleArrow.End.Start.Offset),
						this,
						visibleArrow));
			}
		}

		private void PlugIn1_DecorateLanguageElement(object sender, DecorateLanguageElementEventArgs args)
		{
			LanguageElement element = args.LanguageElement;
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
							CreateErrorArrowAdornment(args, attribute, target, test);
							//ShadeTestAttribute(ea.PaintArgs, attribute, test);
							//OverlayErrorNextToAssert(ea, test);
						}
					}
				}
			}

		}
	}
}
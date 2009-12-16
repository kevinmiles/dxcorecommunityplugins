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

			//
			// TODO: Add your initialization code here.
			//
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
				TestMethodCollection tests = CodeRush.UnitTests.Tests;
				foreach (TestMethod test in tests)
				{
					if (test.Status == TestStatus.Failure)
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
			int startColData = location.IndexOf(',')+2;
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
	}
}
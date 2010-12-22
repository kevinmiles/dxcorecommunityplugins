using System;
using System.Drawing;
using DevExpress.CodeRush.Common;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.Core.Testing;
using System.Text.RegularExpressions;
using Platform = DevExpress.DXCore.Platform.Drawing;

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
				AttributeColors = new ShadeColors {
					PassedColor = Platform.Color.ConvertFrom(OptUnitTestVisualizer.ReadTestPassColor(storage)),
					FailedColor = Platform.Color.ConvertFrom(OptUnitTestVisualizer.ReadTestFailColor(storage)),
					SkippedColor = Platform.Color.ConvertFrom(OptUnitTestVisualizer.ReadTestSkipColor(storage))
				};
			}
		}
		public ShadeColors AttributeColors { get; set; }
		public bool ConvertEscapeCharacters { get; set; }
		public int MaxContextLength { get; set; }
		public bool ShortenLongStrings { get; set; }
		public bool ShadeAttribute { get; set; }
		public bool DrawArrowToAssert { get; set; }

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


		private void CreateErrorArrowAdornment(DecorateLanguageElementEventArgs args,
			DevExpress.CodeRush.StructuralParser.Attribute attribute,
			Method target,
			TestMethod test)
		{
			if (test.Status == TestStatus.Failure && DrawArrowToAssert)
			{
				ArrowDescription visibleArrow = new ArrowDescription(
													attribute,
													target,
													test.TestResults[0],
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
							CreateAttributeShadeAdornment(args, attribute, target, test.Status);
							//OverlayErrorNextToAssert(ea, test);
						}
					}
				}
			}

		}
		private void CreateAttributeShadeAdornment(DecorateLanguageElementEventArgs args, 
            DevExpress.CodeRush.StructuralParser.Attribute attribute, 
            Method target, 
            TestStatus status)
		{
			if (ShadeAttribute && status != TestStatus.Pending && status != TestStatus.PassedWithChanges && status != TestStatus.FailedWithChanges)
			{
				SourceRange shadedArea = new SourceRange(
					attribute.Parent.Range.Start, 
					new SourcePoint(attribute.Range.End.Line, target.NameRange.End.Offset - 1));
				args.AddAdornment(new TestAttributeShaderAdornmentDocumentAdornment(shadedArea, status, AttributeColors));
			}
		}
	}
}
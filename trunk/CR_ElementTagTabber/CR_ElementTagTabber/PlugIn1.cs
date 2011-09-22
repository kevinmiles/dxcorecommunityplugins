using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_ElementTagTabber
{
	public partial class PlugIn1 : StandardPlugIn
	{
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

		#region IsCaretOnPairedHtmlTag
		/// <summary>
		/// Returns true if the specified caret position is located inside either the start or end tag of the specified HtmlElement.
		/// </summary>
		private static bool IsCaretOnPairedHtmlTag(LanguageElement element, SourcePoint caret)
		{
			DevExpress.CodeRush.StructuralParser.HtmlElement htmlElement = element as DevExpress.CodeRush.StructuralParser.HtmlElement;
			if (htmlElement == null)		// Not an HtmlElement
				return false;

			if (htmlElement.CloseTagNameRange == SourceRange.Empty)		// Not a paired Html tag.
				return false;

			return htmlElement.NameRange.Contains(caret) || htmlElement.CloseTagNameRange.Contains(caret);
		}
		#endregion

		private void ctxInPairedHtmlElementNameTag_ContextSatisfied(ContextSatisfiedEventArgs ea)
		{
			ea.Satisfied = IsCaretOnPairedHtmlTag(CodeRush.Source.Active, CodeRush.Caret.SourcePoint);
		}

		private void spHtmlTagNav_CheckAvailability(object sender, CheckSearchAvailabilityEventArgs ea)
		{
			ea.Available = IsCaretOnPairedHtmlTag(ea.Element, ea.Caret);
		}

		private void spHtmlTagNav_SearchReferences(object sender, SearchEventArgs ea)
		{
			DevExpress.CodeRush.StructuralParser.HtmlElement htmlElement = ea.Element as DevExpress.CodeRush.StructuralParser.HtmlElement;
			if (htmlElement == null)
				return;
			ea.AddRange(htmlElement.FileNode, htmlElement.NameRange);
			ea.AddRange(htmlElement.FileNode, htmlElement.CloseTagNameRange);
		}
	}

	public static class Extensions
	{
		public static void AddRange(this SearchEventArgs ea, SourceFile fileNode, SourceRange range)
		{
			ea.AddRange(new FileSourceRange(fileNode, range));
		}
	}

}
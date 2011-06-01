using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_QuickPair
{
	public partial class PlugIn1 : StandardPlugIn
	{
		private const string STR_EmbedQuickPair = "Embed Quick Pair";
		private const string STR_QuickPair = "Quick Pair";
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

		private void actQuickPair_Execute(ExecuteEventArgs ea)
		{
			string left = actQuickPair.Parameters["Left"].ValueAsStr;
			string right = actQuickPair.Parameters["Right"].ValueAsStr;
			bool positionCaretBetweenDelimiters = actQuickPair.Parameters["CaretBetween"].ValueAsBool;
			if (left.StartsWith("\","))
			{
				if (right == "true")
					positionCaretBetweenDelimiters = true;
				right = left.Substring(2).Trim();
				left = "\"";
			}
			TextDocument activeTextDocument = CodeRush.Documents.ActiveTextDocument;
			if (activeTextDocument == null)
				return;
			TextView activeView = activeTextDocument.ActiveView;
			if (activeView == null)
				return;
			TextViewSelection selection = activeView.Selection;
			if (selection == null)
				return;
			TextViewCaret caret = activeView.Caret;
			if (caret == null)
				return;
			if (selection.Exists)
			{
				Embedding embedding = new Embedding();
				embedding.Style = EmbeddingStyle.StartEnd;
				string[] top = { left };
				string[] bottom = { right };
				embedding.Top = top;
				embedding.Bottom = bottom;
				if (positionCaretBetweenDelimiters)
					embedding.AdjustSelection = PostEmbeddingSelectionAdjustment.Leave;
				else
					embedding.AdjustSelection = PostEmbeddingSelectionAdjustment.Extend;
				bool needToMoveCaretToTheRight = false;
        if (selection.AnchorPosition < selection.ActivePosition)
					needToMoveCaretToTheRight = true;
				using (activeTextDocument.NewCompoundAction(STR_EmbedQuickPair))
				{
					activeTextDocument.EmbedSelection(embedding);
					if (needToMoveCaretToTheRight)
					{
						selection.Set(selection.ActivePosition, selection.AnchorPosition);
					}
				}
			}
			else
			{
				if (CodeRush.Windows.IntellisenseEngine.HasSelectedCompletionSet(activeView))
					CodeRush.Command.Execute("Edit.InsertTab");
				using (activeTextDocument.NewCompoundAction(STR_QuickPair))
					if (positionCaretBetweenDelimiters)
						activeTextDocument.ExpandText(caret.SourcePoint, left + "«Caret»«Field()»" + right + "«FinalTarget»");
					else
						activeTextDocument.InsertText(caret.SourcePoint, left + right);
			}
		}
	}
}
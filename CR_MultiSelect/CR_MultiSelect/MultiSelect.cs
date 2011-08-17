using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.Core;
using System.Windows.Forms;
using DevExpress.CodeRush.StructuralParser;

namespace CR_MultiSelect
{
	[Serializable]
	public class MultiSelect
	{
		private const string STR_ClipboardFormatName = "DXCore.MultiCodeSelect";

		public string	FileName = String.Empty;
		public string ClassName = String.Empty;
		public string Language = String.Empty;
    public bool ContainsOnlyMembers;
		public List<PartialSelection> Selections = new List<PartialSelection>();

		
		public void AddSelection(TextView textView)
		{
			if (textView == null)
				return;
			PartialSelection newPartialSelection = new PartialSelection();

			TextViewSelection textViewSelection = textView.Selection;
			if (textViewSelection == null)
				return;
			SourcePoint finalCaretPosition = SourcePoint.Empty;
			LanguageElement memberSelected = null;
			if (!textViewSelection.Exists)
			{
				LanguageElement activeMember = CodeRush.Source.ActiveMember;
				if (activeMember != null)
				{
					SourceRange declareRange = new SourceRange(activeMember.Range.Top, activeMember.NameRange.Bottom);
					if (declareRange.Contains(textView.Caret.SourcePoint))
					{
						SourceRange fullBlockCutRange = activeMember.GetFullBlockCutRange();
						textView.Selection.Set(fullBlockCutRange);
						memberSelected = activeMember;
					}
					else
						CodeRush.Command.Execute("SelectionExpand");
				}
				else
					CodeRush.Command.Execute("SelectionExpand");
				if (!textViewSelection.Exists)
					return;
			}

			SourceRange selectionRange = new SourceRange(textViewSelection.Range.Top, textViewSelection.Range.Bottom);

			for (int i = Selections.Count - 1; i >= 0; i--)
			{
				PartialSelection compareSelection = Selections[i];
				if (selectionRange.Overlaps(compareSelection.Range))
				{
					selectionRange = SourceRange.Union(compareSelection.Range, selectionRange);
					compareSelection.RemoveHighlighter();
					Selections.RemoveAt(i);
				}
			}
			if (memberSelected != null)
			{
				newPartialSelection.ElementName = memberSelected.Name;
				newPartialSelection.ElementType = memberSelected.ElementType;
				finalCaretPosition = memberSelected.Range.Top;
			}
			else
			{
				LanguageElement nodeAtTopOfSelection = CodeRush.Source.GetNodeAt(selectionRange.Top);
				LanguageElement nodeBeforeEndOfSelection = CodeRush.Source.GetNodeBefore(selectionRange.Bottom);
				if (nodeAtTopOfSelection == nodeBeforeEndOfSelection || nodeAtTopOfSelection.Parents(nodeBeforeEndOfSelection) && selectionRange.Contains(nodeAtTopOfSelection.Range))
				{
					newPartialSelection.ElementName = nodeAtTopOfSelection.Name;
					newPartialSelection.ElementType = nodeAtTopOfSelection.ElementType;
				}
				else
				{
					SelectionScanner selectionScanner = new SelectionScanner();
					selectionScanner.Scan();
					if (selectionScanner.ElementsFound == 1)
						newPartialSelection.ElementName = selectionScanner.ElementName;
					newPartialSelection.ElementType = selectionScanner.ElementType;
				}
			}
			newPartialSelection.Range = selectionRange;
			newPartialSelection.Text = textView.TextDocument.GetText(selectionRange);
			newPartialSelection.AddHighlighter(textView);
			Selections.Add(newPartialSelection);

			if (finalCaretPosition == SourcePoint.Empty)
				finalCaretPosition = textViewSelection.Range.Top;
			textView.Caret.MoveTo(finalCaretPosition);
		}

		public void Clear()
		{
			foreach (PartialSelection selection in Selections)
				selection.RemoveHighlighter();
			Selections.Clear();
		}
    public string ToText()
		{
			string result = String.Empty;
			foreach (PartialSelection selection in Selections)
				result += selection.ToText();
			return result;
		}
    public void CopyToClipboard()
		{
			DataFormats.Format format = DataFormats.GetFormat(STR_ClipboardFormatName);
			IDataObject dataObject = new DataObject();
			dataObject.SetData(DataFormats.Text, ToText());
			dataObject.SetData(format.Name, this);
			Clipboard.SetDataObject(dataObject, true);
		}
		public static bool IsOnClipboard()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			return dataObject.GetDataPresent(STR_ClipboardFormatName);
		}
		public static MultiSelect FromClipboard()
		{
			IDataObject dataObject = Clipboard.GetDataObject();
			if (dataObject.GetDataPresent(STR_ClipboardFormatName))
				return dataObject.GetData(STR_ClipboardFormatName) as MultiSelect;
			return null;
		}
		public void Delete(string operation)
		{
			TextDocument textDocument = CodeRush.Documents.Get(FileName) as TextDocument;
			if (textDocument == null)
				return;
			foreach (PartialSelection selection in Selections)
				textDocument.QueueDelete(selection.Range);

			textDocument.ApplyQueuedEdits(operation);
		}
    public MultiSelect()
		{

		}
		public override string ToString()
		{
			CommentBuilder commentBuilder = new CommentBuilder(Language);
			string asStr;
			asStr = commentBuilder.GetComment("FileName: ", FileName);
			asStr += commentBuilder.GetComment("Class: ", ClassName);
			asStr += commentBuilder.GetComment("Language: ", Language);
			asStr += commentBuilder.GetComment("Selections: ");
			foreach (PartialSelection selection in Selections)
				asStr += selection.ToString(commentBuilder);
			return asStr;
		}

	}
}

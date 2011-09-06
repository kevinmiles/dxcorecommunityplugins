using System;
using System.Collections.Generic;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using System.Collections;

namespace DevExpress.CodeRush.Core
{
  [Serializable]
	public class MultiSelect
	{
		public string	FileName = String.Empty;
		public string TypeName = String.Empty;
		public string Language = String.Empty;
    public bool ContainsOnlyMembers;
		public bool WasCut;
		public List<PartialSelection> Selections = new List<PartialSelection>();

		// constructors...
		#region MultiSelect
		public MultiSelect()
		{

		}
		#endregion

		// private methods...
		#region GetHighlightRange
		/// <summary>
		/// Returns a range of code to highlight, which may be different from the actual code selected. This is done to 
		/// work around a cosmetic display bug in the 11.1 version of the BlockHighlighter control when highlighting 
		/// leading white space, and also to be more consistent with user expectations (users think of a member as starting
		/// and ending with code, not white space, but a cut or copy should include leading and trailing white space).
		/// </summary>
		private static SourceRange GetHighlightRange(TextView textView, SourceRange selectionRange)
		{
			SourcePoint top = selectionRange.Top;
			SourcePoint bottom = selectionRange.Bottom;
			if (bottom.Offset == 1)
			{
				if (bottom.Line > 1)
				{
					string lineAbove = textView.TextDocument.GetText(bottom.Line - 1);
					if (lineAbove.Length > 0)
					{
						bottom = new SourcePoint(bottom.Line - 1, lineAbove.Length + 1);
					}
				}
			}
			string topLine = textView.TextDocument.GetText(top.Line);
			//string bottomLine = textView.TextDocument.GetText(bottom.Line);
			string topLeft;
			//string bottomLeft;
			int leadingWhiteSpaceCount = 0;
			//int trailingWhiteSpaceCount = 0;
			if (top.Offset - 1 > 0 && top.Offset - 1 < topLine.Length)
				topLeft = topLine.Substring(0, top.Offset - 1);
			else
				topLeft = topLine;
			leadingWhiteSpaceCount = CodeRush.StrUtil.GetLeadingWhiteSpaceCharCount(topLeft);
			top = top.OffsetPoint(0, leadingWhiteSpaceCount);

			//if (bottom.Offset - 1 > 0 && bottom.Offset - 1 < bottomLine.Length)
			//	bottomLeft = bottomLine.Substring(0, bottom.Offset - 1);
			//else
			//	bottomLeft = bottomLine;
			//trailingWhiteSpaceCount = CodeRush.StrUtil.GetLeadingWhiteSpaceCharCount(bottomLeft);
			//bottom = bottom.OffsetPoint(0, trailingWhiteSpaceCount);

			SourceRange highlightRange = new SourceRange(top, bottom);
			return highlightRange;
		}
		#endregion

		// public methods...
		#region AddSelection(TextView textView)
		/// <summary>
		/// Adds the selection in the specified TextView (or the active member if there is no selection) to this MultiSelect instance.
		/// </summary>
		public void AddSelection(TextView textView)
		{
			if (textView == null)
				return;
			TextViewSelection textViewSelection = textView.Selection;
			if (textViewSelection == null)
				return;

			PartialSelection newPartialSelection = new PartialSelection(Selections.Count);

			// TODO: Refactor this so we find the selected member in one method (currently we have memberSelected, nodeAtTopOfSelection and selectionScanner performing similar functionality).
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
			SourceRange highlightRange;
			if (memberSelected != null)
			{
				LanguageElement startNode;
				LanguageElement endNode;
				memberSelected.GetFullBlockNodes(out startNode, out endNode);
				highlightRange = new SourceRange(startNode.Range.Top, endNode.Range.Bottom);
				if (Selections.Count == 0)
					ContainsOnlyMembers = true;
			}
			else
			{
				highlightRange = selectionRange;
				ContainsOnlyMembers = false;
			}
			newPartialSelection.HighlightRange = highlightRange;
			newPartialSelection.Text = textView.TextDocument.GetText(selectionRange);

			if (finalCaretPosition == SourcePoint.Empty)
				finalCaretPosition = textViewSelection.Range.Top;
			newPartialSelection.CaretPosition = finalCaretPosition;
			AddSelection(textView, newPartialSelection);
		}
		#endregion
    #region AddSelection(TextView textView, PartialSelection selection)
		/// <summary>
		/// Adds the specified PartialSelection to this MultiSelect instance, highlighting the selection onscreen, and clears the Visual Studio selection.
		/// </summary>
		public void AddSelection(TextView textView, PartialSelection selection)
		{
			selection.AddHighlighter(textView);
			Selections.Add(selection);
			textView.Caret.MoveTo(selection.CaretPosition);
		}
		#endregion
    #region Clear
		/// <summary>
		/// Clears all selections from this MultiSelect instance.
		/// </summary>
    public void Clear()
		{
			foreach (PartialSelection selection in Selections)
				selection.RemoveHighlighter();
			Selections.Clear();
		}
    #endregion
		#region CopyToClipboard
		/// <summary>
		/// Copies this MultiSelect instance to the clipboard.
		/// </summary>
		public void CopyToClipboard()
		{
			CodeRushPlaceholder.MultiSelect.CopyToClipboard(this);
		}
		#endregion
		#region Delete
		/// <summary>
		/// Deletes all the multi-selections from the document 
		/// </summary>
		/// <param name="operation"></param>
		public void Delete(string operation)
		{
			CodeRushPlaceholder.MultiSelect.Delete(this, operation);
		}
		#endregion
		#region FromClipboard
		/// <summary>
		/// Gets the MultiSelect instance from the clipboard.
		/// </summary>
		public static MultiSelect FromClipboard()
		{
			return CodeRushPlaceholder.MultiSelect.FromClipboard();
		}
		#endregion
		#region GetSelectionAt
		/// <summary>
		/// Gets the selection at the specified index. Selections are indexed by the order in which they are added.
		/// </summary>
		/// <param name="index">The index order in which this selection was added (0 is the first added selection). </param>
		public PartialSelection GetSelectionAt(int index)
		{
			foreach (PartialSelection selection in Selections)
			{
				if (selection._Index == index)
					return selection;
			}
			return null;
		}
		#endregion
		#region IsOnClipboard
		/// <summary>
		/// Returns true if the clipboard holds a MultiSelect instance.
		/// </summary>
		/// <returns></returns>
		public static bool IsOnClipboard()
		{
			return CodeRushPlaceholder.MultiSelect.IsOnClipboard();
		}
		#endregion
		
		#region PrepareToGenerate
		/// <summary>
		/// Call before generating code based on this MultiSelect instance. Clears the _Generated field inside each PartialSelection.
		/// </summary>
		public void PrepareToGenerate()
		{
			foreach (PartialSelection selection in Selections)
				selection.Generated = false;
		}
		#endregion
		#region RefreshHighlighting
		/// <summary>
		/// Refreshes the block highlighting on screen for the specified TextView. Call this if the multi-select highlight options change.
		/// </summary>
		public void RefreshHighlighting(TextView textView)
		{
			foreach (PartialSelection selection in Selections)
				selection.AddHighlighter(textView);		// AddHighlighter will automatically remove any previous highlighting.
		}
		#endregion
    #region RemoveSelectionAt
		/// <summary>
		/// Removes the selection at the specified index. Selections are indexed by the order in which they are added.
		/// </summary>
		/// <param name="index">The index order in which this selection was added (0 is the first added selection). </param>
		public void RemoveSelectionAt(int index)
		{
			for (int i = 0; i < Selections.Count; i++)
				if (Selections[i]._Index == index)
				{
					Selections.RemoveAt(i);
					return;
				}
		}
		#endregion
		#region Sort
		/// <summary>
		/// Sorts the selections by the order in which they appear in the file.
		/// </summary>
		public void Sort()
		{
			Selections.Sort(new StartPointComparer());
		}
		#endregion
    #region ToString
		/// <summary>
		/// Converts this multi-select instance into a string with single-line comments (in the active language) marking properties.
		/// </summary>
		public override string ToString()
		{
			CommentBuilder commentBuilder = new CommentBuilder(Language);
			string asStr;
			asStr = commentBuilder.GetComment("FileName: ", FileName);
			asStr += commentBuilder.GetComment("Type: ", TypeName);
			asStr += commentBuilder.GetComment("Language: ", Language);
			asStr += commentBuilder.GetComment("WasCut: ", WasCut.ToString());
			asStr += commentBuilder.GetComment("ContainsOnlyMembers: ", ContainsOnlyMembers.ToString());
			asStr += commentBuilder.GetComment("Selections: ");
			foreach (PartialSelection selection in Selections)
				asStr += selection.ToString(commentBuilder);
			return asStr;
		}
		#endregion
		#region ToText
		/// <summary>
		/// Renders this MultiSelect instance as a continuous text string (all the partial selections appended together), suitable for pasting into a document.
		/// </summary>
		/// <returns></returns>
		public string ToText()
		{
			string result = String.Empty;
			foreach (PartialSelection selection in Selections)
				result += selection.ToText();
			return result;
		}
		#endregion

		// public properties...
		#region AllFields
		public IEnumerator AllFields
		{
			get
			{
				return new MultiSelectEnumerator(this, LanguageElementType.Variable);
			}
		}
		#endregion
		#region AllMethods
		public IEnumerator AllMethods
		{
			get
			{
				return new MultiSelectEnumerator(this, LanguageElementType.Method);
			}
		}
		#endregion
		#region AllProperties
		public IEnumerator AllProperties
		{
			get
			{
				return new MultiSelectEnumerator(this, LanguageElementType.Property);
			}
		}
		#endregion
		#region AllEvents
		public IEnumerator AllEvents
		{
			get
			{
				return new MultiSelectEnumerator(this, LanguageElementType.Event);
			}
		}
		#endregion
		#region AllNestedTypes
		public IEnumerator AllNestedTypes
		{
			get
			{
				return new MultiSelectEnumerator(this, LanguageElementType.Class);
			}
		}
		#endregion
		#region AllDelegates
		public IEnumerator AllDelegates
		{
			get
			{
				return new MultiSelectEnumerator(this, LanguageElementType.Delegate);
			}
		}
		#endregion
		#region AllEnums
		public IEnumerator AllEnums
		{
			get
			{
				return new MultiSelectEnumerator(this, LanguageElementType.Enum);
			}
		}
		#endregion
		#region AllConsts
		public IEnumerator AllConsts
		{
			get
			{
				return new MultiSelectEnumerator(this, LanguageElementType.Const);
			}
		}
		#endregion
	}
}

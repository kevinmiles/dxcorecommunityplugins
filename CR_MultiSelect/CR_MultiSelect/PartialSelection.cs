using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.PlugInCore.Adornments;
using DevExpress.DXCore.Platform.Drawing;
using DevExpress.CodeRush.Core;

namespace DevExpress.CodeRush.Core
{
  [Serializable]
	public class PartialSelection
	{
		[NonSerialized]
		private BlockHighlighter _SelectionHighlighter;
		[NonSerialized]
		internal int _Index;
		[NonSerialized]
		internal bool Generated;
		[NonSerialized]
		public SourcePoint CaretPosition;
		[NonSerialized]
		public SourceRange HighlightRange = SourceRange.Empty;
		
    public SourceRange Range = SourceRange.Empty;
		public string Text = String.Empty;
		public string ElementName = String.Empty;
		public LanguageElementType ElementType = LanguageElementType.Unknown;

		// constructors...
		#region PartialSelection
		public PartialSelection(int index)
		{
			_Index = index;
		}
		#endregion

		// public methods...
		#region AddHighlighter
		/// <summary>
		/// Adds a selection highlighter to the specified TextView (removing any previous highlighting associated with this selection).
		/// </summary>
		public void AddHighlighter(TextView textView)
		{
			RemoveHighlighter();
			_SelectionHighlighter = new BlockHighlighter() { DrawSelectionBars = true, FillRange = true, OutlineRange = true, SelectionBarStyle = SelectionBarStyle.Thin };
			_SelectionHighlighter.Select(textView, HighlightRange, CodeRushPlaceholder.MultiSelect.HighlightColor, 0.1, 0.3);
		}
		#endregion
    #region RemoveHighlighter
		/// <summary>
		/// Removes any highlighting associated with this selection.
		/// </summary>
		public void RemoveHighlighter()
		{
			if (_SelectionHighlighter == null)
				return;

			_SelectionHighlighter.DisposeSelectionAdornment();
			_SelectionHighlighter = null;
		}
		#endregion
		#region ToString
		/// <summary>
		/// Converts this PartialSelection instance into a string with single-line comments (in the active language) marking properties.
		/// </summary>
		public string ToString(CommentBuilder commentBuilder)
		{
			
			string asStr = commentBuilder.GetComment("  Element Name: ", ElementName);
			asStr += commentBuilder.GetComment("  ElementType: ", ElementType.ToString());
			asStr += commentBuilder.GetComment("  Range: ", Range.ToString());
			asStr += commentBuilder.GetComment("-----------");
			asStr += Text + Environment.NewLine;
			asStr += commentBuilder.GetComment("-----------");
			asStr += Environment.NewLine;
			return asStr;
		}
		#endregion
		#region ToText
		/// <summary>
		/// Renders this PartialSelection instance as a continuous text string (all the partial selections appended together), suitable for pasting into a document.
		/// </summary>
		public string ToText()
		{
			string result = Text;
			if (!result.EndsWith(Environment.NewLine))
				result += Environment.NewLine;
			return result;
		}
		#endregion
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.PlugInCore.Adornments;
using DevExpress.DXCore.Platform.Drawing;
using DevExpress.CodeRush.Core;

namespace CR_MultiSelect
{
  [Serializable]
	public class PartialSelection
	{
		[NonSerialized]
		private BlockHighlighter _SelectionHighlighter;

		public SourceRange Range = SourceRange.Empty;
		public string Text = String.Empty;
		public string ElementName = String.Empty;
		public LanguageElementType ElementType = LanguageElementType.Unknown;

		public void AddHighlighter(TextView textView)
		{
			RemoveHighlighter();
			_SelectionHighlighter = new BlockHighlighter() { DrawSelectionBars = true, FillRange = true, OutlineRange = true, SelectionBarStyle = SelectionBarStyle.Thin };
			_SelectionHighlighter.Select(textView, Range, Color.FromArgb(0x00, 0xBE, 0x8B), 0.1, 0.3);
		}

		public void RemoveHighlighter()
		{
			if (_SelectionHighlighter == null)
				return;

			_SelectionHighlighter.DisposeSelectionAdornment();
			_SelectionHighlighter = null;
		}

    public PartialSelection()
		{
		}
		
		public string ToText()
		{
			string result = Text;
			if (!result.EndsWith(Environment.NewLine))
				result += Environment.NewLine;
			return result;
		}
		
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
	}
}

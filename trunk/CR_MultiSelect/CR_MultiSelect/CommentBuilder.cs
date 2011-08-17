using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.CodeRush.Core;

namespace CR_MultiSelect
{
	public class CommentBuilder
	{
		public CommentBuilder(string language)
		{
			Language = language;
		}
		public string GetComment(string label)
		{
			string value = String.Empty;
			return GetComment(label, value);
		}
		public string GetComment(string label, string value)
		{
			return CodeRush.Language.GetComment(label + value, Language);
		}
		public string Language { get; private set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.StructuralParser;

namespace Refactor_Comments
{
	public static class LanguageElementExtensions
	{
		public static bool CanBeConvertedToMultiLineComment(this LanguageElement element)
		{
			var comment = element as Comment;

			// Element isn't a comment OR
			// Comment is already multiline OR
			// It's only a one-line comment.
			return
				comment == null ||
				comment.CommentType == CommentType.MultiLine ||
				(comment.PreviousConnectedComment == null && comment.NextConnectedComment == null);
		}

		public static bool CanBeConvertedToSingleLineComment(this LanguageElement element)
		{
			var comment = element as Comment;

			// Element isn't a comment OR
			// Comment is already single line.
			return
				comment == null ||
				comment.CommentType == CommentType.SingleLine;
		}
	}
}

using System;
using DevExpress.CodeRush.StructuralParser;

namespace Refactor_Comments
{
	public static class LanguageElementExtensions
	{
		public static bool CanBeConvertedToMultiLineComment(this LanguageElement element)
		{
			var comment = element as Comment;

			// In order to convert to a multiline comment, the element...
			// ...must be a comment AND
			// ...must be a single-line comment AND
			// ...must have an adjacent comment.
			return
				comment != null &&
				comment.CommentType == CommentType.SingleLine &&
				(comment.PreviousConnectedComment != null || comment.NextConnectedComment != null);
		}

		public static bool CanBeConvertedToSingleLineComment(this LanguageElement element)
		{
			var comment = element as Comment;

			// In order to convert to a multiline comment, the element...
			// ...must be a comment AND
			// ...must be a multi-line comment.
			return
				comment != null &&
				comment.CommentType == CommentType.MultiLine;
		}
	}
}

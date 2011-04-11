using System;
using System.ComponentModel;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.Refactor.Core;

namespace Refactor_Comments
{
	public partial class PlugIn1 : StandardPlugIn
	{
		public void CreateConvertToMultiLineComment()
		{
			var provider = new RefactoringProvider(_components);
			((ISupportInitialize)(provider)).BeginInit();
			provider.ProviderName = ProviderId.ConvertToMultiLineComment;
			provider.DisplayName = "Convert to Multiline Comment";
			provider.CheckAvailability += ConvertToMultilineComment_CheckAvailability;
			provider.LanguageSupported += CommentRefactoringsSupported;
			provider.Apply += ConvertToMultilineComment_Execute;
			((ISupportInitialize)(provider)).EndInit();
		}

		public void CreateConvertToSingleLineComments()
		{
			var provider = new RefactoringProvider(_components);
			((ISupportInitialize)(provider)).BeginInit();
			provider.ProviderName = ProviderId.ConvertToSingleLineComments;
			provider.DisplayName = "Convert To Singleline Comments";
			provider.CheckAvailability += ConvertToMultipleSingleLineComments_CheckAvailability;
			provider.LanguageSupported += CommentRefactoringsSupported;
			provider.Apply += ConvertToMultipleSingleLineComments_Execute;
			((ISupportInitialize)(provider)).EndInit();
		}

		public override void InitializePlugIn()
		{
			base.InitializePlugIn();
			CreateConvertToSingleLineComments();
			CreateConvertToMultiLineComment();
		}

		public override void FinalizePlugIn()
		{
			// TODO: Add your finalization code here.
			base.FinalizePlugIn();
		}

		private void ConvertToMultipleSingleLineComments_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			ea.Available = ea.CodeActive.CanBeConvertedToSingleLineComment();
		}

		private void ConvertToMultipleSingleLineComments_Execute(object sender, ApplyContentEventArgs ea)
		{
			var comment = ea.CodeActive as Comment;
			var commentRange = comment.Range;
			var activeDoc = CodeRush.Documents.ActiveTextDocument;
			string[] lines = comment.Name.Replace('\r', ' ').Split('\n');
			string newText = String.Empty;
			for (int i = 0; i < lines.Length; i++)
			{
				string commentText = lines[i].Trim();
				if (commentText != String.Empty)
				{
					newText += CodeRush.Language.GetComment(" " + commentText);
				}
			}
			activeDoc.QueueReplace(commentRange, newText);
			activeDoc.ApplyQueuedEdits("Convert to Singleline Comments", true);
		}

		private void ConvertToMultilineComment_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			ea.Available = ea.CodeActive.CanBeConvertedToMultiLineComment();
		}

		private void ConvertToMultilineComment_Execute(object sender, ApplyContentEventArgs ea)
		{
			var comment = ea.CodeActive as Comment;
			Comment firstComment;
			Comment lastComment;
			comment.GetFirstAndLastConnectedComments(out firstComment, out lastComment);

			string commentText = Environment.NewLine;
			var currentComment = firstComment;
			string whitespace = CodeRush.Documents.GetLeadingWhiteSpace(firstComment.Range.Start.Line);
			do
			{
				commentText += whitespace + currentComment.Name + Environment.NewLine;
				currentComment = currentComment.NextConnectedComment;
			} while (currentComment != null);

			commentText += whitespace;

			// Determine First Comment
			var newComment = GetCommentMultiline(commentText);
			SourceRange commentRange = new SourceRange(firstComment.Range.Start, lastComment.Range.End);
			var activeDoc = CodeRush.Documents.ActiveTextDocument;
			activeDoc.QueueReplace(commentRange, newComment);
			activeDoc.ApplyQueuedEdits("Convert to Multiline Comment", true);
		}

		private void CommentRefactoringsSupported(LanguageSupportedEventArgs ea)
		{
			ea.Handled = ea.LanguageID.SupportsMultiLineComments();
		}

		private string GetCommentMultiline(string commentText)
		{
			var comment = new Comment()
			{
				Name = commentText,
				CommentType = CommentType.MultiLine
			};
			return CodeRush.CodeMod.GenerateCode(comment);
		}
	}
}
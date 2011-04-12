using System;
using System.ComponentModel;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.Refactor.Core;
using System.Text;

namespace Refactor_Comments
{
	public partial class PlugIn1 : StandardPlugIn
	{
		public void CreateConvertToMultiLineComment()
		{
			var provider = new RefactoringProvider(_components);
			((ISupportInitialize)(provider)).BeginInit();
			provider.ProviderName = ProviderId.ConvertToMultiLineComment;
			provider.DisplayName = Properties.Resources.ConvertToMultiLineComment_DisplayName;
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
			provider.DisplayName = Properties.Resources.ConvertToSingleLineComment_DisplayName;
			provider.CheckAvailability += ConvertToSingleLineComments_CheckAvailability;
			provider.LanguageSupported += CommentRefactoringsSupported;
			provider.Apply += ConvertToSingleLineComments_Execute;
			((ISupportInitialize)(provider)).EndInit();
		}

		private void CommentRefactoringsSupported(LanguageSupportedEventArgs ea)
		{
			ea.Handled = ea.LanguageID.SupportsMultiLineComments();
		}

		private void ConvertToMultilineComment_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			ea.Available = ea.CodeActive.CanBeConvertedToMultiLineComment();
		}

		private void ConvertToMultilineComment_Execute(object sender, ApplyContentEventArgs ea)
		{
			// TODO: Create a custom code generator for multiline comments that allows for our style options.
			// DevExpress.CodeRush.StructuralParser.SupportElementCodeGenBase : LanguageElementCodeGenBase

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
			activeDoc.ApplyQueuedEdits(ProviderId.ConvertToMultiLineComment, true);
		}

		private void ConvertToSingleLineComments_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
		{
			ea.Available = ea.CodeActive.CanBeConvertedToSingleLineComment();
		}

		private void ConvertToSingleLineComments_Execute(object sender, ApplyContentEventArgs ea)
		{
			var comment = ea.CodeActive as Comment;
			if (comment == null)
			{
				return;
			}

			// Normalize the line endings so we can split properly.
			string[] lines = comment.Name.Replace("\r\n", "\n").Split('\n');

			var builder = new StringBuilder();
			for (int i = 0; i < lines.Length; i++)
			{
				// TODO: Test a comment that has leading spaces that should be kept.
				// TODO: Test a comment that has the leading * that you usually get.
				string commentText = lines[i].Trim();
				if (!String.IsNullOrEmpty(commentText))
				{
					builder.Append(CodeRush.Language.GetComment(" " + commentText));
				}
			}

			var commentRange = comment.Range;
			var activeDoc = CodeRush.Documents.ActiveTextDocument;
			activeDoc.QueueReplace(commentRange, builder.ToString());
			activeDoc.ApplyQueuedEdits(ProviderId.ConvertToSingleLineComments, true);
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
	}
}
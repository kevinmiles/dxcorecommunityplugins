using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.Refactor;

namespace Refactor_Comments
{
    public partial class PlugIn1 : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            CreateConvertToMultipleSingleLineComments();
            CreateConvertToMultilineComment();
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

        #region CreateConvertToMultipleSingleLineComments
        public void CreateConvertToMultipleSingleLineComments()
        {
            DevExpress.Refactor.Core.RefactoringProvider ConvertToMultipleSingleLineComments = new DevExpress.Refactor.Core.RefactoringProvider(components);
            ((System.ComponentModel.ISupportInitialize)(ConvertToMultipleSingleLineComments)).BeginInit();
            ConvertToMultipleSingleLineComments.ProviderName = "ConvertToMultipleSinglelineComments"; // Should be Unique
            ConvertToMultipleSingleLineComments.DisplayName = "Convert To Singleline Comments";
            ConvertToMultipleSingleLineComments.CheckAvailability += ConvertToMultipleSingleLineComments_CheckAvailability;
            ConvertToMultipleSingleLineComments.LanguageSupported += ConvertToMultipleSingleLineComments_LanguageSupported;
            ConvertToMultipleSingleLineComments.Apply += ConvertToMultipleSingleLineComments_Execute;
            ((System.ComponentModel.ISupportInitialize)(ConvertToMultipleSingleLineComments)).EndInit();
        }
        #endregion
        #region ConvertToMultipleSingleLineComments_LanguageSupported
        private void ConvertToMultipleSingleLineComments_LanguageSupported(LanguageSupportedEventArgs ea)
        {
            ea.Handled = CodeRush.Language.SupportsMultiLineComments(ea.LanguageID) || ea.LanguageID == "JavaScript";
        }
        #endregion
        #region ConvertToMultipleSingleLineComments_CheckAvailability
        private void ConvertToMultipleSingleLineComments_CheckAvailability(Object sender, CheckContentAvailabilityEventArgs ea)
        {
            var Comment = ea.CodeActive as Comment;
            if (Comment == null)
                return;
            if (Comment.CommentType == CommentType.SingleLine)
                return;
            ea.Available = true;
        }
        #endregion
        #region ConvertToMultipleSingleLineComments_Execute
        private void ConvertToMultipleSingleLineComments_Execute(Object sender, ApplyContentEventArgs ea)
        {
            var Comment = ea.CodeActive as Comment;
            var CommentRange = Comment.Range;
            var ActiveDoc = CodeRush.Documents.ActiveTextDocument;
            string[] Lines = Comment.Name.Replace('\r', ' ').Split('\n');
            string NewText = String.Empty;
            for (int i = 0; i < Lines.Length; i++)
            {
                string CommentText = Lines[i].Trim();
                if (CommentText != String.Empty)
                    NewText += CodeRush.Language.GetComment(" " + CommentText);
            }
            ActiveDoc.QueueReplace(CommentRange, NewText);
            ActiveDoc.ApplyQueuedEdits("Convert to Singleline comments", true);
        }
        #endregion


        #region CreateConvertToMultilineComment
        public void CreateConvertToMultilineComment()
        {
            DevExpress.Refactor.Core.RefactoringProvider ConvertToMultilineComment = new DevExpress.Refactor.Core.RefactoringProvider(components);
            ((System.ComponentModel.ISupportInitialize)(ConvertToMultilineComment)).BeginInit();
            ConvertToMultilineComment.ProviderName = "ConvertToMultilineComment"; // Should be Unique
            ConvertToMultilineComment.DisplayName = "Convert to Multiline Comment";
            ConvertToMultilineComment.CheckAvailability += ConvertToMultilineComment_CheckAvailability;
            ConvertToMultilineComment.LanguageSupported += ConvertToMultilineComment_LanguageSupported;
            ConvertToMultilineComment.Apply += ConvertToMultilineComment_Execute;
            ((System.ComponentModel.ISupportInitialize)(ConvertToMultilineComment)).EndInit();
        }
        #endregion
        #region ConvertToMultilineComment_LanguageSupported
        private void ConvertToMultilineComment_LanguageSupported(LanguageSupportedEventArgs ea)
        {
            ea.Handled = CodeRush.Language.SupportsMultiLineComments(ea.LanguageID) || ea.LanguageID == "JavaScript";
        }
        #endregion
        #region ConvertToMultilineComment_CheckAvailability
        private void ConvertToMultilineComment_CheckAvailability(Object sender, CheckContentAvailabilityEventArgs ea)
        {
            var Comment = ea.CodeActive as Comment;
            if (Comment == null)
                return; // exit because the active element is not a comment
            if (Comment.CommentType == CommentType.MultiLine)
                return; // exit because this comment is already multiline
            if (Comment.PreviousConnectedComment == null && Comment.NextConnectedComment == null)
                return; // Exit because no connected comments
            ea.Available = true; // Change this to return true, only when your refactoring should be available.
        }
        #endregion
        #region ConvertToMultilineComment_Execute
        private void ConvertToMultilineComment_Execute(Object sender, ApplyContentEventArgs ea)
        {
            var Comment = ea.CodeActive as Comment;
            Comment FirstComment;
            Comment LastComment;
            Comment.GetFirstAndLastConnectedComments(out FirstComment, out LastComment);

            string CommentText = Environment.NewLine;
            Comment CurrentComment = FirstComment;
            string Whitespace = CodeRush.Documents.GetLeadingWhiteSpace(FirstComment.Range.Start.Line);
            do
            {
                CommentText += Whitespace + CurrentComment.Name + Environment.NewLine;
                CurrentComment = CurrentComment.NextConnectedComment;
            } while (CurrentComment != null);

            CommentText += Whitespace;
            // Determine First Comment

            var NewComment = GetCommentMultiline(CommentText);
            SourceRange CommentRange = new SourceRange(FirstComment.Range.Start, LastComment.Range.End);
            var ActiveDoc = CodeRush.Documents.ActiveTextDocument;
            ActiveDoc.QueueReplace(CommentRange, NewComment);
            ActiveDoc.ApplyQueuedEdits("Convert to Multiline Comment", true);
        }
        #endregion

        #region GetCommentMultiline
        private string GetCommentMultiline(string CommentText)
        {
            Comment Comment = new Comment() { Name = CommentText, CommentType = CommentType.MultiLine };
            return CodeRush.CodeMod.GenerateCode(Comment);
        }
        #endregion

    }
}
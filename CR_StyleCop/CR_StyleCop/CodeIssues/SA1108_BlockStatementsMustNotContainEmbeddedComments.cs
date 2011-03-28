namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1108_BlockStatementsMustNotContainEmbeddedComments : StyleCopRule
    {
        public SA1108_BlockStatementsMustNotContainEmbeddedComments()
            : base(new SingleLineCommentIssueLocator())
        {
        }
    }
}

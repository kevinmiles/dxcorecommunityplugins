namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1512_SingleLineCommentsMustNotBeFollowedByBlankLine : StyleCopRule
    {
        public SA1512_SingleLineCommentsMustNotBeFollowedByBlankLine()
            : base(new SingleLineCommentIssueLocator())
        {
        }
    }
}

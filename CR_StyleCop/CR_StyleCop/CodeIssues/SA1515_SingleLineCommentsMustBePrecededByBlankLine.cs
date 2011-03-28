namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1515_SingleLineCommentsMustBePrecededByBlankLine : StyleCopRule
    {
        public SA1515_SingleLineCommentsMustBePrecededByBlankLine()
            : base(new SingleLineCommentIssueLocator())
        {
        }
    }
}

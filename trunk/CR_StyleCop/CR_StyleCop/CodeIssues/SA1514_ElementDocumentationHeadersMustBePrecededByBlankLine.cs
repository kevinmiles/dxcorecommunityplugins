namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1514_ElementDocumentationHeadersMustBePrecededByBlankLine : StyleCopRule
    {
        public SA1514_ElementDocumentationHeadersMustBePrecededByBlankLine()
            : base(new SingleLineCommentIssueLocator())
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1111_ClosingParenthesisMustBeOnLineOfLastParameter : KeywordCodeIssue
    {
        public SA1111_ClosingParenthesisMustBeOnLineOfLastParameter()
            : base(Underline.FirstKeywordOnLine, ")", "]")
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1112_ClosingParenthesisMustBeOnLineOfOpeningParenthesis : KeywordCodeIssue
    {
        public SA1112_ClosingParenthesisMustBeOnLineOfOpeningParenthesis()
            : base(Underline.FirstKeywordOnLine, ")", "]")
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1110_OpeningParenthesisMustBeOnDeclarationLine : KeywordCodeIssue
    {
        public SA1110_OpeningParenthesisMustBeOnDeclarationLine()
            : base(Underline.FirstKeywordOnLine, "(", "[")
        {
        }
    }
}

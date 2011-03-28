namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1112_ClosingParenthesisMustBeOnLineOfOpeningParenthesis : StyleCopRule
    {
        public SA1112_ClosingParenthesisMustBeOnLineOfOpeningParenthesis()
            : base(new FirstTokenByTypeIssueLocator(CsTokenType.CloseParenthesis, CsTokenType.CloseSquareBracket))
        {
        }
    }
}

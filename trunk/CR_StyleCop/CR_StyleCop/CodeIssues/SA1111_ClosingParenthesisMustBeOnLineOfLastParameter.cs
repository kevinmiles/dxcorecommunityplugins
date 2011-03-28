namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1111_ClosingParenthesisMustBeOnLineOfLastParameter : StyleCopRule
    {
        public SA1111_ClosingParenthesisMustBeOnLineOfLastParameter()
            : base(new FirstTokenByTypeIssueLocator(CsTokenType.CloseParenthesis, CsTokenType.CloseSquareBracket))
        {
        }
    }
}

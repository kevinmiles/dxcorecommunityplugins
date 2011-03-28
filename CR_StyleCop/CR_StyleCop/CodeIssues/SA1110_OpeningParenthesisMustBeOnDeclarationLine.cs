namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1110_OpeningParenthesisMustBeOnDeclarationLine : StyleCopRule
    {
        public SA1110_OpeningParenthesisMustBeOnDeclarationLine()
            : base(new FirstTokenByTypeIssueLocator(CsTokenType.OpenParenthesis, CsTokenType.OpenSquareBracket))
        {
        }
    }
}

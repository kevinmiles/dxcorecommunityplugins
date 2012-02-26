namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1513_ClosingCurlyBracketsMustNotBeFollowedByBlankLine : StyleCopRule
    {
        public SA1513_ClosingCurlyBracketsMustNotBeFollowedByBlankLine()
            : base(new LastTokenByTypeIssueLocator(ElementTokens, CsTokenType.CloseCurlyBracket))
        {
        }
    }
}

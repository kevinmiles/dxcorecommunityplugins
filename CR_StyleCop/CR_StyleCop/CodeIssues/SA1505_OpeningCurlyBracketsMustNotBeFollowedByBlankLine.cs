namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1505_OpeningCurlyBracketsMustNotBeFollowedByBlankLine : StyleCopRule
    {
        public SA1505_OpeningCurlyBracketsMustNotBeFollowedByBlankLine()
            : base(new LastTokenByTypeIssueLocator(CsTokenType.OpenCurlyBracket))
        {
        }
    }
}

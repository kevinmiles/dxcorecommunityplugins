namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1508_ClosingCurlyBracketsMustNotBePrecededByBlankLine : StyleCopRule
    {
        public SA1508_ClosingCurlyBracketsMustNotBePrecededByBlankLine()
            : base(new FirstTokenByTypeIssueLocator(CsTokenType.CloseCurlyBracket))
        {
        }
    }
}

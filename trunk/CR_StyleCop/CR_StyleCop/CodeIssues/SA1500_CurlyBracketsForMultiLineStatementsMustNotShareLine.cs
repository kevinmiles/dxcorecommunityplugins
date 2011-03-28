namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1500_CurlyBracketsForMultiLineStatementsMustNotShareLine : StyleCopRule
    {
        public SA1500_CurlyBracketsForMultiLineStatementsMustNotShareLine()
            : base(new AllTokensByTypeIssueLocator(CsTokenType.OpenCurlyBracket, CsTokenType.CloseCurlyBracket))
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1510_ChainedStatementBlocksMustNotBePrecededByBlankLine : StyleCopRule
    {
        public SA1510_ChainedStatementBlocksMustNotBePrecededByBlankLine()
            : base(new FirstTokenByTypeIssueLocator(CsTokenType.Else, CsTokenType.Catch, CsTokenType.Finally))
        {
        }
    }
}

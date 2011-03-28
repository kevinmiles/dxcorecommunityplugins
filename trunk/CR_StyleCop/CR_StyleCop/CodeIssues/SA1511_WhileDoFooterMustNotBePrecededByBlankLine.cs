namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1511_WhileDoFooterMustNotBePrecededByBlankLine : StyleCopRule
    {
        public SA1511_WhileDoFooterMustNotBePrecededByBlankLine()
            : base(new FirstTokenByTypeIssueLocator(CsTokenType.WhileDo))
        {
        }
    }
}

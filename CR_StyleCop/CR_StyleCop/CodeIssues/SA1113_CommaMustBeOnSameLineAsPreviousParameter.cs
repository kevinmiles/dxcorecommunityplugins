namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1113_CommaMustBeOnSameLineAsPreviousParameter : StyleCopRule
    {
        public SA1113_CommaMustBeOnSameLineAsPreviousParameter()
            : base(new FirstTokenByTypeIssueLocator(CsTokenType.Comma))
        {
        }
    }
}

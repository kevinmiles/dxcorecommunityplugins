namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1504_AllAccessorsMustBeSingleLineOrMultiLine : StyleCopRule
    {
        public SA1504_AllAccessorsMustBeSingleLineOrMultiLine()
            : base(new FirstTokenByTypeIssueLocator(CsTokenType.Get, CsTokenType.Set, CsTokenType.Add, CsTokenType.Remove))
        {
        }
    }
}

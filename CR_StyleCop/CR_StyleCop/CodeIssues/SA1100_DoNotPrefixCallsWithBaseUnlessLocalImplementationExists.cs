namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1100_DoNotPrefixCallsWithBaseUnlessLocalImplementationExists : StyleCopRule
    {
        public SA1100_DoNotPrefixCallsWithBaseUnlessLocalImplementationExists()
            : base(new FirstTokenByTypeIssueLocator(CsTokenType.Base))
        {
        }
    }
}

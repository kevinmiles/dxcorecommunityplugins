namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1100_DoNotPrefixCallsWithBaseUnlessLocalImplementationExists : KeywordCodeIssue
    {
        public SA1100_DoNotPrefixCallsWithBaseUnlessLocalImplementationExists()
            : base("base")
        {
        }
    }
}

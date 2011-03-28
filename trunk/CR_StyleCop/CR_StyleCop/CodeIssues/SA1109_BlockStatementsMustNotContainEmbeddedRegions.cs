namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1109_BlockStatementsMustNotContainEmbeddedRegions : StyleCopRule
    {
        public SA1109_BlockStatementsMustNotContainEmbeddedRegions()
            : base(new PreprocessorDirectiveIssueLocator())
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1647_IncludeNodeDoesNotContainValidFileAndPath : StyleCopRule
    {
        public SA1647_IncludeNodeDoesNotContainValidFileAndPath()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

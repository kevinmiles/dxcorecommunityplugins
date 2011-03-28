namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1644_DocumentationHeadersMustNotContainBlankLines : StyleCopRule
    {
        public SA1644_DocumentationHeadersMustNotContainBlankLines()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

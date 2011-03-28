namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1607_PartialElementDocumentationMustHaveSummaryText : StyleCopRule
    {
        public SA1607_PartialElementDocumentationMustHaveSummaryText()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

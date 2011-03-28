namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1606_ElementDocumentationMustHaveSummaryText : StyleCopRule
    {
        public SA1606_ElementDocumentationMustHaveSummaryText()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

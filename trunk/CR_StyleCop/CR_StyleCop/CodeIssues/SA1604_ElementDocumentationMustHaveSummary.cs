namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1604_ElementDocumentationMustHaveSummary : StyleCopRule
    {
        public SA1604_ElementDocumentationMustHaveSummary()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1605_PartialElementDocumentationMustHaveSummary : StyleCopRule
    {
        public SA1605_PartialElementDocumentationMustHaveSummary()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1624_PropertySummaryDocumentationMustOmitSetAccessorWithRestrictedAccess : StyleCopRule
    {
        public SA1624_PropertySummaryDocumentationMustOmitSetAccessorWithRestrictedAccess()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

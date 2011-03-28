namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1623_PropertySummaryDocumentationMustMatchAccessors : StyleCopRule
    {
        public SA1623_PropertySummaryDocumentationMustMatchAccessors()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

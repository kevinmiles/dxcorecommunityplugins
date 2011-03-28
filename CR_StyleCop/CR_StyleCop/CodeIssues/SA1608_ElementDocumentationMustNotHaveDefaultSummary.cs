namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1608_ElementDocumentationMustNotHaveDefaultSummary : StyleCopRule
    {
        public SA1608_ElementDocumentationMustNotHaveDefaultSummary()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

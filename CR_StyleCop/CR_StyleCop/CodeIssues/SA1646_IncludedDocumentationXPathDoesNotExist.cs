namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1646_IncludedDocumentationXPathDoesNotExist : StyleCopRule
    {
        public SA1646_IncludedDocumentationXPathDoesNotExist()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

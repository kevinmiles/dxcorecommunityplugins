namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1645_IncludedDocumentationFileDoesNotExist : StyleCopRule
    {
        public SA1645_IncludedDocumentationFileDoesNotExist()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1609_PropertyDocumentationMustHaveValue : StyleCopRule
    {
        public SA1609_PropertyDocumentationMustHaveValue()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1610_PropertyDocumentationMustHaveValueText : StyleCopRule
    {
        public SA1610_PropertyDocumentationMustHaveValueText()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

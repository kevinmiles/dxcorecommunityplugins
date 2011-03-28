namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1614_ElementParameterDocumentationMustHaveText : StyleCopRule
    {
        public SA1614_ElementParameterDocumentationMustHaveText()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

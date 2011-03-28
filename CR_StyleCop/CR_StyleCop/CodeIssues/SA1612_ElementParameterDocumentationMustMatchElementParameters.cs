namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1612_ElementParameterDocumentationMustMatchElementParameters : StyleCopRule
    {
        public SA1612_ElementParameterDocumentationMustMatchElementParameters()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

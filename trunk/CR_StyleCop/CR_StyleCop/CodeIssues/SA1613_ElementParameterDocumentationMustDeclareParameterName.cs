namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1613_ElementParameterDocumentationMustDeclareParameterName : StyleCopRule
    {
        public SA1613_ElementParameterDocumentationMustDeclareParameterName()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

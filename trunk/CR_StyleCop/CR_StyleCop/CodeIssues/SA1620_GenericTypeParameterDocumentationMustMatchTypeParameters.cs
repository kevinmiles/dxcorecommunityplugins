namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1620_GenericTypeParameterDocumentationMustMatchTypeParameters : StyleCopRule
    {
        public SA1620_GenericTypeParameterDocumentationMustMatchTypeParameters()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

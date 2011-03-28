namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1622_GenericTypeParameterDocumentationMustHaveText : StyleCopRule
    {
        public SA1622_GenericTypeParameterDocumentationMustHaveText()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

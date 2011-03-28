namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1621_GenericTypeParameterDocumentationMustDeclareParameterName : StyleCopRule
    {
        public SA1621_GenericTypeParameterDocumentationMustDeclareParameterName()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

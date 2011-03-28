namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1619_GenericTypeParametersMustBeDocumentedPartialClass : StyleCopRule
    {
        public SA1619_GenericTypeParametersMustBeDocumentedPartialClass()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

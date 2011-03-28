namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1618_GenericTypeParametersMustBeDocumented : StyleCopRule
    {
        public SA1618_GenericTypeParametersMustBeDocumented()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

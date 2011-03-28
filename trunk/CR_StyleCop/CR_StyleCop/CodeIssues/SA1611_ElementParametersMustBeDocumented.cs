namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1611_ElementParametersMustBeDocumented : StyleCopRule
    {
        public SA1611_ElementParametersMustBeDocumented()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

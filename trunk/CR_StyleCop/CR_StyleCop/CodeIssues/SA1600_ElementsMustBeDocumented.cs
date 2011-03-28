namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1600_ElementsMustBeDocumented : StyleCopRule
    {
        public SA1600_ElementsMustBeDocumented()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

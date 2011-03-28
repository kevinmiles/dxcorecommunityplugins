namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1601_PartialElementsMustBeDocumented : StyleCopRule
    {
        public SA1601_PartialElementsMustBeDocumented()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

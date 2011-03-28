namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1602_EnumerationItemsMustBeDocumented : StyleCopRule
    {
        public SA1602_EnumerationItemsMustBeDocumented()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

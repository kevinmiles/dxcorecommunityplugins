namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1302_InterfaceNamesMustBeginWithI : StyleCopRule
    {
        public SA1302_InterfaceNamesMustBeginWithI()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

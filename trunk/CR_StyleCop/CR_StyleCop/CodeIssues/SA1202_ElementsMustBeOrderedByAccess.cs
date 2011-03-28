namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1202_ElementsMustBeOrderedByAccess : StyleCopRule
    {
        public SA1202_ElementsMustBeOrderedByAccess()
            : base(new WrongElementOrderIssueLocator())
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1201_ElementsMustAppearInTheCorrectOrder : StyleCopRule
    {
        public SA1201_ElementsMustAppearInTheCorrectOrder()
            : base(new WrongElementOrderIssueLocator())
        {
        }
    }
}

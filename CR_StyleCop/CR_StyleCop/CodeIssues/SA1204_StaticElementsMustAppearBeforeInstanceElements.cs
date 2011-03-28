namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1204_StaticElementsMustAppearBeforeInstanceElements : StyleCopRule
    {
        public SA1204_StaticElementsMustAppearBeforeInstanceElements()
            : base(new WrongElementOrderIssueLocator())
        {
        }
    }
}

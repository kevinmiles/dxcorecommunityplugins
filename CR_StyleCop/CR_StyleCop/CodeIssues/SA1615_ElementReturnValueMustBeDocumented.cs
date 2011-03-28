namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1615_ElementReturnValueMustBeDocumented : StyleCopRule
    {
        public SA1615_ElementReturnValueMustBeDocumented()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

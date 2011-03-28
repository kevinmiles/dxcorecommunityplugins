namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1617_VoidReturnValueMustNotBeDocumented : StyleCopRule
    {
        public SA1617_VoidReturnValueMustNotBeDocumented()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

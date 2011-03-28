namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1504_AllAccessorsMustBeSingleLineOrMultiLine : StyleCopRule
    {
        public SA1504_AllAccessorsMustBeSingleLineOrMultiLine()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

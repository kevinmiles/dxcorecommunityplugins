namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1516_ElementsMustBeSeparatedByBlankLine : StyleCopRule
    {
        public SA1516_ElementsMustBeSeparatedByBlankLine()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

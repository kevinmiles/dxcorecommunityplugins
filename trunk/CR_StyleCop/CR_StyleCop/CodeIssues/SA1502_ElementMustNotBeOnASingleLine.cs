namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1502_ElementMustNotBeOnASingleLine : StyleCopRule
    {
        public SA1502_ElementMustNotBeOnASingleLine()
            : base(new WholeElementIssueLocator())
        {
        }
    }
}

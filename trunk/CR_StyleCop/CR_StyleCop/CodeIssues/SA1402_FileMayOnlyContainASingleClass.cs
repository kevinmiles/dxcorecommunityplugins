namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1402_FileMayOnlyContainASingleClass : StyleCopRule
    {
        public SA1402_FileMayOnlyContainASingleClass()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

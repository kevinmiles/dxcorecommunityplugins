namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1307_AccessibleFieldsMustBeginWithUpperCaseLetter : StyleCopRule
    {
        public SA1307_AccessibleFieldsMustBeginWithUpperCaseLetter()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

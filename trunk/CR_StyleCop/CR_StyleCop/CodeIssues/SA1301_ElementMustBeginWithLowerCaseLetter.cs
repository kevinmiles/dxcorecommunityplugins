namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1301_ElementMustBeginWithLowerCaseLetter : StyleCopRule
    {
        public SA1301_ElementMustBeginWithLowerCaseLetter()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1304_NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter : StyleCopRule
    {
        public SA1304_NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

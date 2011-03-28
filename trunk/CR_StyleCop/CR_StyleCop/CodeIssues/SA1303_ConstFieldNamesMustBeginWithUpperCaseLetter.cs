namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1303_ConstFieldNamesMustBeginWithUpperCaseLetter : StyleCopRule
    {
        public SA1303_ConstFieldNamesMustBeginWithUpperCaseLetter()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

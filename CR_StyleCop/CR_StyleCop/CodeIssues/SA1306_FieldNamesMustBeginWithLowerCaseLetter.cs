namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1306_FieldNamesMustBeginWithLowerCaseLetter : StyleCopRule
    {
        public SA1306_FieldNamesMustBeginWithLowerCaseLetter()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1309_FieldNamesMustNotBeginWithUnderscore : StyleCopRule
    {
        public SA1309_FieldNamesMustNotBeginWithUnderscore()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

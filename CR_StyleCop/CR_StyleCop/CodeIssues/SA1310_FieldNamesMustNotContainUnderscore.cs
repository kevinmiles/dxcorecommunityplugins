namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1310_FieldNamesMustNotContainUnderscore : StyleCopRule
    {
        public SA1310_FieldNamesMustNotContainUnderscore()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

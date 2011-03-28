namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1308_VariableNamesMustNotBePrefixed : StyleCopRule
    {
        public SA1308_VariableNamesMustNotBePrefixed()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1400_AccessModifierMustBeDeclared : StyleCopRule
    {
        public SA1400_AccessModifierMustBeDeclared()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

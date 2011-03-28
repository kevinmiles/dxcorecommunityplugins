namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1209_UsingAliasDirectivesMustBePlacedAfterOtherUsingDirectives : StyleCopRule
    {
        public SA1209_UsingAliasDirectivesMustBePlacedAfterOtherUsingDirectives()
            : base(new UsingDirectiveCodeIssue())
        {
        }
    }
}

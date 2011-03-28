namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1208_SystemUsingDirectivesMustBePlacedBeforeOtherUsingDirectives : StyleCopRule
    {
        public SA1208_SystemUsingDirectivesMustBePlacedBeforeOtherUsingDirectives()
            : base(new UsingDirectiveCodeIssue())
        {
        }
    }
}

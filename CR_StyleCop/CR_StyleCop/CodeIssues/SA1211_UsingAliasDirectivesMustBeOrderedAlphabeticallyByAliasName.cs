namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1211_UsingAliasDirectivesMustBeOrderedAlphabeticallyByAliasName : StyleCopRule
    {
        public SA1211_UsingAliasDirectivesMustBeOrderedAlphabeticallyByAliasName()
            : base(new UsingDirectiveCodeIssue())
        {
        }
    }
}

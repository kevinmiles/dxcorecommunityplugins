namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1207_ProtectedMustComeBeforeInternal : KeywordCodeIssue
    {
        public SA1207_ProtectedMustComeBeforeInternal()
            : base(new[] {"protected", "internal" })
        {
        }
    }
}

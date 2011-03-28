namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1210_UsingDirectivesMustBeOrderedAlphabeticallyByNamespace : StyleCopRule
    {
        public SA1210_UsingDirectivesMustBeOrderedAlphabeticallyByNamespace()
            : base(new UsingDirectiveCodeIssue())
        {
        }
    }
}

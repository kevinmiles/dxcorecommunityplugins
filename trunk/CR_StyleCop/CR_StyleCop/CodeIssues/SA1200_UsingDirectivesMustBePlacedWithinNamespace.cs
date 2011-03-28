namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1200_UsingDirectivesMustBePlacedWithinNamespace : StyleCopRule
    {
        public SA1200_UsingDirectivesMustBePlacedWithinNamespace()
            : base(new UsingDirectiveCodeIssue())
        {
        }
    }
}

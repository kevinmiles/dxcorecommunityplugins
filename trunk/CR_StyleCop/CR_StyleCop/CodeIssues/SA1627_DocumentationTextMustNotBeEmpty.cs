namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1627_DocumentationTextMustNotBeEmpty : StyleCopRule
    {
        public SA1627_DocumentationTextMustNotBeEmpty()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

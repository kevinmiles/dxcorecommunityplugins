namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1625_ElementDocumentationMustNotBeCopiedAndPasted : StyleCopRule
    {
        public SA1625_ElementDocumentationMustNotBeCopiedAndPasted()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

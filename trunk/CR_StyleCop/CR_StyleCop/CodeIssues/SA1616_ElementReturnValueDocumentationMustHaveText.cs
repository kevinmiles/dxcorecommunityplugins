namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1616_ElementReturnValueDocumentationMustHaveText : StyleCopRule
    {
        public SA1616_ElementReturnValueDocumentationMustHaveText()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

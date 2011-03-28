namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1642_ConstructorSummaryDocumentationMustBeginWithStandardText : StyleCopRule
    {
        public SA1642_ConstructorSummaryDocumentationMustBeginWithStandardText()
            : base(new ElementNameIssueLocator())
        {
        }
    }
}

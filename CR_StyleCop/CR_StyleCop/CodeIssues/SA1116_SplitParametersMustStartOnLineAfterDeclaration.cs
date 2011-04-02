namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1116_SplitParametersMustStartOnLineAfterDeclaration : StyleCopRule
    {
        public SA1116_SplitParametersMustStartOnLineAfterDeclaration()
            : base(new FirstParameterIssueLocator())
        {
        }
    }
}

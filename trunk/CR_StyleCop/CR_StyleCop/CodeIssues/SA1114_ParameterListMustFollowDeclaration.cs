namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1114_ParameterListMustFollowDeclaration : StyleCopRule
    {
        public SA1114_ParameterListMustFollowDeclaration()
            : base(new FirstParameterIssueLocator())
        {
        }
    }
}

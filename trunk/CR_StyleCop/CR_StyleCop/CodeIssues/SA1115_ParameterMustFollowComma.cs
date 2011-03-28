namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1115_ParameterMustFollowComma : StyleCopRule
    {
        public SA1115_ParameterMustFollowComma()
            : base(new FirstParameterIssueLocator())
        {
        }
    }
}

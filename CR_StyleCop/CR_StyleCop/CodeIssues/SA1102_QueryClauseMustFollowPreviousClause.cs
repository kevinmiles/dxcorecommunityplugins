namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1102_QueryClauseMustFollowPreviousClause : StyleCopRule
    {
        public SA1102_QueryClauseMustFollowPreviousClause()
            : base(new FirstTokenByTypeIssueLocator(Keywords.QueryExpression))
        {
        }
    }
}

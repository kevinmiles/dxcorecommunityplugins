namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1102_QueryClauseMustFollowPreviousClause : QueryExpressionCodeIssue
    {
        public SA1102_QueryClauseMustFollowPreviousClause()
            : base(Underline.FirstKeywordOnLine)
        {
        }
    }
}

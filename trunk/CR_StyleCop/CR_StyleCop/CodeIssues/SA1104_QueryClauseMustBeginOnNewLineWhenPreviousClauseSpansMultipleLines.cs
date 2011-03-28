namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1104_QueryClauseMustBeginOnNewLineWhenPreviousClauseSpansMultipleLines : StyleCopRule
    {
        public SA1104_QueryClauseMustBeginOnNewLineWhenPreviousClauseSpansMultipleLines()
            : base(new FirstTokenByTypeIssueLocator(Keywords.QueryExpression))
        {
        }
    }
}

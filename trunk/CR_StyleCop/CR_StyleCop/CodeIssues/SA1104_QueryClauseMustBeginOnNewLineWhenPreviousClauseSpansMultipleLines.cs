namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1104_QueryClauseMustBeginOnNewLineWhenPreviousClauseSpansMultipleLines : QueryExpressionCodeIssue
    {
        public SA1104_QueryClauseMustBeginOnNewLineWhenPreviousClauseSpansMultipleLines()
            : base(Underline.FirstKeywordOnLine)
        {
        }
    }
}

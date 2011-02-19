namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1105_QueryClausesSpanningMultipleLinesMustBeginOnOwnLine : QueryExpressionCodeIssue
    {
        public SA1105_QueryClausesSpanningMultipleLinesMustBeginOnOwnLine()
            : base(Underline.LastKeywordOnLine)
        {
        }
    }
}

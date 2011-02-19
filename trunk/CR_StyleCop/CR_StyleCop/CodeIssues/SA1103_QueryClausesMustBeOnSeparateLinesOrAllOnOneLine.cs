namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1103_QueryClausesMustBeOnSeparateLinesOrAllOnOneLine : QueryExpressionCodeIssue
    {
        public SA1103_QueryClausesMustBeOnSeparateLinesOrAllOnOneLine()
            : base(Underline.AllKeywordsOnLine)
        {
        }
    }
}

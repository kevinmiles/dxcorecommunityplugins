namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1103_QueryClausesMustBeOnSeparateLinesOrAllOnOneLine : StyleCopRule
    {
        public SA1103_QueryClausesMustBeOnSeparateLinesOrAllOnOneLine()
            : base(new AllTokensByTypeIssueLocator(Keywords.QueryExpression))
        {
        }
    }
}

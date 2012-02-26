namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1105_QueryClausesSpanningMultipleLinesMustBeginOnOwnLine : StyleCopRule
    {
        public SA1105_QueryClausesSpanningMultipleLinesMustBeginOnOwnLine()
            : base(new LastTokenByTypeIssueLocator(ElementTokens, Keywords.QueryExpression))
        {
        }
    }
}

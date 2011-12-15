namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1001_CommasMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1001_CommasMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[] 
                { 
                    new AllTokensByTypePrecededByWhitespaceIssueLocator(CsTokenType.Comma),
                    new AllTokensByTypeNotFollowedByWhitespaceIssueLocator(CsTokenType.Comma),
                }))
        {
        }
    }
}

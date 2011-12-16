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
                    new AllTokensByTypePrecededByBannedElementIssueLocator(CsTokenType.WhiteSpace, CsTokenType.Comma),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(CsTokenType.WhiteSpace, CsTokenType.Comma),
                }))
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1002_SemicolonsMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1002_SemicolonsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[] 
                { 
                    new AllTokensByTypePrecededByBannedElementIssueLocator(element => element.ElementTokens, CsTokenType.WhiteSpace, CsTokenType.Semicolon),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(element => element.ElementTokens, CsTokenType.WhiteSpace, CsTokenType.Semicolon),
                }))
        {
        }
    }
}

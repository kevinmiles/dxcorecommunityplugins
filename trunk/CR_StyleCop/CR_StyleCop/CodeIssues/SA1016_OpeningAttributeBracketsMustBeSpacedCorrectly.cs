namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1016_OpeningAttributeBracketsMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1016_OpeningAttributeBracketsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensByTypeFollowedByBannedElementIssueLocator(ElementTokens, CsTokenType.OpenAttributeBracket, CsTokenType.WhiteSpace),
                    new AllTokensByTypeFollowedByBannedElementIssueLocator(AttributesTokens, CsTokenType.OpenAttributeBracket, CsTokenType.WhiteSpace),
                }))
        {
        }
    }
}

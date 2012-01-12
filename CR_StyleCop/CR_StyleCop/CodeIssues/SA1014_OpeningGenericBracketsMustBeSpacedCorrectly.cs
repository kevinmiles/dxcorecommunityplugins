namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1014_OpeningGenericBracketsMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1014_OpeningGenericBracketsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensByTypePrecededByBannedElementIssueLocator(ElementTokens, CsTokenType.OpenGenericBracket, CsTokenType.WhiteSpace),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(AttributesTokens, CsTokenType.OpenGenericBracket, CsTokenType.WhiteSpace),
                    new AllTokensByTypeFollowedByBannedElementIssueLocator(ElementTokens, CsTokenType.OpenGenericBracket, CsTokenType.WhiteSpace),
                    new AllTokensByTypeFollowedByBannedElementIssueLocator(AttributesTokens, CsTokenType.OpenGenericBracket, CsTokenType.WhiteSpace),
                }))
        {
        }
    }
}

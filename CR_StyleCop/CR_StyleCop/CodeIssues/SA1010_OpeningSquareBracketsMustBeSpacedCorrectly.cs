namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1010_OpeningSquareBracketsMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1010_OpeningSquareBracketsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensByTypePrecededByBannedElementIssueLocator(ElementTokens, CsTokenType.OpenSquareBracket, CsTokenType.WhiteSpace),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(AttributesTokens, CsTokenType.OpenSquareBracket, CsTokenType.WhiteSpace),
                    new AllTokensByTypeFollowedByBannedElementIssueLocator(ElementTokens, CsTokenType.OpenSquareBracket, CsTokenType.WhiteSpace),
                    new AllTokensByTypeFollowedByBannedElementIssueLocator(AttributesTokens, CsTokenType.OpenSquareBracket, CsTokenType.WhiteSpace),
                }))
        {
        }
    }
}

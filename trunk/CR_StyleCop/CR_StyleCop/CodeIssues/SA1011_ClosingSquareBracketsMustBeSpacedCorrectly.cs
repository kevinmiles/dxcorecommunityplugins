namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1011_ClosingSquareBracketsMustBeSpacedCorrectly : StyleCopRule
    {
        private static string[] allowedOperators = new[] { ".", "->" };
        private static Func<CsToken, bool> isRequiredFollower = token =>
            token.CsTokenType == CsTokenType.WhiteSpace
            || token.CsTokenType == CsTokenType.OpenSquareBracket
            || token.CsTokenType == CsTokenType.CloseSquareBracket
            || token.CsTokenType == CsTokenType.OpenAttributeBracket
            || token.CsTokenType == CsTokenType.CloseAttributeBracket
            || token.CsTokenType == CsTokenType.OpenParenthesis
            || token.CsTokenType == CsTokenType.CloseParenthesis
            || token.CsTokenType == CsTokenType.Comma
            || token.CsTokenType == CsTokenType.Semicolon
            || (token.CsTokenType == CsTokenType.OperatorSymbol && allowedOperators.Contains(token.Text));
 
        public SA1011_ClosingSquareBracketsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensByTypePrecededByBannedElementIssueLocator(ElementTokens, CsTokenType.CloseSquareBracket, CsTokenType.WhiteSpace),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(AttributesTokens, CsTokenType.CloseSquareBracket, CsTokenType.WhiteSpace),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(ElementTokens, CsTokenType.CloseSquareBracket, isRequiredFollower),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(AttributesTokens, CsTokenType.CloseSquareBracket, isRequiredFollower),
                    new AllTokensFollowedByWhitespaceAndBannedElementIssueLocator(ElementTokens, (token, violation) => token.CsTokenType == CsTokenType.CloseSquareBracket, isRequiredFollower),
                    new AllTokensFollowedByWhitespaceAndBannedElementIssueLocator(AttributesTokens, (token, violation) => token.CsTokenType == CsTokenType.CloseSquareBracket, isRequiredFollower),
                }))
        {
        }
    }
}

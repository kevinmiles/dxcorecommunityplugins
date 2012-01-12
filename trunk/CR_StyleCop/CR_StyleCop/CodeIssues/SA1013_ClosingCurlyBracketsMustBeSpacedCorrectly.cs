namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1013_ClosingCurlyBracketsMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1013_ClosingCurlyBracketsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensByTypeNotPrecededByRequiredElementIssueLocator(ElementTokens, CsTokenType.CloseCurlyBracket, CsTokenType.WhiteSpace),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(ElementTokens, CsTokenType.CloseCurlyBracket, CsTokenType.WhiteSpace, CsTokenType.EndOfLine, CsTokenType.CloseParenthesis, CsTokenType.Comma, CsTokenType.Semicolon),
                    new AllTokensFollowedByWhitespaceAndBannedElementIssueLocator(ElementTokens, (token, violation) => token.CsTokenType == CsTokenType.CloseCurlyBracket, CsTokenType.CloseParenthesis, CsTokenType.Comma, CsTokenType.Semicolon),
                }))
        {
        }
    }
}

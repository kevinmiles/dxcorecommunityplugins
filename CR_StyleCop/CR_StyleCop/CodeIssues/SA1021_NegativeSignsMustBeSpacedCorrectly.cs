namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1021_NegativeSignsMustBeSpacedCorrectly : StyleCopRule
    {
        private static Func<CsToken, Violation, bool> isNegativeSign = (token, violation) =>
            token.CsTokenType == CsTokenType.OperatorSymbol && token.Text == "-" && token.Parent is UnaryExpression;

        public SA1021_NegativeSignsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensNotPrecededByRequiredElementIssueLocator(ElementTokens, isNegativeSign, CsTokenType.WhiteSpace, CsTokenType.OpenSquareBracket, CsTokenType.OpenParenthesis),
                    new AllTokensNotPrecededByRequiredElementIssueLocator(AttributesTokens, isNegativeSign, CsTokenType.WhiteSpace, CsTokenType.OpenSquareBracket, CsTokenType.OpenParenthesis),
                    new AllTokensPrecededByBannedElementAndWhitespaceIssueLocator(ElementTokens, isNegativeSign, CsTokenType.OpenSquareBracket, CsTokenType.OpenParenthesis),
                    new AllTokensPrecededByBannedElementAndWhitespaceIssueLocator(AttributesTokens, isNegativeSign, CsTokenType.OpenSquareBracket, CsTokenType.OpenParenthesis),
                    new AllTokensFollowedByBannedElementIssueLocator(ElementTokens, isNegativeSign, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensFollowedByBannedElementIssueLocator(AttributesTokens, isNegativeSign, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                }))
        {
        }
    }
}

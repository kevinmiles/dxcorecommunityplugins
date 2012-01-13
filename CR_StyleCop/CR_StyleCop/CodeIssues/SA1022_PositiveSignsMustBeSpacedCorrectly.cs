namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1022_PositiveSignsMustBeSpacedCorrectly : StyleCopRule
    {
        private static Func<CsToken, Violation, bool> isPositiveSign = (token, violation) =>
            token.CsTokenType == CsTokenType.OperatorSymbol && token.Text == "+" && token.Parent is UnaryExpression;

        public SA1022_PositiveSignsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensNotPrecededByRequiredElementIssueLocator(ElementTokens, isPositiveSign, CsTokenType.WhiteSpace, CsTokenType.OpenSquareBracket, CsTokenType.OpenParenthesis),
                    new AllTokensNotPrecededByRequiredElementIssueLocator(AttributesTokens, isPositiveSign, CsTokenType.WhiteSpace, CsTokenType.OpenSquareBracket, CsTokenType.OpenParenthesis),
                    new AllTokensPrecededByBannedElementAndWhitespaceIssueLocator(ElementTokens, isPositiveSign, CsTokenType.OpenSquareBracket, CsTokenType.OpenParenthesis),
                    new AllTokensPrecededByBannedElementAndWhitespaceIssueLocator(AttributesTokens, isPositiveSign, CsTokenType.OpenSquareBracket, CsTokenType.OpenParenthesis),
                    new AllTokensFollowedByBannedElementIssueLocator(ElementTokens, isPositiveSign, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensFollowedByBannedElementIssueLocator(AttributesTokens, isPositiveSign, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                }))
        {
        }
    }
}

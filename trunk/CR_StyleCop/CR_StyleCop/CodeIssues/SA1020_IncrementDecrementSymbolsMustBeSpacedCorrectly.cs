namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1020_IncrementDecrementSymbolsMustBeSpacedCorrectly : StyleCopRule
    {
        private static readonly Func<CsToken, Violation, bool> isPrefixOperator = (token, violation) =>
            {
                var incrementExpression = token.Parent as IncrementExpression;
                var decrementExpression = token.Parent as DecrementExpression;
                return token.CsTokenType == CsTokenType.OperatorSymbol
                    && ((incrementExpression != null && incrementExpression.Type == IncrementExpression.IncrementType.Prefix)
                    || (decrementExpression != null && decrementExpression.Type == DecrementExpression.DecrementType.Prefix));
            };

        private static readonly Func<CsToken, Violation, bool> isPostfixOperator = (token, violation) =>
            {
                var incrementExpression = token.Parent as IncrementExpression;
                var decrementExpression = token.Parent as DecrementExpression;
                return token.CsTokenType == CsTokenType.OperatorSymbol
                    && ((incrementExpression != null && incrementExpression.Type == IncrementExpression.IncrementType.Postfix)
                    || (decrementExpression != null && decrementExpression.Type == DecrementExpression.DecrementType.Postfix));
            };

        public SA1020_IncrementDecrementSymbolsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensFollowedByBannedElementIssueLocator(ElementTokens, isPrefixOperator, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensFollowedByBannedElementIssueLocator(AttributesTokens, isPrefixOperator, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensPrecededByBannedElementIssueLocator(ElementTokens, isPostfixOperator, CsTokenType.WhiteSpace),
                    new AllTokensPrecededByBannedElementIssueLocator(AttributesTokens, isPostfixOperator, CsTokenType.WhiteSpace),
                }))
        {
        }
    }
}

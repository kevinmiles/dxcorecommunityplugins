namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1023_DereferenceAndAccessOfMustBeSpacedCorrectly : StyleCopRule
    {
        private static Func<CsToken, Violation, bool> isPartOfTypeDefinition = (token, violation) => 
            token.CsTokenType == CsTokenType.OperatorSymbol && token.Text == "*" && token.Parent is TypeToken;

        private static Func<CsToken, Violation, bool> isDereferencingVariable = (token, violation) =>
            token.CsTokenType == CsTokenType.OperatorSymbol && (token.Text == "*" || token.Text == "&") && token.Parent is UnsafeAccessExpression;

        public SA1023_DereferenceAndAccessOfMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensPrecededByBannedElementIssueLocator(ElementTokens, isPartOfTypeDefinition, CsTokenType.WhiteSpace),
                    new AllTokensPrecededByBannedElementIssueLocator(AttributesTokens, isPartOfTypeDefinition, CsTokenType.WhiteSpace),
                    new AllTokensNotFollowedByRequiredElementIssueLocator(ElementTokens, isPartOfTypeDefinition, CsTokenType.WhiteSpace, CsTokenType.EndOfLine, CsTokenType.OpenSquareBracket, CsTokenType.CloseSquareBracket, CsTokenType.OpenParenthesis, CsTokenType.CloseParenthesis, CsTokenType.CloseGenericBracket),
                    new AllTokensNotFollowedByRequiredElementIssueLocator(AttributesTokens, isPartOfTypeDefinition, CsTokenType.WhiteSpace, CsTokenType.EndOfLine, CsTokenType.OpenSquareBracket, CsTokenType.CloseSquareBracket, CsTokenType.OpenParenthesis, CsTokenType.CloseParenthesis, CsTokenType.CloseGenericBracket),
                    new AllTokensFollowedByWhitespaceAndBannedElementIssueLocator(ElementTokens, isPartOfTypeDefinition, CsTokenType.OpenSquareBracket, CsTokenType.CloseSquareBracket, CsTokenType.OpenParenthesis, CsTokenType.CloseParenthesis, CsTokenType.CloseGenericBracket),
                    new AllTokensFollowedByWhitespaceAndBannedElementIssueLocator(AttributesTokens, isPartOfTypeDefinition, CsTokenType.OpenSquareBracket, CsTokenType.CloseSquareBracket, CsTokenType.OpenParenthesis, CsTokenType.CloseParenthesis, CsTokenType.CloseGenericBracket),
                    new AllTokensFollowedByBannedElementIssueLocator(ElementTokens, isDereferencingVariable, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensFollowedByBannedElementIssueLocator(AttributesTokens, isDereferencingVariable, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensNotPrecededByRequiredElementIssueLocator(ElementTokens, isDereferencingVariable, CsTokenType.WhiteSpace, CsTokenType.OpenSquareBracket, CsTokenType.CloseSquareBracket, CsTokenType.OpenParenthesis, CsTokenType.CloseParenthesis),
                    new AllTokensNotPrecededByRequiredElementIssueLocator(AttributesTokens, isDereferencingVariable, CsTokenType.WhiteSpace, CsTokenType.OpenSquareBracket, CsTokenType.CloseSquareBracket, CsTokenType.OpenParenthesis, CsTokenType.CloseParenthesis),
                    new AllTokensPrecededByBannedElementAndWhitespaceIssueLocator(ElementTokens, isDereferencingVariable, CsTokenType.OpenSquareBracket, CsTokenType.CloseSquareBracket, CsTokenType.OpenParenthesis, CsTokenType.CloseParenthesis),
                    new AllTokensPrecededByBannedElementAndWhitespaceIssueLocator(AttributesTokens, isDereferencingVariable, CsTokenType.OpenSquareBracket, CsTokenType.CloseSquareBracket, CsTokenType.OpenParenthesis, CsTokenType.CloseParenthesis),
                }))
        {
        }
    }
}

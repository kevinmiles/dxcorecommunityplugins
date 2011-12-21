namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1003_SymbolsMustBeSpacedCorrectly : StyleCopRule
    {
        private static readonly CsTokenType[] requiredPredecessors = new[] 
            { 
                CsTokenType.WhiteSpace, 
                CsTokenType.OpenParenthesis, 
                CsTokenType.OpenSquareBracket 
            };

        private static readonly CsTokenType[] colons = new[] 
            { 
                CsTokenType.WhereColon, 
                CsTokenType.BaseColon,
            };

        private static readonly string[] unaryOperators = new[] { "!", "-", "~" };
        private static readonly string[] binaryOperators = new[] { "=", "=>", "-", "+", "*", "/", "%", "<<", ">>", "??", "==", "!=", ">", "<", ">=", "<=", "&&", "||", "+=", "-=", "*=", "/=", "%=", "<<=", ">>=", ":", "&", "|", "^", "&=", "|=", "^=" };

        public SA1003_SymbolsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[] 
                { 
                    new AllTokensNotPrecededByRequiredElementIssueLocator(element => element.ElementTokens, (token, violation) => token.CsTokenType == CsTokenType.OperatorSymbol && unaryOperators.Contains(token.Text) && violation.Message.Contains(token.Text), requiredPredecessors),
                    new AllTokensFollowedByBannedElementIssueLocator(element => element.ElementTokens, (token, violation) => token.CsTokenType == CsTokenType.OperatorSymbol && unaryOperators.Contains(token.Text) && violation.Message.Contains(token.Text), CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensNotPrecededByRequiredElementIssueLocator(element => element.ElementTokens, (token, violation) => token.CsTokenType == CsTokenType.OperatorSymbol && binaryOperators.Contains(token.Text) && violation.Message.Contains(token.Text), requiredPredecessors),
                    new AllTokensNotFollowedByRequiredElementIssueLocator(element => element.ElementTokens, (token, violation) => token.CsTokenType == CsTokenType.OperatorSymbol && binaryOperators.Contains(token.Text) && violation.Message.Contains(token.Text), CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensNotPrecededByRequiredElementIssueLocator(element => element.ElementTokens, (token, violation) => colons.Contains(token.CsTokenType) && violation.Message.Contains(token.Text), CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensNotFollowedByRequiredElementIssueLocator(element => element.ElementTokens, (token, violation) => colons.Contains(token.CsTokenType) && violation.Message.Contains(token.Text), CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                }))
        {
        }
    }
}

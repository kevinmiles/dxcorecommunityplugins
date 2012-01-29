namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1019_MemberAccessSymbolsMustBeSpacedCorrectly : StyleCopRule
    {
        private static string[] memberAccessSymbols = new[] { ".", "->", "::" };

        public SA1019_MemberAccessSymbolsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensPrecededByBannedElementIssueLocator(ElementTokens, (token, violation) => token.CsTokenType == CsTokenType.OperatorSymbol && memberAccessSymbols.Contains(token.Text), token => token.CsTokenType == CsTokenType.WhiteSpace && token.Location.StartPoint.IndexOnLine != 0),
                    new AllTokensPrecededByBannedElementIssueLocator(AttributesTokens, (token, violation) => token.CsTokenType == CsTokenType.OperatorSymbol && memberAccessSymbols.Contains(token.Text), token => token.CsTokenType == CsTokenType.WhiteSpace && token.Location.StartPoint.IndexOnLine != 0),
                    new AllTokensFollowedByBannedElementIssueLocator(ElementTokens, (token, violation) => token.CsTokenType == CsTokenType.OperatorSymbol && memberAccessSymbols.Contains(token.Text), CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensFollowedByBannedElementIssueLocator(AttributesTokens, (token, violation) => token.CsTokenType == CsTokenType.OperatorSymbol && memberAccessSymbols.Contains(token.Text), CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                }))
        {
        }
    }
}

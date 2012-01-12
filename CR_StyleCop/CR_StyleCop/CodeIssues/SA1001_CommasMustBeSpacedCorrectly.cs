namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1001_CommasMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1001_CommasMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[] 
                { 
                    new AllTokensByTypePrecededByBannedElementIssueLocator(ElementTokens, CsTokenType.Comma, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(AttributesTokens, CsTokenType.Comma, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(ElementTokens, CsTokenType.Comma, CsTokenType.WhiteSpace, CsTokenType.EndOfLine, CsTokenType.CloseGenericBracket, CsTokenType.CloseSquareBracket),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(AttributesTokens, CsTokenType.Comma, CsTokenType.WhiteSpace, CsTokenType.EndOfLine, CsTokenType.CloseGenericBracket, CsTokenType.CloseSquareBracket),
                }))
        {
        }
    }
}

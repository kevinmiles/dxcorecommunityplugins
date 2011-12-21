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
                    new AllTokensByTypePrecededByBannedElementIssueLocator(element => element.ElementTokens, CsTokenType.Comma, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(element => element.Attributes.SelectMany(attribute => attribute.ChildTokens), CsTokenType.Comma, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(element => element.ElementTokens, CsTokenType.Comma, CsTokenType.WhiteSpace, CsTokenType.EndOfLine, CsTokenType.CloseGenericBracket, CsTokenType.CloseSquareBracket),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(element => element.Attributes.SelectMany(attribute => attribute.ChildTokens), CsTokenType.Comma, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                }))
        {
        }
    }
}

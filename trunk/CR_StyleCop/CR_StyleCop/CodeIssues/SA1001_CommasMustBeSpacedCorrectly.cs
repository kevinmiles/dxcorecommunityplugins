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
                    new AllTokensByTypePrecededByBannedElementIssueLocator(element => element.ElementTokens, CsTokenType.WhiteSpace, CsTokenType.Comma),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(element => element.Attributes.SelectMany(attribute => attribute.ChildTokens), CsTokenType.WhiteSpace, CsTokenType.Comma),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(element => element.ElementTokens, new[] { CsTokenType.WhiteSpace, CsTokenType.CloseGenericBracket, CsTokenType.CloseSquareBracket }, CsTokenType.Comma),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(element => element.Attributes.SelectMany(attribute => attribute.ChildTokens), CsTokenType.WhiteSpace, CsTokenType.Comma),
                }))
        {
        }
    }
}

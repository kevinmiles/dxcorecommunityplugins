using StyleCop.CSharp;
namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    internal class SA1008_OpeningParenthesisMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1008_OpeningParenthesisMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[] 
                {
                    new AllTokensByTypeFollowedByBannedElementIssueLocator(element => element.ElementTokens, CsTokenType.OpenParenthesis, CsTokenType.WhiteSpace),
                    new AllTokensByTypeFollowedByBannedElementIssueLocator(element => element.Attributes.SelectMany(attribute => attribute.ChildTokens), CsTokenType.OpenParenthesis, CsTokenType.WhiteSpace),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(element => element.ElementTokens, CsTokenType.OpenParenthesis, CsTokenType.WhiteSpace),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(element => element.Attributes.SelectMany(attribute => attribute.ChildTokens), CsTokenType.OpenParenthesis, CsTokenType.WhiteSpace),
                    //new AllTokensPrecededByBannedWhitespaceNotPrecededBySpecialElementIssueLocator(element =
                }))
        {

        }
    }
}

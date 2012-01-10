using StyleCop.CSharp;
namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class SA1008_OpeningParenthesisMustBeSpacedCorrectly : StyleCopRule
    {
        private static readonly IEnumerable<CsTokenType> tokenTypesWithRequiredSpace = new[]
                {
                    CsTokenType.OperatorSymbol,
                    CsTokenType.For,
                    CsTokenType.Foreach,
                    CsTokenType.While,
                    CsTokenType.If,
                    CsTokenType.Lock,
                    CsTokenType.Using,
                    CsTokenType.Fixed,
                    CsTokenType.Switch,
                    CsTokenType.Catch,
                    CsTokenType.Throw,
                    CsTokenType.Return,
                    CsTokenType.In, 
                    CsTokenType.Where, 
                    CsTokenType.Group, 
                    CsTokenType.By,
                    CsTokenType.Into,
                    CsTokenType.OrderBy,
                    CsTokenType.Let,
                    CsTokenType.Select,
                    CsTokenType.From,
                    CsTokenType.Join,
                    CsTokenType.On,
                    CsTokenType.Equals,
                    CsTokenType.New
                };

        public SA1008_OpeningParenthesisMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[] 
                {
                    new AllTokensByTypeFollowedByBannedElementIssueLocator(element => element.ElementTokens, CsTokenType.OpenParenthesis, CsTokenType.WhiteSpace),
                    new AllTokensByTypeFollowedByBannedElementIssueLocator(element => element.Attributes.SelectMany(attribute => attribute.ChildTokens), CsTokenType.OpenParenthesis, CsTokenType.WhiteSpace),
                    new AllTokensPrecededByBannedWhitespaceNotPrecededBySpecialElementIssueLocator(element => element.ElementTokens, (token, violation) => token.CsTokenType == CsTokenType.OpenParenthesis, tokenTypesWithRequiredSpace),
                    new AllTokensPrecededByBannedWhitespaceNotPrecededBySpecialElementIssueLocator(element => element.Attributes.SelectMany(attribute => attribute.ChildTokens), (token, violation) => token.CsTokenType == CsTokenType.OpenParenthesis, CsTokenType.OperatorSymbol),
                }))
        {

        }
    }
}

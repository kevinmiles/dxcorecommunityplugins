namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1000_KeywordsMustBeSpacedCorrectly : StyleCopRule
    {
        private static readonly IEnumerable<CsTokenType> tokenTypesWithoutSpace = new[]
                {
                    CsTokenType.DefaultValue,
                    CsTokenType.Typeof,
                    CsTokenType.Sizeof,
                    CsTokenType.Checked,
                    CsTokenType.Unchecked       
                };

        private static readonly IEnumerable<CsTokenType> tokenTypesWithRequiredSpace = new[]
                {
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

        public SA1000_KeywordsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[] 
                { 
                    new AllTokensByTypeFollowedByBannedElementIssueLocator(element => element.ElementTokens, CsTokenType.WhiteSpace, tokenTypesWithoutSpace), 
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(element => element.ElementTokens, CsTokenType.WhiteSpace, tokenTypesWithRequiredSpace),
                    new AllTokensByTypeNotPrecededByRequiredElementIssueLocator(element => element.ElementTokens, CsTokenType.WhiteSpace, tokenTypesWithoutSpace.Concat(tokenTypesWithRequiredSpace))
                }))
        {
        }
    }
}

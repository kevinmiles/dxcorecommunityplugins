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
                    new AllTokensByTypeFollowedByBannedElementIssueLocator(ElementTokens, tokenTypesWithoutSpace, CsTokenType.WhiteSpace, CsTokenType.EndOfLine), 
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(ElementTokens, tokenTypesWithRequiredSpace, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensByTypeNotPrecededByRequiredElementIssueLocator(ElementTokens, tokenTypesWithoutSpace.Concat(tokenTypesWithRequiredSpace), CsTokenType.WhiteSpace, CsTokenType.OpenParenthesis, CsTokenType.OpenSquareBracket)
                }))
        {
        }
    }
}

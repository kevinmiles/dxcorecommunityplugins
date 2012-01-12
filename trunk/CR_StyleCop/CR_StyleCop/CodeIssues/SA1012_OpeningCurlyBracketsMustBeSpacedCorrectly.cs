namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1012_OpeningCurlyBracketsMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1012_OpeningCurlyBracketsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensByTypeNotPrecededByRequiredElementIssueLocator(ElementTokens, CsTokenType.OpenCurlyBracket, CsTokenType.WhiteSpace, CsTokenType.OpenParenthesis),
                    new AllTokensPrecededByBannedElementAndWhitespaceIssueLocator(ElementTokens, (token, violation) => token.CsTokenType == CsTokenType.OpenCurlyBracket, CsTokenType.OpenParenthesis),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(ElementTokens, CsTokenType.OpenCurlyBracket, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                }))
        {
        }
    }
}

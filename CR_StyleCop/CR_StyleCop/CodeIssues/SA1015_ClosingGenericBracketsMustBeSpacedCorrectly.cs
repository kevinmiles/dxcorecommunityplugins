namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1015_ClosingGenericBracketsMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1015_ClosingGenericBracketsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensByTypePrecededByBannedElementIssueLocator(ElementTokens, CsTokenType.CloseGenericBracket, CsTokenType.WhiteSpace),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(AttributesTokens, CsTokenType.CloseGenericBracket, CsTokenType.WhiteSpace),
                }))
        {
        }
    }
}

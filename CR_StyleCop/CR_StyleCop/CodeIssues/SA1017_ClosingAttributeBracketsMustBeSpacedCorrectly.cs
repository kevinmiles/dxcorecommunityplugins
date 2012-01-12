namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1017_ClosingAttributeBracketsMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1017_ClosingAttributeBracketsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensByTypePrecededByBannedElementIssueLocator(ElementTokens, CsTokenType.CloseAttributeBracket, CsTokenType.WhiteSpace),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(AttributesTokens, CsTokenType.CloseAttributeBracket, CsTokenType.WhiteSpace),
                }))
        {
        }
    }
}

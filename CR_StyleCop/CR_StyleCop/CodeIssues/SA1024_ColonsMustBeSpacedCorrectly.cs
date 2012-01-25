namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1024_ColonsMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1024_ColonsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(ElementTokens, CsTokenType.LabelColon, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(AttributesTokens, CsTokenType.LabelColon, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(ElementTokens, CsTokenType.LabelColon, CsTokenType.WhiteSpace),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(AttributesTokens, CsTokenType.LabelColon, CsTokenType.WhiteSpace),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(AttributesTokens, CsTokenType.AttributeColon, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(AttributesTokens, CsTokenType.AttributeColon, CsTokenType.WhiteSpace),
                }))
        {
        }
    }
}

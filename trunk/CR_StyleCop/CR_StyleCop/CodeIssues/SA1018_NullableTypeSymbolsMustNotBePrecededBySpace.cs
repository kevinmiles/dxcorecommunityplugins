namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1018_NullableTypeSymbolsMustNotBePrecededBySpace : StyleCopRule
    {
        public SA1018_NullableTypeSymbolsMustNotBePrecededBySpace()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensByTypePrecededByBannedElementIssueLocator(element => element.ElementTokens, CsTokenType.NullableTypeSymbol, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(element => element.Attributes.SelectMany(attribute => attribute.ChildTokens), CsTokenType.NullableTypeSymbol, CsTokenType.WhiteSpace, CsTokenType.EndOfLine)
                }))
        {
        }
    }
}

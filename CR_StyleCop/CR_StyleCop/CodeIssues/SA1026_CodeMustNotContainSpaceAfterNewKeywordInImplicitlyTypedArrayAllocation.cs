namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1026_CodeMustNotContainSpaceAfterNewKeywordInImplicitlyTypedArrayAllocation : StyleCopRule
    {
        private static readonly Func<CsToken, Violation, bool> isImplicitlyTypedArray = (token, violation) =>
            token.CsTokenType == CsTokenType.New && token.Parent is NewArrayExpression;

        public SA1026_CodeMustNotContainSpaceAfterNewKeywordInImplicitlyTypedArrayAllocation()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensFollowedByBannedElementIssueLocator(ElementTokens, isImplicitlyTypedArray, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                    new AllTokensFollowedByBannedElementIssueLocator(AttributesTokens, isImplicitlyTypedArray, CsTokenType.WhiteSpace, CsTokenType.EndOfLine),
                }))
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class AllTokensByTypePrecededByBannedElementIssueLocator : AllTokensPrecededByBannedElementIssueLocator
    {
        public AllTokensByTypePrecededByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType tokenTypeToInspect, params CsTokenType[] bannedPredecessors)
            : base(getTokens, (token, violation) => token.CsTokenType == tokenTypeToInspect, bannedPredecessors)
        {
        }

        public AllTokensByTypePrecededByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType tokenTypeToInspect, IEnumerable<CsTokenType> bannedPredecessors)
            : base(getTokens, (token, violation) => token.CsTokenType == tokenTypeToInspect, bannedPredecessors)
        {
        }

        public AllTokensByTypePrecededByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> tokenTypesToInspect, params CsTokenType[] bannedPredecessors)
            : base(getTokens, (token, violation) => tokenTypesToInspect.Contains(token.CsTokenType), bannedPredecessors)
        {
        }

        public AllTokensByTypePrecededByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> tokenTypesToInspect, IEnumerable<CsTokenType> bannedPredecessors)
            : base(getTokens, (token, violation) => tokenTypesToInspect.Contains(token.CsTokenType), bannedPredecessors)
        {
        }
    }
}
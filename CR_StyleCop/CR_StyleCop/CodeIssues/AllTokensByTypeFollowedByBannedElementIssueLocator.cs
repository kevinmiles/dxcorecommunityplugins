namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class AllTokensByTypeFollowedByBannedElementIssueLocator : AllTokensFollowedByBannedElementIssueLocator
    {
        public AllTokensByTypeFollowedByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType tokenTypeToInspect, params CsTokenType[] bannedFollowers)
            : base(getTokens, (token, violation) => token.CsTokenType == tokenTypeToInspect, bannedFollowers)
        {
        }

        public AllTokensByTypeFollowedByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType tokenTypeToInspect, IEnumerable<CsTokenType> bannedFollowers)
            : base(getTokens, (token, violation) => token.CsTokenType == tokenTypeToInspect, bannedFollowers)
        {
        }

        public AllTokensByTypeFollowedByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> tokenTypesToInspect, params CsTokenType[] bannedFollowers)
            : base(getTokens, (token, violation) => tokenTypesToInspect.Contains(token.CsTokenType), bannedFollowers)
        {
        }

        public AllTokensByTypeFollowedByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> tokenTypesToInspect, IEnumerable<CsTokenType> bannedFollowers)
            : base(getTokens, (token, violation) => tokenTypesToInspect.Contains(token.CsTokenType), bannedFollowers)
        {
        }
    }
}
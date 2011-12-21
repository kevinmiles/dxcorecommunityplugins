namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class AllTokensByTypeNotFollowedByRequiredElementIssueLocator : AllTokensNotFollowedByRequiredElementIssueLocator
    {
        public AllTokensByTypeNotFollowedByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType tokenTypeToInspect, params CsTokenType[] requiredFollowers)
            : base(getTokens, (token, violation) => token.CsTokenType == tokenTypeToInspect, requiredFollowers)
        {
        }

        public AllTokensByTypeNotFollowedByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType tokenTypeToInspect, IEnumerable<CsTokenType> requiredFollowers)
            : base(getTokens, (token, violation) => token.CsTokenType == tokenTypeToInspect, requiredFollowers)
        {
        }

        public AllTokensByTypeNotFollowedByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> tokenTypesToInspect, params CsTokenType[] requiredFollowers)
            : base(getTokens, (token, violation) => tokenTypesToInspect.Contains(token.CsTokenType), requiredFollowers)
        {
        }

        public AllTokensByTypeNotFollowedByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> tokenTypesToInspect, IEnumerable<CsTokenType> requiredFollowers)
            : base(getTokens, (token, violation) => tokenTypesToInspect.Contains(token.CsTokenType), requiredFollowers)
        {
        }
    }
}
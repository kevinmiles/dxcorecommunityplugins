namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class AllTokensByTypeNotPrecededByRequiredElementIssueLocator : AllTokensNotPrecededByRequiredElementIssueLocator
    {
        public AllTokensByTypeNotPrecededByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType tokenTypeToInspect, params CsTokenType[] requiredPredecessors)
            : base(getTokens, (token, violation) => token.CsTokenType == tokenTypeToInspect, requiredPredecessors)
        {
        }

        public AllTokensByTypeNotPrecededByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType tokenTypeToInspect, IEnumerable<CsTokenType> requiredPredecessors)
            : base(getTokens, (token, violation) => token.CsTokenType == tokenTypeToInspect, requiredPredecessors)
        {
        }

        public AllTokensByTypeNotPrecededByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> tokenTypesToInspect, params CsTokenType[] requiredPredecessors)
            : base(getTokens, (token, violation) => tokenTypesToInspect.Contains(token.CsTokenType), requiredPredecessors)
        {
        }

        public AllTokensByTypeNotPrecededByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> tokenTypesToInspect, IEnumerable<CsTokenType> requiredPredecessors)
            : base(getTokens, (token, violation) => tokenTypesToInspect.Contains(token.CsTokenType), requiredPredecessors)
        {
        }
    }
}
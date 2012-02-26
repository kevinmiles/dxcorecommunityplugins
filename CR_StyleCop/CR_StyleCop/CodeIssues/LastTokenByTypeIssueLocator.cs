namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class LastTokenByTypeIssueLocator : LastTokenIssueLocator
    {
        public LastTokenByTypeIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, params CsTokenType[] tokenTypes)
            : base(getTokens, (csToken, violation) => tokenTypes.Contains(csToken.CsTokenType))
        {
        }
    }
}

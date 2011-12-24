namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1007_OperatorKeywordMustBeFollowedBySpace : StyleCopRule
    {
        public SA1007_OperatorKeywordMustBeFollowedBySpace()
            : base(new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(element => element.ElementTokens, CsTokenType.Operator, CsTokenType.WhiteSpace))
        {
        }
    }
}

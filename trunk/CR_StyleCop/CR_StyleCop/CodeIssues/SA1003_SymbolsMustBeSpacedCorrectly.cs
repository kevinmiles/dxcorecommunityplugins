namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;

    internal class SA1003_SymbolsMustBeSpacedCorrectly : StyleCopRule
    {
        private static readonly CsTokenType[] requiredPredecessors = new[] 
            { 
                CsTokenType.WhiteSpace, 
                CsTokenType.OpenParenthesis, 
                CsTokenType.OpenSquareBracket 
            };

        public SA1003_SymbolsMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[] 
                { 
                    new AllTokensByTypeNotPrecededByRequiredElementIssueLocator(requiredPredecessors, CsTokenType.OperatorSymbol),
                    new AllTokensByTypeNotFollowedByRequiredElementIssueLocator(CsTokenType.WhiteSpace, CsTokenType.OperatorSymbol),
                }))
        {
        }
    }
}

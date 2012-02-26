namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using StyleCop.CSharp;
    
    internal class SA1117_ParametersMustBeOnSameLineOrSeparateLines : StyleCopRule
    {
        private static CsTokenType[] bannedTypes = new[]
            {
                CsTokenType.EndOfLine,
                CsTokenType.WhiteSpace,
                CsTokenType.OpenParenthesis,
                CsTokenType.OpenSquareBracket
            };

        public SA1117_ParametersMustBeOnSameLineOrSeparateLines()
            : base(new LastTokenIssueLocator(ElementTokens, (csToken, violation) => !bannedTypes.Contains(csToken.CsTokenType)))
        {
        }
    }
}

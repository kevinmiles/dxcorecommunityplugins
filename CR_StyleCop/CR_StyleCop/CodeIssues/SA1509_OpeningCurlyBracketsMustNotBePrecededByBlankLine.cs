namespace CR_StyleCop.CodeIssues
{
    using System;
    using StyleCop.CSharp;

    internal class SA1509_OpeningCurlyBracketsMustNotBePrecededByBlankLine : StyleCopRule
    {
        public SA1509_OpeningCurlyBracketsMustNotBePrecededByBlankLine()
            : base(new FirstTokenByTypeIssueLocator(CsTokenType.OpenCurlyBracket))
        {
        }
    }
}

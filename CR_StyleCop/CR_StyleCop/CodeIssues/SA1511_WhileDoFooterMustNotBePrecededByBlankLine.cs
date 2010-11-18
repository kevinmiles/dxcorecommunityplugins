namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1511_WhileDoFooterMustNotBePrecededByBlankLine : KeywordCodeIssue
    {
        public SA1511_WhileDoFooterMustNotBePrecededByBlankLine()
            : base(new string[] { "while" })
        {
        }
    }
}

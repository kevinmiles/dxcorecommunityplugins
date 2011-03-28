namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1507_CodeMustNotContainMultipleBlankLinesInARow : StyleCopRule
    {
        public SA1507_CodeMustNotContainMultipleBlankLinesInARow()
            : base(new WholeLineIssueLocator())
        {
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class SA1518_CodeMustNotContainBlankLinesAtEndOfFile : StyleCopRule
    {
        public SA1518_CodeMustNotContainBlankLinesAtEndOfFile()
            : base(new WholeLineIssueLocator())
        {
        }
    }
}

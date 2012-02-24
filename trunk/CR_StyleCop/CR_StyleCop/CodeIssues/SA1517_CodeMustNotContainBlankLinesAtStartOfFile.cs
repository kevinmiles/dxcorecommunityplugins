namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class SA1517_CodeMustNotContainBlankLinesAtStartOfFile : StyleCopRule
    {
        public SA1517_CodeMustNotContainBlankLinesAtStartOfFile()
            : base(new WholeLineIssueLocator())
        {
        }
    }
}

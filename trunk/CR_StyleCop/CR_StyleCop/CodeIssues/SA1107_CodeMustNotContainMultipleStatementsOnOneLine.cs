namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class SA1107_CodeMustNotContainMultipleStatementsOnOneLine : StyleCopRule
    {
        public SA1107_CodeMustNotContainMultipleStatementsOnOneLine()
            : base(new WholeLineIssueLocator())
        {
        }
    }
}

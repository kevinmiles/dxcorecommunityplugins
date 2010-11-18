using System.Collections.Generic;
namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1510_ChainedStatementBlocksMustNotBePrecededByBlankLine : KeywordCodeIssue
    {
        public SA1510_ChainedStatementBlocksMustNotBePrecededByBlankLine()
            : base(new string[] { "else", "catch", "finally" })
        {
        }
    }
}

using System.Collections.Generic;
namespace CR_StyleCop.CodeIssues
{
    using System;

    internal class SA1510CodeIssue : KeywordCodeIssue
    {
        public SA1510CodeIssue()
            : base(new string[] { "else", "catch", "finally" })
        {
        }
    }
}

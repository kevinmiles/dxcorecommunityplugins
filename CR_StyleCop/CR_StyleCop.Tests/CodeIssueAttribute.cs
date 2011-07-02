namespace CR_StyleCop.Tests
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    public class CodeIssueAttribute : RowAttribute
    {
        public CodeIssueAttribute(int startLine, int startOffset, int endLine, int endOffset)
            : base(startLine, startOffset, endLine, endOffset)
        {
        }

        public int StartLine
        {
            get { return (int)this.Values[0]; }
        }

        public int StartOffset
        {
            get { return (int)this.Values[1]; }
        }

        public int EndLine
        {
            get { return (int)this.Values[2]; }
        }

        public int EndOffset
        {
            get { return (int)this.Values[3]; }
        }
    }
}

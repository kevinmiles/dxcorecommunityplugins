namespace CR_StyleCop.Tests
{
    using System;
    using System.Linq;
    using MbUnit.Framework;

    public class CodeIssueAttribute : RowAttribute
    {
        public CodeIssueAttribute(int startLine, int startOffset, int endLine, int endOffset)
            : this(startLine, startOffset, endLine, endOffset, string.Empty)
        {
        }

        public CodeIssueAttribute(int startLine, int startOffset, int endLine, int endOffset, string fileNameSuffix)
            : base(startLine, startOffset, endLine, endOffset, fileNameSuffix)
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

        public string FileNameSuffix
        {
            get { return (string)this.Values[4]; }
        }
    }
}

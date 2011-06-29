namespace CR_StyleCop
{
    using System;
    using System.IO;

    internal class FileSourceCode : ISourceCode
    {
        private string allCode;
        private string[] lines;

        public FileSourceCode(string filePath)
        {
            this.lines = File.ReadAllLines(filePath);
            this.allCode = string.Join(Environment.NewLine, this.lines);
        }

        public int LineCount
        {
            get { return this.lines.Length; }
        }

        public override string ToString()
        {
            return this.allCode;
        }

        public int LengthOfLine(int lineNumber)
        {
            return this.lines[lineNumber - 1].Length;
        }

        public string GetText(int lineNumber)
        {
            return this.lines[lineNumber - 1];
        }
    }
}

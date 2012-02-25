namespace CR_StyleCop
{
    using System;
    using System.IO;

    internal class FileSourceCode : ISourceCode
    {
        private readonly string allCode;
        private readonly string filePath;
        private readonly string[] lines;

        public FileSourceCode(string filePath)
        {
            this.filePath = filePath;
            this.allCode = File.ReadAllText(filePath);
            this.lines = this.allCode.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        }

        public string FilePath
        {
            get { return this.filePath; }
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

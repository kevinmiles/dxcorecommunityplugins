namespace CR_StyleCop
{
    using System;
    using DevExpress.CodeRush.StructuralParser;

    internal class VSSourceCode : ISourceCode
    {
        private readonly SourceFile sourceFile;

        public VSSourceCode(SourceFile sourceFile)
        {
            this.sourceFile = sourceFile;
        }

        public int LineCount
        {
            get { return this.sourceFile.Document.LineCount; }
        }

        public override string ToString()
        {
            return this.sourceFile.Document.GetText(this.sourceFile.StartLine, this.sourceFile.StartOffset, this.sourceFile.EndLine, this.sourceFile.EndOffset);
        }

        public int LengthOfLine(int lineNumber)
        {
            return this.sourceFile.Document.LengthOfLine(lineNumber);
        }

        public string GetText(int lineNumber)
        {
            return this.sourceFile.Document.GetText(lineNumber);
        }
    }
}

namespace CR_StyleCop
{
    using System;

    internal interface ISourceCode
    {
        string FilePath { get; }
        int LineCount { get; }
        int LengthOfLine(int lineNumber);
        string GetText(int lineNumber);
    }
}

namespace CR_StyleCop
{
    using System;

    internal interface ISourceCode
    {
        int LineCount { get; }
        int LengthOfLine(int lineNumber);
        string GetText(int lineNumber);
    }
}

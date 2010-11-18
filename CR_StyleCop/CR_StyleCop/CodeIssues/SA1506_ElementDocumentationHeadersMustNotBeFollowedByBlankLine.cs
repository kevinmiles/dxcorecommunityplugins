namespace CR_StyleCop.CodeIssues
{
    using System;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;

    internal class SA1506_ElementDocumentationHeadersMustNotBeFollowedByBlankLine : ICodeIssue
    {
        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            string message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            if (violation.Element == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }
            for (int lineNumber = violation.Line; lineNumber < document.LineCount; lineNumber++)
            {
                string nextLine = document.GetText(lineNumber + 1).Trim();
                if (string.IsNullOrEmpty(nextLine))
                {
                    string lineText = document.GetText(lineNumber);
                    string textToUnderline = lineText.TrimStart();
                    SourceRange sourceRange = new SourceRange(lineNumber, lineText.Length - textToUnderline.Length, lineNumber, document.LengthOfLine(lineNumber) + 1);
                    ea.AddSmell(sourceRange, message, 10);
                    return;
                }
            }
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;

    internal class ClosingCurlyBracketCodeIssue : ICodeIssue
    {
        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            string message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            if (violation.Element == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }
            foreach (var token in violation.Element.ElementTokens)
            {
                if (violation.Line == token.Location.StartPoint.LineNumber && token.Text == "}")
                {
                    SourceRange sourceRange = new SourceRange(token.Location.StartPoint.LineNumber, 1, token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2);
                    ea.AddSmell(sourceRange, message, 10);
                    return;
                }
            }
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    internal class ClosingCurlyBracketCodeIssue : ICodeIssue
    {
        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            string message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            CsElement csElement = violation.Element as CsElement;
            if (csElement == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }
            foreach (var location in from token in csElement.ElementTokens
                                     where violation.Line == token.Location.StartPoint.LineNumber && token.Text == "}"
                                     select token.Location)
            {
                SourceRange sourceRange = new SourceRange(location.StartPoint.LineNumber, 1, location.EndPoint.LineNumber, location.EndPoint.IndexOnLine + 2);
                ea.AddSmell(sourceRange, message, 10);
                return;
            }
        }
    }
}

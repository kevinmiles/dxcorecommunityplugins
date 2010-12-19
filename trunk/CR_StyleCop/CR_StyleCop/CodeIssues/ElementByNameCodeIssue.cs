namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    internal class ElementByNameCodeIssue : ICodeIssue
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
                                  where csElement.Name.EndsWith(token.Text)
                                  select token.Location)
            {
                SourceRange sourceRange = new SourceRange(location.StartPoint.LineNumber, location.StartPoint.IndexOnLine + 1, location.EndPoint.LineNumber, location.EndPoint.IndexOnLine + 2);
                ea.AddSmell(sourceRange, message, 10);
                return;
            }
        }
    }
}
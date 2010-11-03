namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    internal class SA1501CodeIssue : ICodeIssue
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
            CsToken startToken = null;
            foreach (var token in from token in csElement.ElementTokens
                                  where violation.Line == token.Location.StartPoint.LineNumber
                                  select token)
            {
                if (token.Text == "{")
                {
                    startToken = token;
                    continue;
                }
                if (token.Text == "}")
                {
                    SourceRange sourceRange = new SourceRange(startToken.Location.StartPoint.LineNumber, startToken.Location.StartPoint.IndexOnLine + 1, token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2);
                    ea.AddSmell(sourceRange, message, 10);
                    return;
                }
            }
        }
    }
}

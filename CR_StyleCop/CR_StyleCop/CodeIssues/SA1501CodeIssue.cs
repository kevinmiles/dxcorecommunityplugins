using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using Microsoft.StyleCop;

namespace CR_StyleCop.CodeIssues
{
    internal class SA1501CodeIssue : ICodeIssue
    {
        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            string message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            if (violation.Element == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }
            Microsoft.StyleCop.Token startToken = null;
            foreach (var token in violation.Element.ElementTokens)
            {
                if (violation.Line == token.Location.StartPoint.LineNumber)
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
}

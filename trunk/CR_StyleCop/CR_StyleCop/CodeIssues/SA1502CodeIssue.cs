using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using Microsoft.StyleCop;
using Microsoft.StyleCop.CSharp;

namespace CR_StyleCop.CodeIssues
{
    internal class SA1502CodeIssue : ICodeIssue
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
            CodePoint startPoint = csElement.ElementTokens.First().Location.StartPoint;
            CodePoint endPoint = csElement.ElementTokens.Last().Location.EndPoint;
            SourceRange sourceRange = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, endPoint.LineNumber, endPoint.IndexOnLine + 2);
            ea.AddSmell(sourceRange, message, 10);
        }
    }
}

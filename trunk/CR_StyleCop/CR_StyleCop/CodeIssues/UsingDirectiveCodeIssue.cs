namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    public class UsingDirectiveCodeIssue : ICodeIssue
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

            CodePoint startPoint = null;
            CodePoint endPoint = null;
            foreach (var token in from token in csElement.ElementTokens
                                     where token.LineNumber == violation.Line
                                     select token)
            {
                if (token.Text == "using")
                {
                    startPoint = token.Location.StartPoint;
                    continue;
                }

                if (token.Text == ";")
                {
                    endPoint = token.Location.EndPoint;
                }

                if (startPoint != null && endPoint != null)
                {
                    SourceRange sourceRange = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, endPoint.LineNumber, endPoint.IndexOnLine + 2);
                    ea.AddSmell(sourceRange, message, 10);
                    return;
                }
            }
        }
    }
}

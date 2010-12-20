namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    internal class SA1403_FileMayOnlyContainASingleNamespace : ICodeIssue
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

            SourcePoint? startPoint = null;
            SourcePoint? endPoint = null;
            foreach (var token in from token in csElement.ElementTokens
                                  where token.LineNumber >= violation.Line && !string.IsNullOrEmpty(token.Text.Trim())
                                  select token)
            {
                if (token.Text == "namespace")
                {
                    startPoint = endPoint = new SourcePoint(token.Location.StartPoint.LineNumber, token.Location.StartPoint.IndexOnLine + 1);
                    continue;
                }

                if (token.Text == "{")
                {
                    if (startPoint != null)
                    {
                        var sourceRange = new SourceRange(startPoint.Value, endPoint.Value);
                        ea.AddSmell(sourceRange, message, 10);
                    }

                    return;
                }

                if (startPoint != null)
                {
                    endPoint = new SourcePoint(token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2);
                    continue;
                }
            }
        }
    }
}

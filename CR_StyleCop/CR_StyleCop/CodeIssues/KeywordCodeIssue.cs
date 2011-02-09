namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    internal class KeywordCodeIssue : ICodeIssue
    {
        private readonly IEnumerable<string> keywords;

        public KeywordCodeIssue(params string[] keywords)
        {
            this.keywords = keywords;
        }

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
            foreach (var location in from token in csElement.ElementTokens
                                  where token.LineNumber == violation.Line && this.keywords.Contains(token.Text)
                                  select token.Location)
            {
                if (startPoint == null)
                {
                    startPoint = location.StartPoint;
                }

                endPoint = location.EndPoint;
            }

            if (startPoint != null && endPoint != null)
            {
                var sourceRange = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, endPoint.LineNumber, endPoint.IndexOnLine + 2);
                ea.AddSmell(sourceRange, message, 10);
            }
        }
    }
}

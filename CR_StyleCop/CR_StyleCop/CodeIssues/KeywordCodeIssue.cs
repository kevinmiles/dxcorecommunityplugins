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
        private readonly Underline underline;

        public KeywordCodeIssue(string keyword)
            : this(Underline.FirstKeywordOnLine, keyword)
        {
        }

        public KeywordCodeIssue(Underline underline, params string[] keywords)
        {
            this.underline = underline;
            this.keywords = keywords;
        }

        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            var message = string.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            var csElement = violation.Element as CsElement;
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
                switch (this.underline)
                {
                    case Underline.FirstKeywordOnLine:
                        AddCodeSmell(ea, message, location.StartPoint, location.EndPoint);
                        return;
                    case Underline.LastKeywordOnLine:
                        startPoint = location.StartPoint;
                        endPoint = location.EndPoint;
                        break;
                    case Underline.AllKeywordsOnLine:
                        AddCodeSmell(ea, message, location.StartPoint, location.EndPoint);
                        break;
                    case Underline.SpanFromFirstToLastKeywordOnLine:
                        if (startPoint == null)
                        {
                            startPoint = location.StartPoint;
                        }

                        endPoint = location.EndPoint;
                        break;
                }
            }

            if (startPoint != null && endPoint != null)
            {
                AddCodeSmell(ea, message, startPoint, endPoint);
            }
        }

        private static void AddCodeSmell(CheckCodeIssuesEventArgs ea, string message, CodePoint startPoint, CodePoint endPoint)
        {
            var sourceRange = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, endPoint.LineNumber, endPoint.IndexOnLine + 2);
            ea.AddSmell(sourceRange, message, 10);
        }
    }
}

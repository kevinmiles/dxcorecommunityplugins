namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    internal class FirstParameterCodeIssue : ICodeIssue
    {
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
            foreach (var token in from token in csElement.ElementTokens
                                  where token.LineNumber == violation.Line && token.CsTokenType != CsTokenType.WhiteSpace
                                  select token)
            {
                if (startPoint == null)
                {
                    startPoint = token.Location.StartPoint;
                    endPoint = token.Location.EndPoint;
                }

                if (token.CsTokenType == CsTokenType.CloseSquareBracket
                    || token.CsTokenType == CsTokenType.CloseParenthesis
                    || token.CsTokenType == CsTokenType.Comma)
                {
                    var sourceRange = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, endPoint.LineNumber, endPoint.IndexOnLine + 2);
                    ea.AddSmell(sourceRange, message, 10);
                    return;
                }

                endPoint = token.Location.EndPoint;
            }
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    internal class SA1006_PreprocessorKeywordsMustNotBePrecededBySpace : ICodeIssue
    {
        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            var message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            var csElement = violation.Element as CsElement;
            if (csElement == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }

            foreach (var token in from token in csElement.ElementTokens
                                  where token.LineNumber == violation.Line && token.CsTokenType == CsTokenType.PreprocessorDirective
                                  select token)
            {
                int underlineLength = 1;
                while (token.Text[underlineLength] == ' ' || token.Text[underlineLength] == '\t')
                {
                    underlineLength++;
                }

                while (underlineLength < token.Text.Length && token.Text[underlineLength] != ' ' && token.Text[underlineLength] != '\t')
                {
                    underlineLength++;
                }

                var startPoint = token.Location.StartPoint;
                var sourceRange = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, startPoint.LineNumber, startPoint.IndexOnLine + 1 + underlineLength);
                ea.AddSmell(sourceRange, message, 10);
            }
        }
    }
}

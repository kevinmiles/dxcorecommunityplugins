namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    internal class SA1101_PrefixLocalCallsWithThis : ICodeIssue
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

            int prefixLength = "SA1101: The call to ".Length;
            var memberName = message.Substring(prefixLength, message.IndexOf(" must") - prefixLength);
            var thisFound = false;
            foreach (var token in from token in csElement.ElementTokens
                                  where token.LineNumber == violation.Line && token.CsTokenType != CsTokenType.WhiteSpace
                                  select token)
            {
                if (token.Text == "this" || (thisFound && token.Text == "."))
                {
                    thisFound = true;
                }
                else
                {
                    if (token.Text == memberName && !thisFound)
                    {
                        var sourceRange = new SourceRange(token.Location.StartPoint.LineNumber, token.Location.StartPoint.IndexOnLine + 1, token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2);
                        ea.AddSmell(sourceRange, message, 10);
                    }

                    thisFound = false;
                }
            }
        }
    }
}

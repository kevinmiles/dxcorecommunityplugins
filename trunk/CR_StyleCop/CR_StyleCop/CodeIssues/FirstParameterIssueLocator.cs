namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class FirstParameterIssueLocator : ICodeIssueLocator
    {
        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            IDocument document,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            CodePoint startPoint = null;
            CodePoint endPoint = null;
            foreach (var token in from token in csElement.ElementTokens
                                  where token.LineNumber == violation.Line && token.CsTokenType != CsTokenType.WhiteSpace
                                  select token)
            {
                if (token.CsTokenType == CsTokenType.OpenParenthesis
                    || token.CsTokenType == CsTokenType.OpenSquareBracket)
                {
                    startPoint = null;
                    endPoint = null;
                }
                else if (startPoint == null)
                {
                    startPoint = token.Location.StartPoint;
                    endPoint = token.Location.EndPoint;
                }

                if (startPoint != null && endPoint != null &&
                    (token.CsTokenType == CsTokenType.CloseSquareBracket
                    || token.CsTokenType == CsTokenType.CloseParenthesis
                    || token.CsTokenType == CsTokenType.Comma))
                {
                    var sourceRange = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, endPoint.LineNumber, endPoint.IndexOnLine + 2);
                    return new[] { new StyleCopCodeIssue(CodeIssueType.CodeSmell, sourceRange) };
                }

                endPoint = token.Location.EndPoint;
            }

            return Enumerable.Empty<StyleCopCodeIssue>();
        }
    }
}

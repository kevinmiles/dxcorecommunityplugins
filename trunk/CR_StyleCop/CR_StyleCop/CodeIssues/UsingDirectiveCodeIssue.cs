namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class UsingDirectiveCodeIssue : ICodeIssueLocator
    {
        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            CodePoint startPoint = null;
            CodePoint endPoint = null;
            foreach (var token in from token in csElement.ElementTokens
                                  where token.LineNumber >= violation.Line
                                  select token)
            {
                if (token.CsTokenType == CsTokenType.UsingDirective)
                {
                    startPoint = token.Location.StartPoint;
                    continue;
                }

                if (token.CsTokenType == CsTokenType.Semicolon)
                {
                    endPoint = token.Location.EndPoint;
                }

                if (startPoint != null && endPoint != null)
                {
                    var sourceRange = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, endPoint.LineNumber, endPoint.IndexOnLine + 2);
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, sourceRange);
                }
            }
        }
    }
}

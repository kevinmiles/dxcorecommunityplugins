namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AllTokensByTypePrecededByWhitespaceIssueLocator : ICodeIssueLocator
    {
        private readonly IEnumerable<CsTokenType> tokenTypesWithRedundantSpace;

        public AllTokensByTypePrecededByWhitespaceIssueLocator(params CsTokenType[] tokenTypesWithRedundantSpace)
        {
            this.tokenTypesWithRedundantSpace = tokenTypesWithRedundantSpace;
        }

        public AllTokensByTypePrecededByWhitespaceIssueLocator(IEnumerable<CsTokenType> tokenTypesWithRedundantSpace)
        {
            this.tokenTypesWithRedundantSpace = tokenTypesWithRedundantSpace;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            bool whitespaceFound = false;
            foreach (var token in csElement.ElementTokens.Where(x => x.LineNumber == violation.Line))
            {
                if (whitespaceFound && this.tokenTypesWithRedundantSpace.Contains(token.CsTokenType))
                {
                    whitespaceFound = false;
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(token.Location.StartPoint.LineNumber, token.Location.StartPoint.IndexOnLine + 1, token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2));
                }
                else if (token.CsTokenType == CsTokenType.WhiteSpace)
                {
                    whitespaceFound = true;
                }
                else
                {
                    whitespaceFound = false;
                }
            }
        }
    }
}

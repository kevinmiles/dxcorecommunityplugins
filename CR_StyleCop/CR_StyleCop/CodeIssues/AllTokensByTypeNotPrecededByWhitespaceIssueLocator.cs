namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AllTokensByTypeNotPrecededByWhitespaceIssueLocator : ICodeIssueLocator
    {
        private readonly IEnumerable<CsTokenType> tokenTypesWithMissingSpace;

        public AllTokensByTypeNotPrecededByWhitespaceIssueLocator(params CsTokenType[] tokenTypesWithMissingSpace)
        {
            this.tokenTypesWithMissingSpace = tokenTypesWithMissingSpace;
        }

        public AllTokensByTypeNotPrecededByWhitespaceIssueLocator(IEnumerable<CsTokenType> tokenTypesWithMissingSpace)
        {
            this.tokenTypesWithMissingSpace = tokenTypesWithMissingSpace;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            bool whitespaceFound = true;
            foreach (var token in csElement.ElementTokens.Where(x => x.LineNumber == violation.Line))
            {
                if (!whitespaceFound && this.tokenTypesWithMissingSpace.Contains(token.CsTokenType))
                {
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(token.Location.StartPoint.LineNumber, token.Location.StartPoint.IndexOnLine + 1, token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2));
                    whitespaceFound = false;
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

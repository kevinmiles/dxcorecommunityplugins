namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AllTokensByTypeNotFollowedByWhitespaceIssueLocator : ICodeIssueLocator
    {
        private readonly IEnumerable<CsTokenType> tokenTypesWithMissingSpace;

        public AllTokensByTypeNotFollowedByWhitespaceIssueLocator(params CsTokenType[] tokenTypesWithMissingSpace)
        {
            this.tokenTypesWithMissingSpace = tokenTypesWithMissingSpace;
        }

        public AllTokensByTypeNotFollowedByWhitespaceIssueLocator(IEnumerable<CsTokenType> tokenTypesWithMissingSpace)
        {
            this.tokenTypesWithMissingSpace = tokenTypesWithMissingSpace;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            CodeLocation issueLocation = null;
            foreach (var token in csElement.ElementTokens.Where(x => x.LineNumber == violation.Line))
            {
                if (this.tokenTypesWithMissingSpace.Contains(token.CsTokenType))
                {
                    issueLocation = token.Location;
                }
                else if (token.CsTokenType != CsTokenType.WhiteSpace && issueLocation != null)
                {
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(issueLocation.StartPoint.LineNumber, issueLocation.StartPoint.IndexOnLine + 1, issueLocation.EndPoint.LineNumber, issueLocation.EndPoint.IndexOnLine + 2));
                    issueLocation = null;
                }
                else
                {
                    issueLocation = null;
                }
            }
        }
    }
}

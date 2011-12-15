using DevExpress.CodeRush.StructuralParser;
using StyleCop;
namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using StyleCop.CSharp;
    using DevExpress.CodeRush.Core;

    internal class AllTokensByTypeFollowedByWhitespaceIssueLocator : ICodeIssueLocator
    {
        private readonly IEnumerable<CsTokenType> tokenTypesWithRedundantSpace;

        public AllTokensByTypeFollowedByWhitespaceIssueLocator(params CsTokenType[] tokenTypesWithRedundantSpace)
        {
            this.tokenTypesWithRedundantSpace = tokenTypesWithRedundantSpace;
        }

        public AllTokensByTypeFollowedByWhitespaceIssueLocator(IEnumerable<CsTokenType> tokenTypesWithRedundantSpace)
        {
            this.tokenTypesWithRedundantSpace = tokenTypesWithRedundantSpace;
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
                if (this.tokenTypesWithRedundantSpace.Contains(token.CsTokenType))
                {
                    issueLocation = token.Location;
                }
                else if (token.CsTokenType == CsTokenType.WhiteSpace && issueLocation != null)
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

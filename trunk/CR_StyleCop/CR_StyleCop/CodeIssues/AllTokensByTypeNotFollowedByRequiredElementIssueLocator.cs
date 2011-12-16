namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AllTokensByTypeNotFollowedByRequiredElementIssueLocator : ICodeIssueLocator
    {
        private readonly IEnumerable<CsTokenType> requiredFollowers;
        private readonly IEnumerable<CsTokenType> tokenTypesToInspect;

        public AllTokensByTypeNotFollowedByRequiredElementIssueLocator(CsTokenType requiredFollower, params CsTokenType[] tokenTypesToInspect)
        {
            this.requiredFollowers = new[] { requiredFollower };
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public AllTokensByTypeNotFollowedByRequiredElementIssueLocator(CsTokenType requiredFollower, IEnumerable<CsTokenType> tokenTypesToInspect)
        {
            this.requiredFollowers = new[] { requiredFollower };
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public AllTokensByTypeNotFollowedByRequiredElementIssueLocator(IEnumerable<CsTokenType> requiredFollowers, params CsTokenType[] tokenTypesToInspect)
        {
            this.requiredFollowers = requiredFollowers;
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public AllTokensByTypeNotFollowedByRequiredElementIssueLocator(IEnumerable<CsTokenType> requiredFollowers, IEnumerable<CsTokenType> tokenTypesToInspect)
        {
            this.requiredFollowers = requiredFollowers;
            this.tokenTypesToInspect = tokenTypesToInspect;
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
                if (this.tokenTypesToInspect.Contains(token.CsTokenType))
                {
                    issueLocation = token.Location;
                }
                else if (!this.requiredFollowers.Contains(token.CsTokenType) && issueLocation != null)
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

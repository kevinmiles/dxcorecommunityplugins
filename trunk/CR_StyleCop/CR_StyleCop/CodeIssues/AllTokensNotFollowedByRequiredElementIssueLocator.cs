namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AllTokensNotFollowedByRequiredElementIssueLocator : ICodeIssueLocator
    {
        private readonly Func<CsElement, IEnumerable<CsToken>> getTokens;
        private readonly Func<CsToken, bool> isRequiredFollower;
        private readonly Func<CsToken, Violation, bool> reportIssueFor;

        public AllTokensNotFollowedByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, Func<CsToken, Violation, bool> predicate, params CsTokenType[] requiredFollowers)
        {
            this.getTokens = getTokens;
            this.isRequiredFollower = token => requiredFollowers.Contains(token.CsTokenType);
            this.reportIssueFor = predicate;
        }

        public AllTokensNotFollowedByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, Func<CsToken, Violation, bool> predicate, IEnumerable<CsTokenType> requiredFollowers)
        {
            this.getTokens = getTokens;
            this.isRequiredFollower = token => requiredFollowers.Contains(token.CsTokenType);
            this.reportIssueFor = predicate;
        }

        public AllTokensNotFollowedByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, Func<CsToken, Violation, bool> predicate, Func<CsToken, bool> isRequiredFollower)
        {
            this.getTokens = getTokens;
            this.isRequiredFollower = isRequiredFollower;
            this.reportIssueFor = predicate;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            CodeLocation issueLocation = null;
            foreach (var token in this.getTokens(csElement).Flatten().Where(x => x.LineNumber == violation.Line))
            {
                if (!this.isRequiredFollower(token) && issueLocation != null)
                {
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(issueLocation.StartPoint.LineNumber, issueLocation.StartPoint.IndexOnLine + 1, issueLocation.EndPoint.LineNumber, issueLocation.EndPoint.IndexOnLine + 2));
                    issueLocation = null;
                }
                else if (this.reportIssueFor(token, violation))
                {
                    issueLocation = token.Location;
                }
                else
                {
                    issueLocation = null;
                }
            }
        }
    }
}
namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AllTokensByTypeFollowedByBannedElementIssueLocator : ICodeIssueLocator
    {
        private readonly IEnumerable<CsTokenType> bannedFollowers;
        private readonly IEnumerable<CsTokenType> tokenTypesToInspect;
        private readonly Func<CsElement, IEnumerable<CsToken>> getTokens;

        public AllTokensByTypeFollowedByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType bannedFollower, params CsTokenType[] tokenTypesToInspect)
        {
            this.getTokens = getTokens;
            this.bannedFollowers = new[] { bannedFollower };
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public AllTokensByTypeFollowedByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType bannedFollower, IEnumerable<CsTokenType> tokenTypesToInspect)
        {
            this.getTokens = getTokens;
            this.bannedFollowers = new[] { bannedFollower };
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public AllTokensByTypeFollowedByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> bannedFollowers, params CsTokenType[] tokenTypesToInspect)
        {
            this.getTokens = getTokens;
            this.bannedFollowers = bannedFollowers;
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public AllTokensByTypeFollowedByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> bannedFollowers, IEnumerable<CsTokenType> tokenTypesToInspect)
        {
            this.getTokens = getTokens;
            this.bannedFollowers = bannedFollowers;
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            CodeLocation issueLocation = null;
            foreach (var token in this.getTokens(csElement).Where(x => x.LineNumber == violation.Line).Flatten())
            {
                if (this.tokenTypesToInspect.Contains(token.CsTokenType))
                {
                    issueLocation = token.Location;
                }
                else if (this.bannedFollowers.Contains(token.CsTokenType) && issueLocation != null)
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
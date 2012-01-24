namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AllTokensFollowedByWhitespaceAndBannedElementIssueLocator : ICodeIssueLocator
    {
        private readonly Func<CsElement, IEnumerable<CsToken>> getTokens;
        private readonly Func<CsToken, bool> isBannedFollower;
        private readonly Func<CsToken, Violation, bool> reportIssueFor;

        public AllTokensFollowedByWhitespaceAndBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, Func<CsToken, Violation, bool> predicate, params CsTokenType[] bannedFollowers)
        {
            this.getTokens = getTokens;
            this.isBannedFollower = token => bannedFollowers.Contains(token.CsTokenType);
            this.reportIssueFor = predicate;
        }

        public AllTokensFollowedByWhitespaceAndBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, Func<CsToken, Violation, bool> predicate, IEnumerable<CsTokenType> bannedFollowers)
        {
            this.getTokens = getTokens;
            this.isBannedFollower = token => bannedFollowers.Contains(token.CsTokenType);
            this.reportIssueFor = predicate;
        }

        public AllTokensFollowedByWhitespaceAndBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, Func<CsToken, Violation, bool> predicate, Func<CsToken, bool> isBannedFollower)
        {
            this.getTokens = getTokens;
            this.isBannedFollower = isBannedFollower;
            this.reportIssueFor = predicate;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            CsToken potentialViolation = null;
            bool whitespaceFound = false;
            foreach (var token in this.getTokens(csElement).Flatten().Where(x => x.LineNumber == violation.Line))
            {
                if (potentialViolation != null && token.CsTokenType == CsTokenType.WhiteSpace)
                {
                    whitespaceFound = true;
                }
                else if (potentialViolation != null && whitespaceFound && isBannedFollower(token))
                {
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(potentialViolation.Location.StartPoint.LineNumber, potentialViolation.Location.StartPoint.IndexOnLine + 1, potentialViolation.Location.EndPoint.LineNumber, potentialViolation.Location.EndPoint.IndexOnLine + 2));
                    whitespaceFound = false;
                    potentialViolation = null;
                }
                else if (this.reportIssueFor(token, violation))
                {
                    potentialViolation = token;
                    whitespaceFound = false;
                }
                else 
                {
                    whitespaceFound = false;
                    potentialViolation = null;
                }
            }
        }
    }
}


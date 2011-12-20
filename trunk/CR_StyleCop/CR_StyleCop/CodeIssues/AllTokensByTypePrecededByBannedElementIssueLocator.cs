namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AllTokensByTypePrecededByBannedElementIssueLocator : AllTokensByTypeLocator, ICodeIssueLocator
    {
        private readonly Func<CsElement, IEnumerable<CsToken>> getTokens;
        private readonly IEnumerable<CsTokenType> bannedPredecessors;
        private readonly IEnumerable<CsTokenType> tokenTypesToInspect;

        public AllTokensByTypePrecededByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType bannedPredecessor, params CsTokenType[] tokenTypesToInspect)
        {
            this.getTokens = getTokens;
            this.bannedPredecessors = new[] { bannedPredecessor };
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public AllTokensByTypePrecededByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType bannedPredecessor, IEnumerable<CsTokenType> tokenTypesToInspect)
        {
            this.getTokens = getTokens;
            this.bannedPredecessors = new[] { bannedPredecessor };
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public AllTokensByTypePrecededByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> bannedPredecessors, params CsTokenType[] tokenTypesToInspect)
        {
            this.getTokens = getTokens;
            this.bannedPredecessors = bannedPredecessors;
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public AllTokensByTypePrecededByBannedElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> bannedPredecessors, IEnumerable<CsTokenType> tokenTypesToInspect)
        {
            this.getTokens = getTokens;
            this.bannedPredecessors = bannedPredecessors;
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            bool predecessorFound = false;
            foreach (var token in this.getTokens(csElement).Where(x => x.LineNumber == violation.Line).SelectMany(token => Flatten(token)))
            {
                if (predecessorFound && this.tokenTypesToInspect.Contains(token.CsTokenType))
                {
                    predecessorFound = false;
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(token.Location.StartPoint.LineNumber, token.Location.StartPoint.IndexOnLine + 1, token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2));
                }
                else if (this.bannedPredecessors.Contains(token.CsTokenType))
                {
                    predecessorFound = true;
                }
                else
                {
                    predecessorFound = false;
                }
            }
        }
    }
}
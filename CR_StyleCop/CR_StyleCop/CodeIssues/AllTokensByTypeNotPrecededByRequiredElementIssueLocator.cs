namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AllTokensByTypeNotPrecededByRequiredElementIssueLocator : ICodeIssueLocator
    {
        private readonly Func<CsElement, IEnumerable<CsToken>> getTokens;
        private readonly IEnumerable<CsTokenType> requiredPredecessors;
        private readonly IEnumerable<CsTokenType> tokenTypesToInspect;

        public AllTokensByTypeNotPrecededByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType requiredPredecessor, params CsTokenType[] tokenTypesToInspect)
        {
            this.getTokens = getTokens;
            this.requiredPredecessors = new[] { requiredPredecessor };
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public AllTokensByTypeNotPrecededByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, CsTokenType requiredPredecessor, IEnumerable<CsTokenType> tokenTypesToInspect)
        {
            this.getTokens = getTokens;
            this.requiredPredecessors = new[] { requiredPredecessor };
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public AllTokensByTypeNotPrecededByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> requiredPredecessors, params CsTokenType[] tokenTypesToInspect)
        {
            this.getTokens = getTokens;
            this.requiredPredecessors = requiredPredecessors;
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public AllTokensByTypeNotPrecededByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, IEnumerable<CsTokenType> requiredPredecessors, IEnumerable<CsTokenType> tokenTypesToInspect)
        {
            this.getTokens = getTokens;
            this.requiredPredecessors = requiredPredecessors;
            this.tokenTypesToInspect = tokenTypesToInspect;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            bool predecessorFound = true;
            foreach (var token in this.getTokens(csElement).Where(x => x.LineNumber == violation.Line).Flatten())
            {
                if (!predecessorFound && this.tokenTypesToInspect.Contains(token.CsTokenType))
                {
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(token.Location.StartPoint.LineNumber, token.Location.StartPoint.IndexOnLine + 1, token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2));
                    predecessorFound = false;
                }
                else if (this.requiredPredecessors.Contains(token.CsTokenType))
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
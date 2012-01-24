namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AllTokensNotPrecededByRequiredElementIssueLocator : ICodeIssueLocator
    {
        private readonly Func<CsElement, IEnumerable<CsToken>> getTokens;
        private readonly IEnumerable<CsTokenType> requiredPredecessors;
        private readonly Func<CsToken, Violation, bool> reportIssueFor;

        public AllTokensNotPrecededByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, Func<CsToken, Violation, bool> predicate, params CsTokenType[] requiredPredecessors)
        {
            this.getTokens = getTokens;
            this.requiredPredecessors = requiredPredecessors;
            this.reportIssueFor = predicate;
        }

        public AllTokensNotPrecededByRequiredElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, Func<CsToken, Violation, bool> predicate, IEnumerable<CsTokenType> requiredPredecessors)
        {
            this.getTokens = getTokens;
            this.requiredPredecessors = requiredPredecessors;
            this.reportIssueFor = predicate;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            bool predecessorFound = true;
            foreach (var token in this.getTokens(csElement).Flatten().Where(x => x.LineNumber == violation.Line))
            {
                if (!predecessorFound && this.reportIssueFor(token, violation))
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
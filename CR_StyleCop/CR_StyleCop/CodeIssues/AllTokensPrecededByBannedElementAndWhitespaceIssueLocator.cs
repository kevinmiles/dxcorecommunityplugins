namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AllTokensPrecededByBannedElementAndWhitespaceIssueLocator : ICodeIssueLocator
    {
        private readonly Func<CsElement, IEnumerable<CsToken>> getTokens;
        private readonly IEnumerable<CsTokenType> bannedPredecessors;
        private readonly Func<CsToken, Violation, bool> reportIssueFor;

        public AllTokensPrecededByBannedElementAndWhitespaceIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, Func<CsToken, Violation, bool> predicate, params CsTokenType[] bannedPredecessors)
        {
            this.getTokens = getTokens;
            this.bannedPredecessors = bannedPredecessors;
            this.reportIssueFor = predicate;
        }

        public AllTokensPrecededByBannedElementAndWhitespaceIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, Func<CsToken, Violation, bool> predicate, IEnumerable<CsTokenType> bannedPredecessors)
        {
            this.getTokens = getTokens;
            this.bannedPredecessors = bannedPredecessors;
            this.reportIssueFor = predicate;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            int predecessorFound = 0;
            foreach (var token in this.getTokens(csElement).Where(x => x.LineNumber == violation.Line).Flatten())
            {
                if (predecessorFound == 2 && this.reportIssueFor(token, violation))
                {
                    predecessorFound = 0;
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(token.Location.StartPoint.LineNumber, token.Location.StartPoint.IndexOnLine + 1, token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2));
                }
                else if (this.bannedPredecessors.Contains(token.CsTokenType))
                {
                    predecessorFound = 1;
                }
                else if (predecessorFound == 1 && token.CsTokenType == CsTokenType.WhiteSpace)
                {
                    predecessorFound = 2;
                }
                else
                {
                    predecessorFound = 0;
                }
            }
        }
    }
}
namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AllTokensPrecededByBannedWhitespaceNotPrecededBySpecialElementIssueLocator : ICodeIssueLocator
    {
        private readonly Func<CsElement, IEnumerable<CsToken>> getTokens;
        private readonly IEnumerable<CsTokenType> specialPredecessors;
        private readonly Func<CsToken, Violation, bool> reportIssueFor;

        public AllTokensPrecededByBannedWhitespaceNotPrecededBySpecialElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, Func<CsToken, Violation, bool> predicate, params CsTokenType[] specialPredecessors)
        {
            this.getTokens = getTokens;
            this.specialPredecessors = specialPredecessors;
            this.reportIssueFor = predicate;
        }

        public AllTokensPrecededByBannedWhitespaceNotPrecededBySpecialElementIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens, Func<CsToken, Violation, bool> predicate, IEnumerable<CsTokenType> specialPredecessors)
        {
            this.getTokens = getTokens;
            this.specialPredecessors = specialPredecessors;
            this.reportIssueFor = predicate;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            bool specialPredecessorFound = true;
            bool whitespaceFound = false;
            foreach (var token in this.getTokens(csElement).Flatten().Where(x => x.LineNumber == violation.Line))
            {
                if (!specialPredecessorFound && whitespaceFound && this.reportIssueFor(token, violation))
                {
                    whitespaceFound = false;
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(token.Location.StartPoint.LineNumber, token.Location.StartPoint.IndexOnLine + 1, token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2));
                }
                else if (this.specialPredecessors.Contains(token.CsTokenType))
                {
                    specialPredecessorFound = true;
                    whitespaceFound = false;
                }
                else if (token.CsTokenType == CsTokenType.WhiteSpace)
                {
                    whitespaceFound = true;
                }
                else
                {
                    specialPredecessorFound = false;
                    whitespaceFound = false;
                }
            }
        }
    }
}
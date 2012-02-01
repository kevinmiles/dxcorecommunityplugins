namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class ElementNameIssueLocator : ICodeIssueLocator
    {
        private readonly Func<CsToken, bool> reportViolation;

        public ElementNameIssueLocator()
            : this(_ => true)
        {
        }

        public ElementNameIssueLocator(Func<CsToken, bool> reportViolation)
        {
            this.reportViolation = reportViolation;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode, 
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate, 
            Violation violation, 
            CsElement csElement)
        {
            foreach (var token in csElement.ElementTokens.Flatten().Where(
                x => x.LineNumber == violation.Line
                    && x.CsTokenType == CsTokenType.Other
                    && csElement.Name.Contains(x.Text)
                    && this.reportViolation(x)))
            {
                var startPoint = token.Location.StartPoint;
                var endPoint = token.Location.EndPoint;
                var range = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, endPoint.LineNumber, endPoint.IndexOnLine + 2);
                yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, range);
            }
        }
    }
}

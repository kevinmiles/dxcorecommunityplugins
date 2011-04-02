namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class WholeElementIssueLocator : ICodeIssueLocator
    {
        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            IDocument document,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            var startPoint = csElement.ElementTokens.First().Location.StartPoint;
            var endPoint = csElement.ElementTokens.Last().Location.EndPoint;
            var sourceRange = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, endPoint.LineNumber, endPoint.IndexOnLine + 2);
            yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, sourceRange);
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class LastXmlDocCommentIssueLocator : ICodeIssueLocator
    {
        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            var token = csElement.Header.ChildTokens.Last();
            if (token == null)
            {
                return Enumerable.Empty<StyleCopCodeIssue>();
            }

            var location = token.Location;
            return new[] { new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(location.StartPoint.LineNumber, location.StartPoint.IndexOnLine + 1, location.EndPoint.LineNumber, location.EndPoint.IndexOnLine + 2)) };
        }
    }
}

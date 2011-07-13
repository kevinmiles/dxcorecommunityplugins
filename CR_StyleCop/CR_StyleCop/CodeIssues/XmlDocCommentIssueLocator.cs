namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class XmlDocCommentIssueLocator : ICodeIssueLocator
    {
        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            return (from token in csElement.Header.ChildTokens
                    where token.LineNumber == violation.Line
                    let location = token.Location
                    select new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(location.StartPoint.LineNumber, location.StartPoint.IndexOnLine + 1, location.EndPoint.LineNumber, location.EndPoint.IndexOnLine + 2))).Take(1);
        }
    }
}

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
        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode, 
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate, 
            Violation violation, 
            CsElement csElement)
        {
            return (from token in csElement.ElementTokens
                    where csElement.Name.EndsWith(token.Text)
                    let startPoint = token.Location.StartPoint
                    let endPoint = token.Location.EndPoint
                    let range = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, endPoint.LineNumber, endPoint.IndexOnLine + 2)
                    select new StyleCopCodeIssue(CodeIssueType.CodeSmell, range))
                    .Take(1);
        }
    }
}

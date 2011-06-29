namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;
    using System.Collections.Generic;

    internal class SA1626_SingleLineCommentsMustNotUseDocumentationStyleSlashes : StyleCopRule
    {
        public SA1626_SingleLineCommentsMustNotUseDocumentationStyleSlashes()
            : base(new IssueLocator())
        {
        }

        internal class IssueLocator : ICodeIssueLocator
        {
            public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
                ISourceCode sourceCode, 
                Func<ElementTypeFilter, IEnumerable<IElement>> crEnumerable, 
                Violation violation, 
                CsElement csElement)
            {
                return from token in csElement.ElementTokens
                       where token.Text.StartsWith("///") && !token.Text.StartsWith("////")
                       let startPoint = token.Location.StartPoint
                       let endPoint = token.Location.EndPoint
                       let sourceRange = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, endPoint.LineNumber, endPoint.IndexOnLine + 2)
                       select new StyleCopCodeIssue(CodeIssueType.CodeSmell, sourceRange);
            }
        }
    }
}

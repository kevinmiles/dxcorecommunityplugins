namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1004_DocumentationLinesMustBeginWithSingleSpace : StyleCopRule
    {
        public SA1004_DocumentationLinesMustBeginWithSingleSpace()
            : base(new IssueLocator())
        {
        }

        private class IssueLocator : ICodeIssueLocator
        {
            public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
                ISourceCode sourceCode, 
                Func<ElementTypeFilter, IEnumerable<IElement>> enumerate, 
                Violation violation, 
                CsElement csElement)
            {
                foreach (var token in csElement.Header.ChildTokens.Where(childToken => childToken.CsTokenType == CsTokenType.XmlHeaderLine
                    && ((childToken.Text.Length > 3 && childToken.Text[3] != ' ') 
                        || (childToken.Text.Length > 4 && childToken.Text[4] == ' '))))
                {
                    var location = token.Location;
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(location.StartPoint.LineNumber, location.StartPoint.IndexOnLine + 1, location.EndPoint.LineNumber, location.EndPoint.IndexOnLine + 2));
                }
            }
        }
    }
}

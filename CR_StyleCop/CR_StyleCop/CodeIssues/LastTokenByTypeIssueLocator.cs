namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class LastTokenByTypeIssueLocator : ICodeIssueLocator
    {
        private readonly IEnumerable<CsTokenType> tokenTypes;

        public LastTokenByTypeIssueLocator(params CsTokenType[] tokenTypes)
        {
            this.tokenTypes = tokenTypes;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            IDocument document,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            var issue = (from token in csElement.ElementTokens
                         where token.LineNumber == violation.Line && this.tokenTypes.Contains(token.CsTokenType)
                         let location = token.Location
                         select new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(location.StartPoint.LineNumber, location.StartPoint.IndexOnLine + 1, location.EndPoint.LineNumber, location.EndPoint.IndexOnLine + 2))).LastOrDefault();
            if (issue != null)
            {
                yield return issue;
            }
        }
    }
}

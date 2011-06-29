namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class FirstToLastTokenByTypeIssueLocator: ICodeIssueLocator
    {
        private readonly IEnumerable<CsTokenType> tokenTypes;

        public FirstToLastTokenByTypeIssueLocator(params CsTokenType[] tokenTypes)
        {
            this.tokenTypes = tokenTypes;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            var locations = from token in csElement.ElementTokens
                   where token.LineNumber == violation.Line && this.tokenTypes.Contains(token.CsTokenType)
                   select token.Location;
            var firstToken = locations.FirstOrDefault();
            var lastToken = locations.LastOrDefault();

            if (firstToken != null && lastToken != null)
            {
                yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(firstToken.StartPoint.LineNumber, firstToken.StartPoint.IndexOnLine + 1, lastToken.EndPoint.LineNumber, lastToken.EndPoint.IndexOnLine + 2));
            }
        }
    }
}

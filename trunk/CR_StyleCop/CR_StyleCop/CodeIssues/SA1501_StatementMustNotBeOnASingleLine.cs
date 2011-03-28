namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1501_StatementMustNotBeOnASingleLine : StyleCopRule
    {
        public SA1501_StatementMustNotBeOnASingleLine()
            : base(new IssueLocator())
        {
        }

        internal class IssueLocator : ICodeIssueLocator
        {
            public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
                IDocument document, 
                Func<ElementTypeFilter, IEnumerable<IElement>> enumerate, 
                Violation violation, 
                CsElement csElement)
            {
                CsToken startToken = null;
                foreach (var token in from token in csElement.ElementTokens
                                      where violation.Line == token.Location.StartPoint.LineNumber
                                        && (token.CsTokenType == CsTokenType.OpenCurlyBracket 
                                            || token.CsTokenType == CsTokenType.CloseCurlyBracket)
                                      select token)
                {
                    if (token.CsTokenType == CsTokenType.OpenCurlyBracket)
                    {
                        startToken = token;
                        continue;
                    }

                    if (token.CsTokenType == CsTokenType.CloseCurlyBracket)
                    {
                        var sourceRange = new SourceRange(startToken.Location.StartPoint.LineNumber, startToken.Location.StartPoint.IndexOnLine + 1, token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2);
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, sourceRange);
                    }
                }
            }
        }
    }
}

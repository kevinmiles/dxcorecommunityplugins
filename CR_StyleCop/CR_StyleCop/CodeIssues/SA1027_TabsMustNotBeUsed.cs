namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1027_TabsMustNotBeUsed : StyleCopRule
    {
        public SA1027_TabsMustNotBeUsed()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new IssueLocator(ElementTokens),
                }))
        {
        }

        private class IssueLocator : ICodeIssueLocator
        {
            private readonly Func<CsElement, IEnumerable<CsToken>> getTokens;

            public IssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens)
            {
                this.getTokens = getTokens;
            }

            public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
                ISourceCode sourceCode, 
                Func<ElementTypeFilter, IEnumerable<IElement>> enumerate, 
                Violation violation, 
                CsElement csElement)
            {
                foreach (var token in this.getTokens(csElement).Flatten().Where(x => x.LineNumber == violation.Line))
                {
                    if (token.CsTokenType == CsTokenType.WhiteSpace && token.Text.Contains("\t"))
                    {
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(token.Location.StartPoint.LineNumber, token.Location.StartPoint.IndexOnLine + 1, token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2));
                    }
                }
            }
        }
    }
}

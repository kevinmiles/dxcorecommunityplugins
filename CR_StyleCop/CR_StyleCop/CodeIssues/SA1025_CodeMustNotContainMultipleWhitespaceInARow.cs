namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1025_CodeMustNotContainMultipleWhitespaceInARow : StyleCopRule
    {
        public SA1025_CodeMustNotContainMultipleWhitespaceInARow()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new IssueLocator(ElementTokens),
                    new IssueLocator(AttributesTokens),
                }))
        {
        }

        private class IssueLocator : ICodeIssueLocator
        {
            private static readonly IEnumerable<CsTokenType> requiredPredecessors = new[] { CsTokenType.Comma, CsTokenType.Semicolon };
            private static readonly IEnumerable<CsTokenType> requiredFollowers = new[] { CsTokenType.OperatorSymbol, CsTokenType.EndOfLine};
            private static readonly Func<CsToken, Violation, bool> reportIssueFor = (token, violation) =>
                token.CsTokenType == CsTokenType.WhiteSpace && token.Text.Length > 1;

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
                bool predecessorFound = true;
                CodeLocation issueLocation = null;
                foreach (var token in this.getTokens(csElement).Flatten().Where(x => x.LineNumber == violation.Line))
                {
                    if (!predecessorFound && !requiredFollowers.Contains(token.CsTokenType) && issueLocation != null)
                    {
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(issueLocation.StartPoint.LineNumber, issueLocation.StartPoint.IndexOnLine + 1, issueLocation.EndPoint.LineNumber, issueLocation.EndPoint.IndexOnLine + 2));
                        predecessorFound = false;
                        issueLocation = null;
                    }
                    else if (reportIssueFor(token, violation))
                    {
                        issueLocation = token.Location;
                    }
                    else if (requiredPredecessors.Contains(token.CsTokenType))
                    {
                        predecessorFound = true;
                    }
                    else
                    {
                        predecessorFound = false;
                        issueLocation = null;
                    }
                }
            }
        }
    }
}

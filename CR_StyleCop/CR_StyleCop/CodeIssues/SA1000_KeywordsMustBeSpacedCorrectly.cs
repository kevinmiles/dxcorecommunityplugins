namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1000_KeywordsMustBeSpacedCorrectly : StyleCopRule
    {
        public SA1000_KeywordsMustBeSpacedCorrectly()
            : base(new IssueLocator())
        {
        }

        internal class IssueLocator : ICodeIssueLocator
        {
            private readonly IEnumerable<CsTokenType> tokenTypesWithoutSpace = new[]
                {
                    CsTokenType.DefaultValue,
                    CsTokenType.Typeof,
                    CsTokenType.Sizeof,
                    CsTokenType.Checked,
                    CsTokenType.Unchecked       
                };

            private readonly IEnumerable<CsTokenType> tokenTypesWithRequiredSpace = new[]
                {
                    CsTokenType.For,
                    CsTokenType.Foreach,
                    CsTokenType.While,
                    CsTokenType.If,
                    CsTokenType.Lock,
                    CsTokenType.Using,
                    CsTokenType.Fixed,
                    CsTokenType.Switch,
                    CsTokenType.Catch,
                    CsTokenType.Throw,
                    CsTokenType.Return,
                    CsTokenType.In, 
                    CsTokenType.Where, 
                    CsTokenType.Group, 
                    CsTokenType.By,
                    CsTokenType.Into,
                    CsTokenType.OrderBy,
                    CsTokenType.Let,
                    CsTokenType.Select,
                    CsTokenType.From,
                    CsTokenType.Join,
                    CsTokenType.On,
                    CsTokenType.Equals,
                    CsTokenType.New
                };

            public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
                ISourceCode sourceCode, 
                Func<ElementTypeFilter, IEnumerable<IElement>> enumerate, 
                Violation violation, 
                CsElement csElement)
            {
                CodeLocation issueLocation = null;
                foreach (var token in csElement.ElementTokens.Where(x => x.LineNumber == violation.Line))
                {
                    if (this.tokenTypesWithRequiredSpace.Contains(token.CsTokenType))
                    {
                        issueLocation = token.Location;
                    }
                    else if (token.CsTokenType != CsTokenType.WhiteSpace && issueLocation != null)
                    {
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(issueLocation.StartPoint.LineNumber, issueLocation.StartPoint.IndexOnLine + 1, issueLocation.EndPoint.LineNumber, issueLocation.EndPoint.IndexOnLine + 2));
                        issueLocation = null;
                    }
                    else
                    {
                        issueLocation = null;
                    }
                }

                foreach (var token in csElement.ElementTokens.Where(x => x.LineNumber == violation.Line))
                {
                    if (this.tokenTypesWithoutSpace.Contains(token.CsTokenType))
                    {
                        issueLocation = token.Location;
                    }
                    else if (token.CsTokenType == CsTokenType.WhiteSpace && issueLocation != null)
                    {
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(issueLocation.StartPoint.LineNumber, issueLocation.StartPoint.IndexOnLine + 1, issueLocation.EndPoint.LineNumber, issueLocation.EndPoint.IndexOnLine + 2));
                        issueLocation = null;
                    }
                    else
                    {
                        issueLocation = null;
                    }
                }
            }
        }
    }
}

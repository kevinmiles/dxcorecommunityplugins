namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1403_FileMayOnlyContainASingleNamespace : StyleCopRule
    {
        public SA1403_FileMayOnlyContainASingleNamespace()
            : base(new IssueLocator())
        {
        }

        internal class IssueLocator : ICodeIssueLocator
        {
            public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
                ISourceCode sourceCode, 
                Func<ElementTypeFilter, IEnumerable<IElement>> enumerate, 
                Violation violation, 
                CsElement csElement)
            {
                SourcePoint? startPoint = null;
                SourcePoint? endPoint = null;
                foreach (var token in from token in csElement.ElementTokens
                                      where token.LineNumber == violation.Line && token.CsTokenType != CsTokenType.WhiteSpace
                                      select token)
                {
                    if (token.CsTokenType == CsTokenType.Namespace)
                    {
                        startPoint = endPoint = new SourcePoint(token.Location.StartPoint.LineNumber, token.Location.StartPoint.IndexOnLine + 1);
                    }
                    else if (token.CsTokenType == CsTokenType.OpenCurlyBracket || token.CsTokenType == CsTokenType.EndOfLine)
                    {
                        if (startPoint != null)
                        {
                            var sourceRange = new SourceRange(startPoint.Value, endPoint.Value);
                            yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, sourceRange);
                        }

                        yield break;
                    }
                    else if (startPoint != null)
                    {
                        endPoint = new SourcePoint(token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2);
                    }
                }
            }
        }
    }
}

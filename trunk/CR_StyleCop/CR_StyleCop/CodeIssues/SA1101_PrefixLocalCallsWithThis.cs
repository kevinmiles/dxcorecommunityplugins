namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1101_PrefixLocalCallsWithThis : StyleCopRule
    {
        public SA1101_PrefixLocalCallsWithThis()
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
                int prefixLength = "The call to ".Length;
                var memberName = violation.Message.Substring(prefixLength, violation.Message.IndexOf(" must") - prefixLength);
                var thisFound = false;
                foreach (var token in from token in csElement.ElementTokens
                                      where token.LineNumber == violation.Line && token.CsTokenType != CsTokenType.WhiteSpace
                                      select token)
                {
                    if (token.CsTokenType == CsTokenType.This || (thisFound && token.Text == "."))
                    {
                        thisFound = true;
                    }
                    else
                    {
                        if (token.Text == memberName && !thisFound)
                        {
                            var sourceRange = new SourceRange(token.Location.StartPoint.LineNumber, token.Location.StartPoint.IndexOnLine + 1, token.Location.EndPoint.LineNumber, token.Location.EndPoint.IndexOnLine + 2);
                            yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, sourceRange);
                        }

                        thisFound = false;
                    }
                }
            }
        }
    }
}

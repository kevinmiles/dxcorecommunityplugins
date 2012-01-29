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
                    new IssueLocator(AttributesTokens),
                    new IssueLocator(XmlDocTokens),
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
                int tabSize = sourceCode is FileSourceCode ? 4 : CodeRush.VSSettings.GetTabSettings(ParserLanguageID.CSharp).TabSize;
                int charIndex = 1;
                int start = 0;
                int end = 0;
                bool inTabs = false;
                foreach (char character in sourceCode.GetText(violation.Line))
                {
                    if (character == '\t')
                    {
                        if (inTabs)
                        {
                            end += tabSize;
                        }
                        else
                        {
                            start = charIndex;
                            inTabs = true;
                            end = charIndex + tabSize - ((charIndex - 1) % tabSize);
                        }
                    }
                    else
                    {
                        if (inTabs)
                        {
                            inTabs = false;
                            charIndex = end + 1;
                            yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(violation.Line, start, violation.Line, end));
                        }
                        else
                        {
                            charIndex++;
                        }
                    }
                }
            }
        }
    }
}

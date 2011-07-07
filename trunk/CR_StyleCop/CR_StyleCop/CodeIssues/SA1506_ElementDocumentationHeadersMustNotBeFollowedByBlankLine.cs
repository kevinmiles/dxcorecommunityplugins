namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1506_ElementDocumentationHeadersMustNotBeFollowedByBlankLine : StyleCopRule
    {
        public SA1506_ElementDocumentationHeadersMustNotBeFollowedByBlankLine()
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
                for (int lineNumber = violation.Line + 1; lineNumber < sourceCode.LineCount; lineNumber++)
                {
                    string lineText = sourceCode.GetText(lineNumber).Trim();
                    if (string.IsNullOrEmpty(lineText))
                    {
                        var sourceRange = new SourceRange(lineNumber, 1, lineNumber, Math.Max(2, sourceCode.LengthOfLine(lineNumber) + 1));
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, sourceRange);
                    }
                }
            }
        }
    }
}

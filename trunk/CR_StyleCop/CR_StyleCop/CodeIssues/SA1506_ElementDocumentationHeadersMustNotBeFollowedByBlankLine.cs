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
                for (int lineNumber = violation.Line; lineNumber < sourceCode.LineCount; lineNumber++)
                {
                    string nextLine = sourceCode.GetText(lineNumber + 1).Trim();
                    if (string.IsNullOrEmpty(nextLine))
                    {
                        string lineText = sourceCode.GetText(lineNumber);
                        string textToUnderline = lineText.TrimStart();
                        var sourceRange = new SourceRange(lineNumber, lineText.Length - textToUnderline.Length, lineNumber, sourceCode.LengthOfLine(lineNumber) + 1);
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, sourceRange);
                    }
                }
            }
        }
    }
}

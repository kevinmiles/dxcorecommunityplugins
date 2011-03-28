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
                IDocument document, 
                Func<ElementTypeFilter, IEnumerable<IElement>> enumerate, 
                Violation violation, 
                CsElement csElement)
            {
                for (int lineNumber = violation.Line; lineNumber < document.LineCount; lineNumber++)
                {
                    string nextLine = document.GetText(lineNumber + 1).Trim();
                    if (string.IsNullOrEmpty(nextLine))
                    {
                        string lineText = document.GetText(lineNumber);
                        string textToUnderline = lineText.TrimStart();
                        var sourceRange = new SourceRange(lineNumber, lineText.Length - textToUnderline.Length, lineNumber, document.LengthOfLine(lineNumber) + 1);
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, sourceRange);
                    }
                }
            }
        }
    }
}

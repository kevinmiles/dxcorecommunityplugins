namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class WholeLineIssueLocator : ICodeIssueLocator
    {
        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode, 
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate, 
            Violation violation, 
            CsElement csElement)
        {
            yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(violation.Line, 1, violation.Line, Math.Max(2, sourceCode.LengthOfLine(violation.Line) + 1)));
        }
    }
}

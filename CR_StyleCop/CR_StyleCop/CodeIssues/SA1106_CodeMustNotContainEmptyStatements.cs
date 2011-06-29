namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;
    using DX = DevExpress.CodeRush.StructuralParser;
    using System.Collections.Generic;

    internal class SA1106_CodeMustNotContainEmptyStatements : StyleCopRule
    {
        public SA1106_CodeMustNotContainEmptyStatements()
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
                return from x in enumerate(new ElementTypeFilter(LanguageElementType.Statement))
                       let statement = (DX.Statement)x.ToLanguageElement()
                       where statement.StartLine == violation.Line
                           && statement.DetailNodeCount == 0
                       select new StyleCopCodeIssue(CodeIssueType.CodeSmell, statement.Range);
            }
        }
    }
}

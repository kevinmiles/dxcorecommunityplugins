namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;
    using DX = DevExpress.CodeRush.StructuralParser;

    internal class SA1409_RemoveUnnecessaryCode : StyleCopRule
    {
        public SA1409_RemoveUnnecessaryCode()
            : base(new IssueLocator())
        {
        }

        private class IssueLocator : ICodeIssueLocator
        {
            private LanguageElementType[] deleteWhenEmpty = new LanguageElementType[]
            {
                LanguageElementType.Checked,
                LanguageElementType.Unchecked,
                LanguageElementType.Lock,
                LanguageElementType.UnsafeStatement,
                LanguageElementType.Try,
                LanguageElementType.Finally
            };

            public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
                ISourceCode sourceCode,
                Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
                Violation violation,
                CsElement csElement)
            {
                foreach (var method in from x in enumerate(new ElementTypeFilter(LanguageElementType.Method))
                                       let method = (DX.Method)x.ToLanguageElement()
                                       where method.RecoveredRange.Start.Line == violation.Line
                                          && method.IsStatic && method.IsConstructor
                                          && method.FirstChild == null
                                       select method)
                {
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, method.NameRange);
                }

                foreach (var statement in from x in enumerate(new ElementTypeFilter(deleteWhenEmpty))
                                          let statement = x.ToLanguageElement()
                                          where statement.RecoveredRange.Start.Line == violation.Line
                                             && (statement.ElementType == LanguageElementType.Try || statement.FirstChild == null)
                                          select statement)
                {
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, statement.RecoveredRange);
                }
            }
        }
    }
}

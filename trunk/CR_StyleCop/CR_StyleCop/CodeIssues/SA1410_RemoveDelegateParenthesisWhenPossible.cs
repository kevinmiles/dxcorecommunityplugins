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

    internal class SA1410_RemoveDelegateParenthesisWhenPossible : StyleCopRule
    {
        public SA1410_RemoveDelegateParenthesisWhenPossible()
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
                foreach (var method in from x in enumerate(new ElementTypeFilter(LanguageElementType.AnonymousMethodExpression))
                                       let method = (DX.AnonymousMethodExpression)x.ToLanguageElement()
                                       where method.RecoveredRange.Start.Line == violation.Line
                                          && method.ParameterCount == 0
                                          && !method.ParamOpenRange.IsEmpty
                                          && !method.ParamCloseRange.IsEmpty
                                       select method)
                {
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(method.RecoveredRange.Start.Line, method.RecoveredRange.Start.Offset, method.ParamCloseRange.End.Line, method.ParamCloseRange.End.Offset));
                }
            }
        }
    }
}

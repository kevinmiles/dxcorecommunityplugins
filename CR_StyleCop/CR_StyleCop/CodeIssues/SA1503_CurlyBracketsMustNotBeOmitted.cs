namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1503_CurlyBracketsMustNotBeOmitted : StyleCopRule
    {
        public SA1503_CurlyBracketsMustNotBeOmitted()
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
                foreach (ParentingStatement statement in enumerate(new ElementTypeFilter(new[] { LanguageElementType.If, LanguageElementType.While, LanguageElementType.Lock, LanguageElementType.For, LanguageElementType.ForEach, LanguageElementType.Do, LanguageElementType.UsingStatement }))
                                                            .Select(element => element.ToLanguageElement()))
                {
                    if (!statement.HasDelimitedBlock 
                        && statement.NodeCount == 1 
                        && statement.BlockCodeRange.Start.Line == violation.Line 
                        && violation.Message.Contains(statement.GetKeyword()))
                    {
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, statement.BlockCodeRange);
                    }
                }
            }
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1404_CodeAnalysisSuppressionMustHaveJustification : StyleCopRule
    {
        public SA1404_CodeAnalysisSuppressionMustHaveJustification()
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
                foreach (IElement element in from x in enumerate(new ElementTypeFilter(LanguageElementType.Attribute))
                                             where x.FirstNameRange.Start.Line == violation.Line
                                             select x)
                {
                    if (element.Name.Contains("SuppressMessage"))
                    {
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, element.FirstNameRange);
                    }
                }
            }
        }
    }
}

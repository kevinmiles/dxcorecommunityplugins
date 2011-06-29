namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class WrongElementOrderIssueLocator : ICodeIssueLocator
    {
        private LanguageElementType[] orderableElements = new[] 
            {
                LanguageElementType.ExternAlias,
                LanguageElementType.NamespaceReference,
                LanguageElementType.Namespace,
                LanguageElementType.Delegate,
                LanguageElementType.Enum,
                LanguageElementType.Interface,
                LanguageElementType.Struct,
                LanguageElementType.Class,
                LanguageElementType.Variable,
                LanguageElementType.InitializedVariable,
                LanguageElementType.Const,
                LanguageElementType.Method,
                LanguageElementType.Event,
                LanguageElementType.Property
            };

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            foreach (var element in from x in enumerate(new ElementTypeFilter(orderableElements))
                                    where x.FirstNameRange.Start.Line == violation.Line
                                    select x)
            {
                var nameRange = element.FirstNameRange;
                var sourceRange = new SourceRange(nameRange.Start.Line, nameRange.Start.Offset, nameRange.End.Line, nameRange.End.Offset);
                yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, sourceRange);
            }
        }
    }
}

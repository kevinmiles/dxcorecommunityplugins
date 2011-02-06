namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    internal class WrongElementOrderCodeIssue : ICodeIssue
    {
        private LanguageElementType[] orderableElements = new [] 
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

        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            string message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            CsElement csElement = violation.Element as CsElement;
            if (csElement == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }

            foreach (IElement element in ea.GetEnumerable(ea.Scope, new ElementTypeFilter(orderableElements)).Where(x => x.FirstNameRange.Start.Line == violation.Line))
            {
                TextRange nameRange = element.FirstNameRange;
                SourceRange sourceRange = new SourceRange(nameRange.Start.Line, nameRange.Start.Offset, nameRange.End.Line, nameRange.End.Offset);
                ea.AddSmell(sourceRange, message, 10);
                return;
            }
        }
    }
}

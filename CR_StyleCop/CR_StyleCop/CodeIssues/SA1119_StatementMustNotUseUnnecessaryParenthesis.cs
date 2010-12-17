namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using DevExpress.CodeRush.Diagnostics.General;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    internal class SA1119_StatementMustNotUseUnnecessaryParenthesis : ICodeIssue
    {
        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            string message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            var csElement = violation.Element as CsElement;
            if (csElement == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }

            foreach (IElement element in ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.ParenthesizedExpression)).Where(x => x.FirstNameRange.Start.Line == violation.Line))
            {
                Log.SendMsg("CR_StyleCop: element found " + element.Name);
                Log.SendMsg("CR_StyleCop: element parent is " + element.Parent.ElementType);
                if (element.Parent.ElementType == LanguageElementType.Assignment
                    || element.Parent.ElementType == LanguageElementType.ImplicitVariable
                    || element.Parent.ElementType == LanguageElementType.InitializedVariable
                    || element.Parent.ElementType == LanguageElementType.Return)
                {
                    ea.AddSmell(element.FirstNameRange, message, 10);
                }
            }
        }
    }
}

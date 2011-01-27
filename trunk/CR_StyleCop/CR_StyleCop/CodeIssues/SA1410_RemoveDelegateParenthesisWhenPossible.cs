namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;
    using DX = DevExpress.CodeRush.StructuralParser;

    internal class SA1410_RemoveDelegateParenthesisWhenPossible : ICodeIssue
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

            foreach (var method in from x in ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.AnonymousMethodExpression))
                                      let method = (DX.AnonymousMethodExpression)x.ToLanguageElement()
                                      where method.RecoveredRange.Start.Line == violation.Line
                                         && method.ParameterCount == 0 
                                         && !method.ParamOpenRange.IsEmpty
                                         && !method.ParamCloseRange.IsEmpty
                                      select method)
            {
                ea.AddSmell(new SourceRange(method.RecoveredRange.Start.Line, method.RecoveredRange.Start.Offset, method.ParamCloseRange.End.Line, method.ParamCloseRange.End.Offset), message, 10);
            }
        }
    }
}

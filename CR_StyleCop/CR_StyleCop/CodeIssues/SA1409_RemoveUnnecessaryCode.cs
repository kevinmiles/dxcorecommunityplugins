namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;
    using DX = DevExpress.CodeRush.StructuralParser;

    internal class SA1409_RemoveUnnecessaryCode : ICodeIssue
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

        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            string message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            var csElement = violation.Element as CsElement;
            if (csElement == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }

            foreach (var method in from x in ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.Method))
                                   let method = (DX.Method)x.ToLanguageElement()
                                   where method.RecoveredRange.Start.Line == violation.Line
                                      && method.IsStatic && method.IsConstructor
                                      && method.FirstChild == null
                                   select method)
            {
                ea.AddSmell(method.RecoveredRange, message, 10);
            }

            foreach (var statement in from x in ea.GetEnumerable(ea.Scope, new ElementTypeFilter(deleteWhenEmpty))
                                   let statement = x.ToLanguageElement()
                                   where statement.RecoveredRange.Start.Line == violation.Line
                                      && (statement.ElementType == LanguageElementType.Try || statement.FirstChild == null)
                                   select statement)
            {
                ea.AddSmell(statement.RecoveredRange, message, 10);
            }
        }
    }
}

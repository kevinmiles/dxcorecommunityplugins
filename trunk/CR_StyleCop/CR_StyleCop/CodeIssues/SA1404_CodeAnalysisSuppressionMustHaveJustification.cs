using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using Microsoft.StyleCop;
using Microsoft.StyleCop.CSharp;

namespace CR_StyleCop.CodeIssues
{
    internal class SA1404_CodeAnalysisSuppressionMustHaveJustification : ICodeIssue
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

            foreach (IElement element in ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.Attribute)).Where(x => x.FirstNameRange.Start.Line == violation.Line))
            {
                if (element.Name.Contains("SuppressMessage"))
                {
                    ea.AddSmell(element.FirstNameRange, message, 10);
                }
            }
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    internal class MethodCallCodeIssue : ICodeIssue
    {
        private readonly string methodName;
        private readonly Func<MethodCall, bool> qualifyParameters;

        public MethodCallCodeIssue(string methodName, Func<MethodCall, bool> qualifyParameters)
        {
            this.methodName = methodName;
            this.qualifyParameters = qualifyParameters;
        }

        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            string message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            CsElement csElement = violation.Element as CsElement;
            if (csElement == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }

            foreach (IElement element in ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.MethodCall)).Where(x => x.FirstNameRange.Start.Line == violation.Line))
            {
                if (element.Name == this.methodName && this.qualifyParameters((MethodCall)element.ToLanguageElement()))
                {
                    ea.AddSmell(element.FirstNameRange, message, 10);
                }
            }
        }
    }
}

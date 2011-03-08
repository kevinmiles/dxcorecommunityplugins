namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;
    using DX = DevExpress.CodeRush.StructuralParser;

    internal class SA1106_CodeMustNotContainEmptyStatements : ICodeIssue
    {
        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            var message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            var csElement = violation.Element as CsElement;
            if (csElement == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }

            DevExpress.CodeRush.Diagnostics.General.Log.SendMsg("SA1106 hit");
            foreach (var emptyStatement in from x in ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.Statement))
                                           let statement = (DX.Statement)x.ToLanguageElement()
                                           where statement.StartLine == violation.Line
                                                && statement.DetailNodeCount == 0
                                           select statement)
            {
                DevExpress.CodeRush.Diagnostics.General.Log.SendMsg("SA1106 hit x");
                ea.AddSmell(emptyStatement.Range, message, 10);
            }
        }
    }
}

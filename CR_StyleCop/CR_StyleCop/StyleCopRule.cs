namespace CR_StyleCop
{
    using System;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class StyleCopRule : IStyleCopRule
    {
        private ICodeIssueLocator issueLocator;

        public StyleCopRule(ICodeIssueLocator issueLocator)
        {
            this.issueLocator = issueLocator;
        }

        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, ISourceCode sourceCode, Violation violation)
        {
            var message = string.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            var csElement = violation.Element as CsElement;
            if (csElement == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, sourceCode.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }

            foreach (var styleCopCodeIssue in this.issueLocator.GetCodeIssues(sourceCode, filter => ea.GetEnumerable(ea.Scope, filter), violation, csElement))
            {
                ea.AddIssue(styleCopCodeIssue.IssueType, styleCopCodeIssue.Range, message);
            }
        }
    }
}

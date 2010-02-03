namespace CR_StyleCop.CodeIssues
{
    using System;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
 
    internal class NullCodeIssue : ICodeIssue
    {
        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
        }
    }
}

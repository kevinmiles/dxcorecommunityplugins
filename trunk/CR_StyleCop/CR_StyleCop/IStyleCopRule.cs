namespace CR_StyleCop
{
    using System;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;

    internal interface IStyleCopRule
    {
        void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation);
    }
}

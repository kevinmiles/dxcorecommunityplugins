namespace CR_StyleCop
{
    using System;
    using DevExpress.CodeRush.Core;
    using StyleCop;

    internal interface IStyleCopRule
    {
        void AddViolationIssue(CheckCodeIssuesEventArgs ea, ISourceCode sourceCode, Violation violation);
    }
}

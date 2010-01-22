namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using CR_StyleCop.CodeIssues;
    using Microsoft.StyleCop;

    internal class CodeIssueFactory
    {
        private Dictionary<string, ICodeIssue> handlers = new Dictionary<string, ICodeIssue>();
        private ICodeIssue nullHandler = new NullCodeIssue();

        public CodeIssueFactory()
        {
            this.handlers.Add("SA1600", new SA1600ElementsMustBeDocumentedCodeIssue());
        }

        public ICodeIssue GetIssueFor(Violation violation)
        {
            ICodeIssue handler = null;
            this.handlers.TryGetValue(violation.Rule.CheckId, out handler);
            return handler ?? this.nullHandler;
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class AggregatedIssueLocator : ICodeIssueLocator
    {
        private IEnumerable<ICodeIssueLocator> issueLocators;

        public AggregatedIssueLocator(IEnumerable<ICodeIssueLocator> issueLocators)
        {
            this.issueLocators = issueLocators;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            return this.issueLocators.SelectMany(locator => locator.GetCodeIssues(sourceCode, enumerate, violation, csElement));
        }
    }
}

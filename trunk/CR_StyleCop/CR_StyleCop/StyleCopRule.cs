namespace CR_StyleCop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal abstract class StyleCopRule : IStyleCopRule
    {
        private static readonly Func<CsElement, IEnumerable<CsToken>> elementTokens = element => element.ElementTokens;
        
        private static readonly Func<CsElement, IEnumerable<CsToken>> attributesTokens = element => element.Attributes != null 
            ? element.Attributes.SelectMany(attribute => attribute.ChildTokens) 
            : Enumerable.Empty<CsToken>();
        
        private readonly ICodeIssueLocator issueLocator;

        public StyleCopRule(ICodeIssueLocator issueLocator)
        {
            this.issueLocator = issueLocator;
        }

        public static Func<CsElement, IEnumerable<CsToken>> AttributesTokens
        {
            get { return attributesTokens; }
        }

        public static Func<CsElement, IEnumerable<CsToken>> ElementTokens
        {
            get { return elementTokens; }
        }

        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, ISourceCode sourceCode, Violation violation)
        {
            var message = string.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            var csElement = violation.Element as CsElement;
            if (csElement == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, sourceCode.LengthOfLine(violation.Line) + 1), message, violation, 10);
                return;
            }

            foreach (var styleCopCodeIssue in this.issueLocator.GetCodeIssues(sourceCode, filter => ea.GetEnumerable(ea.Scope, filter), violation, csElement))
            {
                ea.AddIssue(styleCopCodeIssue.IssueType, styleCopCodeIssue.Range, message, violation);
            }
        }
    }
}

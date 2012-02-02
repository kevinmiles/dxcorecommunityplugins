namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class MethodCallIssueLocator : ICodeIssueLocator
    {
        private readonly string methodName;
        private readonly Func<MethodCall, bool> qualifyParameters;

        public MethodCallIssueLocator(string methodName, Func<MethodCall, bool> qualifyParameters)
        {
            this.methodName = methodName;
            this.qualifyParameters = qualifyParameters;
        }

        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            foreach (var element in from x in enumerate(new ElementTypeFilter(LanguageElementType.MethodCall))
                                    where x.FirstNameRange.Start.Line == violation.Line && x.Name == this.methodName
                                    select x)
            {
                MethodCall methodCall = (MethodCall)element.ToLanguageElement();
                if (this.qualifyParameters(methodCall))
                {
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, methodCall.Range);
                }
            }
        }
    }
}

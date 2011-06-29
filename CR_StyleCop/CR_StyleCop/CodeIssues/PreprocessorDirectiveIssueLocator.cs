namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class PreprocessorDirectiveIssueLocator : ICodeIssueLocator
    {
        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode,
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
            Violation violation,
            CsElement csElement)
        {
            var preprocessorToken = (from token in csElement.ElementTokens
                         where token.LineNumber == violation.Line && token.CsTokenType == CsTokenType.PreprocessorDirective
                         select token).FirstOrDefault();
            if (preprocessorToken != null)
            {
                int underlineLength = 1;
                while (preprocessorToken.Text[underlineLength] == ' ' || preprocessorToken.Text[underlineLength] == '\t')
                {
                    underlineLength++;
                }

                while (underlineLength < preprocessorToken.Text.Length && preprocessorToken.Text[underlineLength] != ' ' && preprocessorToken.Text[underlineLength] != '\t')
                {
                    underlineLength++;
                }

                var startPoint = preprocessorToken.Location.StartPoint;
                var sourceRange = new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, startPoint.LineNumber, startPoint.IndexOnLine + 1 + underlineLength);
                yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, sourceRange);
            }
        }
    }
}

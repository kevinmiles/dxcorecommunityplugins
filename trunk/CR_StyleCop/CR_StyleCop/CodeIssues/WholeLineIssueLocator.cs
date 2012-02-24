namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class WholeLineIssueLocator : ICodeIssueLocator
    {
        public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
            ISourceCode sourceCode, 
            Func<ElementTypeFilter, IEnumerable<IElement>> enumerate, 
            Violation violation, 
            CsElement csElement)
        {
            CodePoint startPoint = null;
            CodePoint endPoint = null;
            foreach (var token in csElement.ElementTokens.Flatten().Where(x => x.LineNumber == violation.Line))
            {
                if (startPoint == null)
                {
                    startPoint = token.Location.StartPoint;
                    endPoint = token.Location.EndPoint;
                }
                
                if (token.CsTokenType != CsTokenType.WhiteSpace && token.CsTokenType != CsTokenType.EndOfLine)
                {
                    endPoint = token.Location.EndPoint;
                }
                else if (token.CsTokenType == CsTokenType.EndOfLine)
                {
                    yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(startPoint.LineNumber, startPoint.IndexOnLine + 1, endPoint.LineNumber, endPoint.IndexOnLine + 2));
                }
            }
        }
    }
}

namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;
    
    internal class SA1009_ClosingParenthesisMustBeSpacedCorrectly : StyleCopRule
    {
        private static Func<CsToken, bool> isBannedFollower = token =>
            token.CsTokenType == CsTokenType.CloseParenthesis
            || token.CsTokenType == CsTokenType.CloseSquareBracket
            || token.CsTokenType == CsTokenType.CloseAttributeBracket
            || token.CsTokenType == CsTokenType.Comma
            || token.CsTokenType == CsTokenType.OpenParenthesis
            || token.CsTokenType == CsTokenType.OpenSquareBracket
            || token.CsTokenType == CsTokenType.OpenAttributeBracket
            || token.CsTokenType == CsTokenType.Semicolon
            || token.CsTokenType == CsTokenType.OperatorSymbol && (token.Text == "." || token.Text == "->");

        public SA1009_ClosingParenthesisMustBeSpacedCorrectly()
            : base(new AggregatedIssueLocator(new ICodeIssueLocator[]
                {
                    new AllTokensByTypePrecededByBannedElementIssueLocator(ElementTokens, CsTokenType.CloseParenthesis, CsTokenType.WhiteSpace),
                    new AllTokensByTypePrecededByBannedElementIssueLocator(AttributesTokens, CsTokenType.CloseParenthesis, CsTokenType.WhiteSpace),
                    new AllTokensFollowedByWhitespaceAndBannedElementIssueLocator(ElementTokens, (token, violation) => token.CsTokenType == CsTokenType.CloseParenthesis, isBannedFollower),
                    new AllTokensFollowedByWhitespaceAndBannedElementIssueLocator(AttributesTokens, (token, violation) => token.CsTokenType == CsTokenType.CloseParenthesis, isBannedFollower),
                    new TypeCastFollowedByWhiteSpaceIssueLocator(ElementTokens),
                    new TypeCastFollowedByWhiteSpaceIssueLocator(AttributesTokens),
                }))
        {
        }

        internal class TypeCastFollowedByWhiteSpaceIssueLocator : ICodeIssueLocator
        {
            private readonly Func<CsElement, IEnumerable<CsToken>> getTokens;

            public TypeCastFollowedByWhiteSpaceIssueLocator(Func<CsElement, IEnumerable<CsToken>> getTokens)
            {
                this.getTokens = getTokens;
            }

            public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
                ISourceCode sourceCode,
                Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
                Violation violation, 
                CsElement csElement)
            {
                bool inTypeOfOrSizeOfOrDefault = false;
                bool openParenFound = false;
                bool typeFound = false;
                CsToken potentialViolation = null;
                foreach (var token in this.getTokens(csElement).Where(x => x.LineNumber == violation.Line))
                {
                    if (token.CsTokenType == CsTokenType.Typeof || token.CsTokenType == CsTokenType.Sizeof || token.CsTokenType == CsTokenType.DefaultValue)
                    {
                        inTypeOfOrSizeOfOrDefault = true;
                    }
                    else if (inTypeOfOrSizeOfOrDefault && token.CsTokenType == CsTokenType.CloseParenthesis)
                    {
                        inTypeOfOrSizeOfOrDefault = false;
                    }
                    else if (!inTypeOfOrSizeOfOrDefault && token.CsTokenType == CsTokenType.OpenParenthesis)
                    {
                        openParenFound = true;
                    }
                    else if (!inTypeOfOrSizeOfOrDefault && openParenFound && potentialViolation == null && token.CsTokenType == CsTokenType.WhiteSpace)
                    {
                    }
                    else if (!inTypeOfOrSizeOfOrDefault && openParenFound && potentialViolation == null && token is TypeToken)
                    {
                        typeFound = true;
                    }
                    else if (!inTypeOfOrSizeOfOrDefault && openParenFound && typeFound && potentialViolation == null && token.CsTokenType == CsTokenType.CloseParenthesis)
                    {
                        potentialViolation = token;
                    }
                    else if (!inTypeOfOrSizeOfOrDefault && potentialViolation != null && token.CsTokenType == CsTokenType.WhiteSpace)
                    {
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, new SourceRange(potentialViolation.Location.StartPoint.LineNumber, potentialViolation.Location.StartPoint.IndexOnLine + 1, potentialViolation.Location.EndPoint.LineNumber, potentialViolation.Location.EndPoint.IndexOnLine + 2));
                        openParenFound = false;
                        typeFound = false;
                        potentialViolation = null;
                    }
                    else
                    {
                        openParenFound = false;
                        typeFound = false;
                        potentialViolation = null;
                    }
                }
            }
        }
    }
}

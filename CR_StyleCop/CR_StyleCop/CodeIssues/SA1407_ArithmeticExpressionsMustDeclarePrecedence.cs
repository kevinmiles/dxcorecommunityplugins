namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1407_ArithmeticExpressionsMustDeclarePrecedence : StyleCopRule
    {
        public SA1407_ArithmeticExpressionsMustDeclarePrecedence()
            : base(new IssueLocator())
        {
        }

        private class IssueLocator : ICodeIssueLocator
        {
            private Dictionary<string, OperatorType> operators = new Dictionary<string, OperatorType>() 
            {
                {"+", OperatorType.Sum},
                {"-", OperatorType.Sum},
                {"*", OperatorType.Multiply},
                {"/", OperatorType.Multiply},
                {">>", OperatorType.Shift},
                {"<<", OperatorType.Shift},
                {"%", OperatorType.Modulo}
            };

            public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
                ISourceCode sourceCode,
                Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
                Violation violation,
                CsElement csElement)
            {
                foreach (BinaryOperatorExpression operation in from x in enumerate(new ElementTypeFilter(LanguageElementType.BinaryOperatorExpression))
                                                               let binaryOperation = (BinaryOperatorExpression)x.ToLanguageElement()
                                                               where binaryOperation.RecoveredRange.Start.Line == violation.Line
                                                                  && (binaryOperation.LeftSide.ElementType == LanguageElementType.BinaryOperatorExpression
                                                                      || binaryOperation.RightSide.ElementType == LanguageElementType.BinaryOperatorExpression)
                                                               select binaryOperation)
                {
                    var parentType = operators[operation.Name];
                    var leftChildType = operation.LeftSide.ElementType == LanguageElementType.BinaryOperatorExpression ? operators[operation.LeftSide.Name] : parentType;
                    var rightChildType = operation.RightSide.ElementType == LanguageElementType.BinaryOperatorExpression ? operators[operation.RightSide.Name] : parentType;
                    var isLeftModulo = operation.LeftSide.ElementType == LanguageElementType.BinaryOperatorExpression ? operation.LeftSide.Name == "%" : operation.Name == "%";
                    var isRightModulo = operation.RightSide.ElementType == LanguageElementType.BinaryOperatorExpression ? operation.RightSide.Name == "%" : operation.Name == "%";
                    if (rightChildType > parentType)
                    {
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, operation.RightSide.RecoveredRange);
                    }
                    else if (leftChildType > parentType || (leftChildType == rightChildType && isLeftModulo != isRightModulo))
                    {
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, operation.LeftSide.RecoveredRange);
                    }
                }
            }
        }

        private enum OperatorType
        {
            Shift = 0,
            Sum = 1,
            Multiply = 2,
            Modulo = 2
        }
    }
}

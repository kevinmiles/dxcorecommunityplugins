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

        internal class IssueLocator : ICodeIssueLocator
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
                IDocument document,
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
                    OperatorType parentType = operators[operation.Name];
                    OperatorType leftChildType = operation.LeftSide.ElementType == LanguageElementType.BinaryOperatorExpression ? operators[operation.LeftSide.Name] : parentType;
                    OperatorType rightChildType = operation.RightSide.ElementType == LanguageElementType.BinaryOperatorExpression ? operators[operation.RightSide.Name] : parentType;
                    if (leftChildType != parentType || rightChildType != parentType)
                    {
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, operation.RecoveredRange);
                    }
                }
            }
        }

        private enum OperatorType
        {
            Sum,
            Multiply,
            Shift,
            Modulo
        }
    }
}

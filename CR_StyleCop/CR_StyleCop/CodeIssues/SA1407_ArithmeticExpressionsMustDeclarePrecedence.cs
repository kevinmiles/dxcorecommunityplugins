namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop.CSharp;

    internal class SA1407_ArithmeticExpressionsMustDeclarePrecedence : ICodeIssue
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

        public void AddViolationIssue(DevExpress.CodeRush.Core.CheckCodeIssuesEventArgs ea, DevExpress.CodeRush.StructuralParser.IDocument document, Microsoft.StyleCop.Violation violation)
        {
            string message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            var csElement = violation.Element as CsElement;
            if (csElement == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }

            foreach (BinaryOperatorExpression operation in from x in ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.BinaryOperatorExpression))
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
                    ea.AddSmell(operation.RecoveredRange, message, 10);
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

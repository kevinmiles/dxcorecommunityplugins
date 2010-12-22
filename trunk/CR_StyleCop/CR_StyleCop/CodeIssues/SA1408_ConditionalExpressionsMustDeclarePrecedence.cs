namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;

    internal class SA1408_ConditionalExpressionsMustDeclarePrecedence : ICodeIssue
    {
        private Dictionary<string, OperatorType> operators = new Dictionary<string, OperatorType>() 
            {
                {"&&", OperatorType.And},
                {"||", OperatorType.Or}
            };

        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            string message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            var csElement = violation.Element as CsElement;
            if (csElement == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }

            foreach (LogicalOperation operation in from x in ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.LogicalOperation))
                                                   let logicalOperation = (LogicalOperation)x.ToLanguageElement()
                                                   where logicalOperation.RecoveredRange.Start.Line == violation.Line
                                                        && (logicalOperation.LeftSide.ElementType == LanguageElementType.LogicalOperation
                                                             || logicalOperation.RightSide.ElementType == LanguageElementType.LogicalOperation)
                                                   select logicalOperation)
            {
                OperatorType parentType = operators[operation.Name];
                OperatorType leftChildType = operation.LeftSide.ElementType == LanguageElementType.LogicalOperation ? operators[operation.LeftSide.Name] : parentType;
                OperatorType rightChildType = operation.RightSide.ElementType == LanguageElementType.LogicalOperation ? operators[operation.RightSide.Name] : parentType;
                if (leftChildType != parentType || rightChildType != parentType)
                {
                    ea.AddSmell(operation.RecoveredRange, message, 10);
                }
            }
        }

        private enum OperatorType
        {
            Or,
            And
        }
    }
}

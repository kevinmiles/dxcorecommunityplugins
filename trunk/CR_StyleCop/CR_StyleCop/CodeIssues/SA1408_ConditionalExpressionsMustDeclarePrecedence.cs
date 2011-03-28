namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CR_StyleCop;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1408_ConditionalExpressionsMustDeclarePrecedence : StyleCopRule
    {
        public SA1408_ConditionalExpressionsMustDeclarePrecedence()
            : base(new IssueLocator())
        {
        }

        internal class IssueLocator : ICodeIssueLocator
        {
            private Dictionary<string, OperatorType> operators = new Dictionary<string, OperatorType>() 
            {
                {"&&", OperatorType.And},
                {"||", OperatorType.Or}
            };

            public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
                IDocument document,
                Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
                Violation violation,
                CsElement csElement)
            {
                foreach (LogicalOperation operation in from x in enumerate(new ElementTypeFilter(LanguageElementType.LogicalOperation))
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
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, operation.RecoveredRange);
                    }
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

namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using StyleCop;
    using StyleCop.CSharp;

    internal class SA1119_StatementMustNotUseUnnecessaryParenthesis : StyleCopRule
    {
        public SA1119_StatementMustNotUseUnnecessaryParenthesis()
            : base(new IssueLocator())
        {
        }

        internal class IssueLocator : ICodeIssueLocator
        {
            private List<LanguageElementType> validParenthesizedContent = new List<LanguageElementType>()
            {
                LanguageElementType.TypeCheck,
                LanguageElementType.ConditionalTypeCast,
                LanguageElementType.TypeCastExpression,
                LanguageElementType.NullCoalescingExpression,
                LanguageElementType.LogicalInversion,
                LanguageElementType.LogicalOperation,
                LanguageElementType.ConditionalExpression,
                LanguageElementType.AssignmentExpression,
                LanguageElementType.BinaryOperatorExpression,
                LanguageElementType.UnaryDecrement,
                LanguageElementType.UnaryIncrement,
                LanguageElementType.UnaryOperatorExpression,
                LanguageElementType.ObjectCreationExpression,
                LanguageElementType.ArrayCreateExpression,
                LanguageElementType.LambdaExpression,
                LanguageElementType.QueryExpression,
                LanguageElementType.RelationalOperation
            };

            private List<LanguageElementType> invalidParenthesizedParent = new List<LanguageElementType>()
            {
                LanguageElementType.Assignment,
                LanguageElementType.Block,
                LanguageElementType.Break,
                LanguageElementType.Case,
                LanguageElementType.CaseClause,
                LanguageElementType.CaseClausesList,
                LanguageElementType.Catch,
                LanguageElementType.Checked,
                LanguageElementType.CheckedExpression,
                LanguageElementType.ConstructorInitializer,
                LanguageElementType.Continue,
                LanguageElementType.Do,
                LanguageElementType.DoStatement,
                LanguageElementType.Else,
                LanguageElementType.EmptyStatement,
                LanguageElementType.Finally,
                LanguageElementType.Fixed,
                LanguageElementType.For,
                LanguageElementType.ForEach,
                LanguageElementType.ForEachExpression,
                LanguageElementType.ForExpression,
                LanguageElementType.Goto,
                LanguageElementType.If,
                LanguageElementType.IfExpression,
                LanguageElementType.ImplicitVariable,
                LanguageElementType.InitializedVariable,
                LanguageElementType.Label,
                LanguageElementType.Lock,
                LanguageElementType.Return,
                LanguageElementType.Switch,
                LanguageElementType.Throw,
                LanguageElementType.Try,
                LanguageElementType.Unchecked,
                LanguageElementType.UncheckedExpression,
                LanguageElementType.UnsafeStatement,
                LanguageElementType.UsingStatement,
                LanguageElementType.While,
                LanguageElementType.WhileExpression,
                LanguageElementType.YieldBreak,
                LanguageElementType.YieldComputation,
                LanguageElementType.YieldResult,
                LanguageElementType.YieldReturn
            };

            public IEnumerable<StyleCopCodeIssue> GetCodeIssues(
                ISourceCode sourceCode,
                Func<ElementTypeFilter, IEnumerable<IElement>> enumerate,
                Violation violation,
                CsElement csElement)
            {
                foreach (IElement element in from x in enumerate(new ElementTypeFilter(LanguageElementType.ParenthesizedExpression))
                                             where x.FirstNameRange.Start.Line == violation.Line
                                             select x)
                {
                    if (!this.validParenthesizedContent.Contains(element.Children[0].ElementType)
                        || this.invalidParenthesizedParent.Contains(element.Parent.ElementType))
                    {
                        yield return new StyleCopCodeIssue(CodeIssueType.CodeSmell, element.FirstNameRange);
                    }
                }
            }
        }
    }
}

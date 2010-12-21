namespace CR_StyleCop.CodeIssues
{
    using System;
    using System.Linq;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.StructuralParser;
    using DevExpress.CodeRush.Diagnostics.General;
    using Microsoft.StyleCop;
    using Microsoft.StyleCop.CSharp;
    using System.Collections.Generic;

    internal class SA1119_StatementMustNotUseUnnecessaryParenthesis : ICodeIssue
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

        public void AddViolationIssue(CheckCodeIssuesEventArgs ea, IDocument document, Violation violation)
        {
            string message = String.Format("{0}: {1}", violation.Rule.CheckId, violation.Message);
            var csElement = violation.Element as CsElement;
            if (csElement == null)
            {
                ea.AddSmell(new SourceRange(violation.Line, 1, violation.Line, document.LengthOfLine(violation.Line) + 1), message, 10);
                return;
            }

            foreach (IElement element in ea.GetEnumerable(ea.Scope, new ElementTypeFilter(LanguageElementType.ParenthesizedExpression)).Where(x => x.FirstNameRange.Start.Line == violation.Line))
            {
                if (!this.validParenthesizedContent.Contains(element.Children[0].ElementType)
                    || this.invalidParenthesizedParent.Contains(element.Parent.ElementType))
                {
                    ea.AddSmell(element.FirstNameRange, message, 10);
                }
            }
        }
    }
}

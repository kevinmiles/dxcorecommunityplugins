namespace CR_StackOverflowIssues
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using DevExpress.CodeRush.Core;
    using DevExpress.CodeRush.PlugInCore;
    using DevExpress.CodeRush.StructuralParser;

    public partial class StackOverflowIssuesPlugin : StandardPlugIn
    {
        private const string StackOverflowExceptionInRuntimeIssueText = "StackOverflowException in runtime";

        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
            this.changeToBaseCallRefactoringProvider.CodeIssueMessage = StackOverflowExceptionInRuntimeIssueText;
            this.changeToFieldCallRefactoringProvider.CodeIssueMessage = StackOverflowExceptionInRuntimeIssueText;
            //
            // TODO: Add your initialization code here.
            //
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }
        #endregion

        private static bool ExpressionReferencesProperty(ElementReferenceExpression expression, Property property)
        {
            return expression != null
                && property != null
                && expression.Name == property.Name;
        }

        private static bool InstanceContextIsMet(ElementReferenceExpression expression, Property property)
        {
            return !property.IsStatic
                && (expression.LastChild == null || expression.LastChild is ThisReferenceExpression);
        }

        private static bool StaticContextIsMet(ElementReferenceExpression expression, Property property)
        {
            return property.IsStatic
                && (expression.LastChild == null || expression.LastChild.IsRelatedTo(property.GetParentClassInterfaceOrStruct()));
        }

        private static bool ExpressionReferencesParentProperty(ElementReferenceExpression expression)
        {
            return expression != null && expression.InsideProperty
                && ExpressionReferencesProperty(expression, expression.GetProperty());
        }

        private static bool ExpressionReferencesParentPropertyInInstanceContext(ElementReferenceExpression expression)
        {
            return expression != null && expression.InsideProperty
                && ExpressionReferencesPropertyInInstanceContext(expression, expression.GetProperty());
        }

        private static bool ExpressionReferencesParentPropertyInStaticContext(ElementReferenceExpression expression)
        {
            return expression != null && expression.InsideProperty
                && ExpressionReferencesPropertyInStaticContext(expression, expression.GetProperty());
        }

        private bool ExpressionReferencesParentPropertyInInstanceOrStaticContext(ElementReferenceExpression expression)
        {
            return expression != null && expression.InsideProperty
                && ExpressionReferencesPropertyInInstanceOrStaticContext(expression, expression.GetProperty());
        }

        private static bool ExpressionReferencesPropertyInInstanceOrStaticContext(ElementReferenceExpression expression, Property property)
        {
            return ExpressionReferencesProperty(expression, property)
                && (InstanceContextIsMet(expression, property)
                    || StaticContextIsMet(expression, property));
        }

        private static bool ExpressionReferencesPropertyInInstanceContext(ElementReferenceExpression expression, Property property)
        {
            return ExpressionReferencesProperty(expression, property)
                && InstanceContextIsMet(expression, property);
        }

        private static bool ExpressionReferencesPropertyInStaticContext(ElementReferenceExpression expression, Property property)
        {
            return ExpressionReferencesProperty(expression, property)
                && StaticContextIsMet(expression, property);
        }

        private static void CheckIssuesForPropertyReturn(CheckCodeIssuesEventArgs ea, Property property, LanguageElement languageElement)
        {
            var ret = languageElement as Return;
            if (ret != null && ExpressionReferencesPropertyInInstanceOrStaticContext(ret.Expression as ElementReferenceExpression, property))
            {
                ea.AddError(ret.Range, StackOverflowExceptionInRuntimeIssueText);
            }
            foreach (LanguageElement child in languageElement.Nodes)
            {
                CheckIssuesForPropertyReturn(ea, property, child);
            }
        }

        private static void CheckIssuesForPropertyAssignment(CheckCodeIssuesEventArgs ea, Property property, LanguageElement languageElement)
        {
            var assignment = languageElement as Assignment;
            if (assignment != null && ExpressionReferencesPropertyInInstanceOrStaticContext(assignment.LeftSide as ElementReferenceExpression, property))
            {
                ea.AddError(assignment.Range, StackOverflowExceptionInRuntimeIssueText);
            }
            foreach (LanguageElement child in languageElement.Nodes)
            {
                CheckIssuesForPropertyAssignment(ea, property, child);
            }
        }

        private void StackOverflowInGetterIssueProvider_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
        {
            if (ea.IsSuppressed(ea.Scope))
                return;

            foreach (IElement element in ea.GetEnumerable(ea.Scope, ElementFilters.Property))
            {
                var propertyElement = element as IPropertyElement;
                if (propertyElement == null)
                    continue;

                Property property = (Property)propertyElement.ToLanguageElement();
                if (property.Getter != null)
                {
                    foreach (LanguageElement getterElement in property.Getter.Nodes)
                    {
                        CheckIssuesForPropertyReturn(ea, property, getterElement);
                    }
                }
                if (property.Setter != null)
                {
                    foreach (LanguageElement setterElement in property.Setter.Nodes)
                    {
                        CheckIssuesForPropertyAssignment(ea, property, setterElement);
                    }
                }
            }
        }

        private static LanguageElement GetCodeElementToReplace(LanguageElement element, Property property)
        {
            if (property.HasGetter && element.Inside(LanguageElementType.PropertyAccessorGet))
            {
                return GetElement<Return>(element);
            }
            if (property.HasSetter && element.Inside(LanguageElementType.PropertyAccessorSet))
            {
                return GetElement<Assignment>(element);
            }
            return element;
        }

        private static string GetChangeToBaseCallCode(ElementBuilder elementBuilder, LanguageElement toReplace, Property property)
        {
            Return oldReturn = toReplace as Return;
            if (oldReturn != null)
            {
                Return newReturn = elementBuilder.AddReturn(null, property.Name);
                newReturn.Expression.AddNode(elementBuilder.BuildBaseReferenceExpression());
            }
            Assignment oldAssignment = toReplace as Assignment;
            if (oldAssignment != null)
            {
                Assignment newAssignment = elementBuilder.AddAssignment(null, property.Name, oldAssignment.Expression);
                newAssignment.LeftSide.AddNode(elementBuilder.BuildBaseReferenceExpression());
            }
            return elementBuilder.GenerateCode().Trim();
        }

        private string GetChangeToFieldCallCode(ElementBuilder elementBuilder, LanguageElement toReplace, Property property)
        {
            string fieldVariableName = CodeRush.Strings.Get("FormatFieldName", property.Name);

            Return oldReturn = toReplace as Return;
            if (oldReturn != null)
            {
                Return newReturn = elementBuilder.AddReturn(null, fieldVariableName);
                if (oldReturn.Expression.LastChild is ThisReferenceExpression)
                {
                    newReturn.Expression.AddNode(elementBuilder.BuildThisReferenceExpression());
                }
            }
            Assignment oldAssignment = toReplace as Assignment;
            if (oldAssignment != null)
            {
                Assignment newAssignment = elementBuilder.AddAssignment(null, fieldVariableName, oldAssignment.Expression);
                if (oldAssignment.LeftSide.LastChild is ThisReferenceExpression)
                {
                    newAssignment.LeftSide.AddNode(elementBuilder.BuildThisReferenceExpression());
                }
            }
            return elementBuilder.GenerateCode().Trim();
        }

        private static TElement GetElement<TElement>(LanguageElement languageElement)
            where TElement : LanguageElement
        {
            TElement wantedElement = languageElement as TElement;
            while (wantedElement == null && languageElement.Parent != null)
            {
                languageElement = languageElement.Parent;
                wantedElement = languageElement as TElement;
            }
            return wantedElement;
        }

        private void ChangeToBaseCallRefactoringProvider_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            var @return = GetElement<Return>(ea.Element);
            bool propertyReturn = @return != null && ExpressionReferencesParentPropertyInInstanceContext(@return.Expression as ElementReferenceExpression);
            var assignment = GetElement<Assignment>(ea.Element);
            bool propertyAssignment = assignment != null && ExpressionReferencesParentPropertyInInstanceContext(assignment.LeftSide as ElementReferenceExpression);
            ea.Available = propertyReturn || propertyAssignment;
        }

        private void ChangeToBaseCallRefactoringProvider_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
        {
            var property = ea.Element.GetProperty();
            var toReplace = GetCodeElementToReplace(ea.Element, property);
            var elementBuilder = ea.NewElementBuilder();
            string generatedCode = GetChangeToBaseCallCode(elementBuilder, toReplace, property);

            ea.AddCodePreview(toReplace.Range.Start, generatedCode);
            ea.AddStrikethrough(toReplace.Range);
        }

        private void ChangeToBaseCallRefactoringProvider_Apply(object sender, ApplyContentEventArgs ea)
        {
            var property = ea.Element.GetProperty();
            var toReplace = GetCodeElementToReplace(ea.Element, property);
            var elementBuilder = ea.NewElementBuilder();
            string generatedCode = GetChangeToBaseCallCode(elementBuilder, toReplace, property);
        
            ea.TextDocument.Replace(toReplace.Range, generatedCode, "Change to 'base' call", true);
        }

        private void ChangeToFieldCallRefactoringProvider_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            var @return = GetElement<Return>(ea.Element);
            bool propertyReturn = @return != null && ExpressionReferencesParentPropertyInInstanceOrStaticContext(@return.Expression as ElementReferenceExpression);
            var assignment = GetElement<Assignment>(ea.Element);
            bool propertyAssignment = assignment != null && ExpressionReferencesParentPropertyInInstanceOrStaticContext(assignment.LeftSide as ElementReferenceExpression);
            ea.Available = propertyReturn || propertyAssignment;
        }

        private void ChangeToFieldCallRefactoringProvider_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
        {
            var property = ea.Element.GetProperty();
            var toReplace = GetCodeElementToReplace(ea.Element, property);
            var elementBuilder = ea.NewElementBuilder();
            string generatedCode = GetChangeToFieldCallCode(elementBuilder, toReplace, property);

            ea.AddCodePreview(toReplace.Range.Start, generatedCode);
            ea.AddStrikethrough(toReplace.Range);
        }

        private void ChangeToFieldCallRefactoringProvider_Apply(object sender, ApplyContentEventArgs ea)
        {
            var property = ea.Element.GetProperty();
            var toReplace = GetCodeElementToReplace(ea.Element, property);
            var elementBuilder = ea.NewElementBuilder();
            string generatedCode = GetChangeToFieldCallCode(elementBuilder, toReplace, property);

            ea.TextDocument.Replace(toReplace.Range, generatedCode, "Change to field call", true);
        }
    }
}
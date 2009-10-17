using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_StackOverflowIssues
{
    public partial class StackOverflowIssuesPlugin : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

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
                ea.AddError(ret.Range, "StackOverflowException in runtime");
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
                ea.AddError(assignment.Range, "StackOverflowException in runtime");
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

        private void ChangeToBaseCallRefactoringProvider_Apply(object sender, ApplyContentEventArgs ea)
        {
            ElementBuilder elementBuilder = ea.NewElementBuilder();
            Property property = ea.Element.GetProperty();
            LanguageElement toReplace = ea.Element;
            if (property.HasGetter && ea.Element.Inside(LanguageElementType.PropertyAccessorGet))
            {
                Return oldReturn = GetReturnElement(ea.Element);
            	Return newReturn = elementBuilder.AddReturn(null, property.Name);
                newReturn.Expression.AddNode(elementBuilder.BuildBaseReferenceExpression());
                toReplace = oldReturn;
            }
            if (property.HasSetter && ea.Element.Inside(LanguageElementType.PropertyAccessorSet))
            {
                Assignment oldAssignment = GetAssignmentElement(ea.Element);
                Assignment newAssignment = elementBuilder.AddAssignment(null, property.Name, oldAssignment.Expression);
                newAssignment.LeftSide.AddNode(elementBuilder.BuildBaseReferenceExpression());
                toReplace = oldAssignment;
            }
            string generatedCode = elementBuilder.GenerateCode();
            ea.TextDocument.Replace(toReplace.Range, generatedCode, "Change to 'base' call", true);
        }

        private static Return GetReturnElement(LanguageElement languageElement)
        {
            Return @return = languageElement as Return;
            while (@return == null && languageElement.Parent != null)
            {
                languageElement = languageElement.Parent;
                @return = languageElement as Return;
            }
            return @return;
        }

        private static Assignment GetAssignmentElement(LanguageElement languageElement)
        {
            Assignment assignment = languageElement as Assignment;
            while (assignment == null && languageElement.Parent != null)
            {
                languageElement = languageElement.Parent;
                assignment = languageElement as Assignment;
            }
            return assignment;
        }

        private void ChangeToBaseCallRefactoringProvider_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            var @return = GetReturnElement(ea.Element);
            bool propertyReturn = @return != null && ExpressionReferencesParentPropertyInInstanceContext(@return.Expression as ElementReferenceExpression);
            var assignment = GetAssignmentElement(ea.Element);
            bool propertyAssignment = assignment != null && ExpressionReferencesParentPropertyInInstanceContext(assignment.LeftSide as ElementReferenceExpression);
            ea.Available = propertyReturn || propertyAssignment;
        }
    }
}
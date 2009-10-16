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
                && expression.Name == property.Name
                && (expression.LastChild == null 
                    || (!property.IsStatic && expression.LastChild is ThisReferenceExpression)
                    || (property.IsStatic && expression.LastChild.IsRelatedTo(property.GetParentClassInterfaceOrStruct())));
        }

        private static void CheckForPropertyReturn(CheckCodeIssuesEventArgs ea, Property property, LanguageElement languageElement)
        {
            var ret = languageElement as Return;
            if (ret != null && ExpressionReferencesProperty(ret.Expression as ElementReferenceExpression, property))
            {
                ea.AddError(ret.Range, "StackOverflowException in runtime");
            }
            foreach (LanguageElement child in languageElement.Nodes)
            {
                CheckForPropertyReturn(ea, property, child);
            }
        }

        private static void CheckForPropertyAssignment(CheckCodeIssuesEventArgs ea, Property property, LanguageElement languageElement)
        {
            var assignment = languageElement as Assignment;
            if (assignment != null && ExpressionReferencesProperty(assignment.LeftSide as ElementReferenceExpression, property))
            {
                ea.AddError(assignment.Range, "StackOverflowException in runtime");
            }
            foreach (LanguageElement child in languageElement.Nodes)
            {
                CheckForPropertyAssignment(ea, property, child);
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
                        CheckForPropertyReturn(ea, property, getterElement);
                    }
                }
                if (property.Setter != null)
                {
                    foreach (LanguageElement setterElement in property.Setter.Nodes)
                    {
                        CheckForPropertyAssignment(ea, property, setterElement);
                    }
                }
            }
        }
    }
}
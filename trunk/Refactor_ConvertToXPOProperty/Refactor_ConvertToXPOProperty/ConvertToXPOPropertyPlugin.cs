// <copyright file="ConvertToXPOPropertyPlugin.cs" company="VirgoTech Krzysztof Blacha">
// Project: Refactor_ConvertToXPOProperty
// File: ConvertToXPOPropertyPlugin.cs (4,90 KB, 139 lines)
// Creation date: 2010-07-30 14:10
// Last modified: 2010-07-31 18:33
// </copyright>

#region Using directives
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
#endregion

namespace Refactor_ConvertToXPOProperty
{
    /// <summary>
    /// Convert to XPO property plugin
    /// </summary>
    public partial class ConvertToXPOPropertyPlugin : StandardPlugIn
    {
        // DXCore-generated code...
        #region InitializePlugIn
        /// <summary>
        /// Initialize plug in
        /// </summary>
        public override void InitializePlugIn()
        {
            base.InitializePlugIn();

            //
            // TODO: Add your initialization code here.
            //
        } // InitializePlugIn
        #endregion
        #region FinalizePlugIn
        /// <summary>
        /// Finalize plug in
        /// </summary>
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        } // FinalizePlugIn
        #endregion

        /// <summary>
        /// _Property
        /// </summary>
        private Property _Property;

        /// <summary>
        /// Refactoring provider check availability
        /// </summary>
        private void refactoringProvider_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            LanguageElement element = ea.Element;
            if (element == null)
                return;

            if (element.InsideClass && element is Property)
            {
                _Property = (Property)element;
                if (_Property.IsAutoImplemented)
                    ea.Available = true;
            }
        } // refactoringProvider_CheckAvailability

        /// <summary>
        /// Refactoring provider apply
        /// </summary>
        private void refactoringProvider_Apply(object sender, ApplyContentEventArgs ea)
        {
            if (_Property != null)
            {
                ElementBuilder elementBuilder = ea.NewElementBuilder();
                string newCode = GetNewPropertyDeclaration(elementBuilder, _Property);
                ea.TextDocument.Replace(_Property.Range, newCode, "Converting into Xpo property", true);
            }
        } // refactoringProvider_Apply

        /// <summary>
        /// Get new property declaration
        /// </summary>
        private string GetNewPropertyDeclaration(ElementBuilder elementBuilder, Property oldProperty)
        {
            string propName = oldProperty.Name;
            string typeName = oldProperty.GetTypeName();
            string fieldVariableName = CodeRush.Strings.Get("FormatFieldName", propName);

            Variable fieldVar = elementBuilder.AddVariable(null, typeName, fieldVariableName);
            fieldVar.IsStatic = oldProperty.IsStatic;
            fieldVar.Visibility = MemberVisibility.Private;

            Property newProperty = elementBuilder.AddProperty(null, typeName, propName);
            newProperty.Visibility = oldProperty.Visibility;
            newProperty.IsStatic = oldProperty.IsStatic;
            newProperty.IsVirtual = oldProperty.IsVirtual;
            newProperty.IsOverride = oldProperty.IsOverride;
            newProperty.IsExplicitInterfaceMember = oldProperty.IsExplicitInterfaceMember;
            
            if (oldProperty.HasGetter)
            {
                Get getter = elementBuilder.AddGetter(newProperty);
                elementBuilder.AddReturn(getter, fieldVariableName);
            }

            if (oldProperty.HasSetter)
            {
                Set setter = elementBuilder.AddSetter(newProperty);
                ExpressionCollection expressionCollection = new ExpressionCollection();
                expressionCollection.Add(new PrimitiveExpression(String.Format("\"{0}\"", propName)));
                expressionCollection.Add(new ArgumentDirectionExpression(ArgumentDirection.Ref,
                    new ElementReferenceExpression(fieldVariableName)));
                expressionCollection.Add(new ElementReferenceExpression("value"));
                elementBuilder.AddMethodCall(setter, "SetPropertyValue", expressionCollection, null);
            }

            return elementBuilder.GenerateCode();
        } // GetNewPropertyDeclaration

        /// <summary>
        /// Refactoring provider prepare preview
        /// </summary>
        private void refactoringProvider_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
        {
            if (_Property != null)
            {
                ElementBuilder elementBuilder = ea.NewElementBuilder();
                string newCode = GetNewPropertyDeclaration(elementBuilder, _Property);

                ea.AddCodePreview(_Property.Range.Start, newCode.Trim());
                ea.AddStrikethrough(_Property.Range);
            }
        } // refactoringProvider_PreparePreview
    } // class ConvertToXPOPropertyPlugin
} // namespace Refactor_ConvertToXPOProperty

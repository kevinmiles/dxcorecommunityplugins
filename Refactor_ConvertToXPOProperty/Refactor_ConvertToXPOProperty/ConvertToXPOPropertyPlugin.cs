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
using System.Diagnostics;
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
        } // InitializePlugIn()
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
        } // FinalizePlugIn()
        #endregion

        private const string MenuItem_ConvertTo_XPOProperty = "ConvertToXPOProperty";
        private const string MenuItem_ConvertTo_DelayedProperty = "ConvertToDelayedProperty";
        private const string PersistentAttribute = "DevExpress.Xpo.PersistentAttribute";
        private const string PersistentAliasAttribute = "DevExpress.Xpo.PersistentAliasAttribute";
        private const string NonPersistentAttribute = "DevExpress.Xpo.NonPersistentAttribute";

        /// <summary>
        /// _Property
        /// </summary>
        private Property _Property;

        /// <summary>
        /// _New source tree resolver
        /// </summary>
        private SourceTreeResolver _NewSourceTreeResolver;

        /// <summary>
        /// _Is xpo property
        /// </summary>
        private bool _IsXpoProperty;

        /// <summary>
        /// _Is auto implemented
        /// </summary>
        private bool _IsAutoImplemented;

        /// <summary>
        /// Is element of type
        /// </summary>
        public bool IsElementOfType(ITypeElement DeclaredTypeElement, string type)
        {
            if (DeclaredTypeElement == null)
                return false;
            if (DeclaredTypeElement.FullName.Replace("`1", "") == type || DeclaredTypeElement.DescendsFrom(type))
                return true;
            return false;
        } // IsElementOfType(DeclaredTypeElement, type)

        /// <summary>
        /// Is element of type
        /// </summary>
        public bool IsElementOfType(ITypeReferenceExpression declaredTypeExpression, string type)
        {
            try
            {
                if (declaredTypeExpression == null)
                    return false;
                if (_NewSourceTreeResolver == null)
                    _NewSourceTreeResolver = new SourceTreeResolver();
                ITypeElement DeclaredTypeElement = _NewSourceTreeResolver.ResolveExpression(declaredTypeExpression) as ITypeElement;
                return IsElementOfType(DeclaredTypeElement, type);
            } // try
            catch (Exception ex)
            {
                Trace.WriteLine("Exception in IsElementOfType Function: " + ex.Message);
            }
            return false;
        } // IsElementOfType(declaredTypeExpression, type)

        /// <summary>
        /// Is xpo property
        /// </summary>
        private bool IsXpoProperty(Property property)
        {
            // Don;t know if this is a valid assumption but I do not have
            // better right now.
            // Some ideas borrowed from EasyFields plugin - Thank you
            IHasAttributes hasAttrs = property as IHasAttributes;
            if (hasAttrs != null)
            {
                foreach (IAttributeElement attr in hasAttrs.Attributes)
                {
            	    ITypeElement attrTypeElement = attr.GetDeclaration() as ITypeElement;
                    if (attrTypeElement != null)
                    {
                        if (IsElementOfType(attrTypeElement, PersistentAttribute) || IsElementOfType(attrTypeElement, PersistentAliasAttribute))
                            return true;
                        if (IsElementOfType(attrTypeElement, NonPersistentAttribute))
                            return false;
                    }
                } // foreach
            } // if
            if (property.Visibility == MemberVisibility.Public)
                return true;
            return false;
        } // IsXpoProperty(property)

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
                _IsXpoProperty = IsXpoProperty(_Property);
                _IsAutoImplemented = _Property.IsAutoImplemented;
                if (_IsAutoImplemented || _IsXpoProperty)
                {
                    ea.Available = true;
                    if (_IsAutoImplemented)
                        ea.AddSubMenuItem(MenuItem_ConvertTo_XPOProperty, "XPO property");
                    ea.AddSubMenuItem(MenuItem_ConvertTo_DelayedProperty, "Delayed property");
                }
            } // if
        } // refactoringProvider_CheckAvailability(sender, ea)

        /// <summary>
        /// Refactoring provider apply
        /// </summary>
        private void refactoringProvider_Apply(object sender, ApplyContentEventArgs ea)
        {
            if (_Property != null && ea.SelectedSubMenuItem != null)
            {
                ElementBuilder elementBuilder = ea.NewElementBuilder();
                string newCode;
                switch (ea.SelectedSubMenuItem.Name)
                {
                    case MenuItem_ConvertTo_XPOProperty:
                        newCode = GetNewPropertyDeclaration(elementBuilder, _Property);
                        ea.TextDocument.Replace(_Property.Range, newCode, "Converting into XPO property", true);
                        break;
                    case MenuItem_ConvertTo_DelayedProperty:
                        newCode = GetNewDelayedPropertyDeclaration(elementBuilder, _Property);
                        ea.TextDocument.Replace(_Property.Range, newCode, "Converting into delayed property", true);
                        // How detect and remove property variable?
                        //if (_IsXpoProperty && _Property.HasGetter)
                        //    ea.TextDocument.Replace(_Property.Getter.LastChild.FirstDetail.Range, string.Empty, "Converting into delayed property - field var removed", true);
                        break;
                } // switch
            } // if
        } // refactoringProvider_Apply(sender, ea)

        /// <summary>
        /// Get new delayed property declaration
        /// </summary>
        private string GetNewDelayedPropertyDeclaration(ElementBuilder elementBuilder, Property oldProperty)
        {
            string propName = oldProperty.Name;
            string typeName = oldProperty.GetTypeName();

            Property newProperty = elementBuilder.AddProperty(null, typeName, propName);
            newProperty.Visibility = oldProperty.Visibility;
            newProperty.IsStatic = oldProperty.IsStatic;
            newProperty.IsVirtual = oldProperty.IsVirtual;
            newProperty.IsOverride = oldProperty.IsOverride;
            newProperty.IsExplicitInterfaceMember = oldProperty.IsExplicitInterfaceMember;

            AttributeSection attrSection = elementBuilder.AddAttributeSection(newProperty);
            elementBuilder.AddAttribute(attrSection, "Delayed");

            if (oldProperty.HasGetter)
            {
                Get getter = elementBuilder.AddGetter(newProperty);
                ExpressionCollection expressionCollection = new ExpressionCollection();
                expressionCollection.Add(new PrimitiveExpression(String.Format("\"{0}\"", propName)));
                string methodName = String.Format("GetDelayedPropertyValue<{0}>", typeName);
                elementBuilder.AddReturn(getter, elementBuilder.BuildMethodCall(methodName, expressionCollection, null));
            }

            if (oldProperty.HasSetter)
            {
                Set setter = elementBuilder.AddSetter(newProperty);
                ExpressionCollection expressionCollection = new ExpressionCollection();
                expressionCollection.Add(new PrimitiveExpression(String.Format("\"{0}\"", propName)));
                expressionCollection.Add(new ElementReferenceExpression("value"));
                string methodName = String.Format("SetDelayedPropertyValue<{0}>", typeName);
                elementBuilder.AddMethodCall(setter, methodName, expressionCollection, null);
            } // if

            return elementBuilder.GenerateCode();
        } // GetNewDelayedPropertyDeclaration(elementBuilder, oldProperty)

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
            } // if

            return elementBuilder.GenerateCode();
        } // GetNewPropertyDeclaration(elementBuilder, oldProperty)

        /// <summary>
        /// Refactoring provider prepare preview
        /// </summary>
        private void refactoringProvider_PreparePreview(object sender, PrepareContentPreviewEventArgs ea)
        {
            if (_Property != null && ea.SelectedSubMenuItem != null)
            {
                ElementBuilder elementBuilder = ea.NewElementBuilder();
                string newCode = string.Empty;
                switch (ea.SelectedSubMenuItem.Name)
                {
                    case MenuItem_ConvertTo_XPOProperty:
                        newCode = GetNewPropertyDeclaration(elementBuilder, _Property);
                        break;
                    case MenuItem_ConvertTo_DelayedProperty:
                        newCode = GetNewDelayedPropertyDeclaration(elementBuilder, _Property);
                        break;
                } // switch

                if (!string.IsNullOrEmpty(newCode))
                {
                    ea.AddCodePreview(_Property.Range.Start, newCode.Trim());
                    ea.AddStrikethrough(_Property.Range);

                    // How remove field variable used by that property?
                    //if (_IsXpoProperty && _Property.HasGetter)
                    //    ea.AddStrikethrough(_Property.Getter.LastChild.FirstDetail.Range);
                }
            } // if
        } // refactoringProvider_PreparePreview(sender, ea)
    } // class ConvertToXPOPropertyPlugin
} // namespace Refactor_ConvertToXPOProperty

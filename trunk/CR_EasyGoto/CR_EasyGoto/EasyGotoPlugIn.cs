// <copyright file="EasyGotoPlugIn.cs" company="VirgoTech Krzysztof Blacha">
// Project: CR_EasyGoto
// File: EasyGotoPlugIn.cs (5,12 KB, 140 lines)
// Creation date: 2010-07-31 19:36
// Last modified: 2010-07-31 19:39
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
using DevExpress.Refactor.Core;
#endregion

namespace CR_EasyGoto
{
    /// <summary>
    /// Easy goto plug in
    /// </summary>
    public partial class EasyGotoPlugIn : StandardPlugIn
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

        private const string MenuItem_ClassDefinition = "ClassDefinition";
        private const string MenuItem_ClassDefinition_InheritedClass = "ClassDefinitionInheritedClass";
        private const string MenuItem_ClassDefinition_InheritedClass_Definition = "ClassDefinitionInheritedClassDefinition";

        /// <summary>
        /// _element class
        /// </summary>
        private Class _currentClass;
        /// <summary>
        /// _element base class
        /// </summary>
        private TypeReferenceExpression _currentBaseClassRef;

        /// <summary>
        /// Is class
        /// </summary>
        private static bool IsClass(LanguageElement element)
        {
            IElement declaration = element.GetDeclaration();
            return (declaration as IClassElement) != null;
        } // IsClass

        /// <summary>
        /// Get class base type
        /// </summary>
        private static TypeReferenceExpression GetClassBaseType(LanguageElement classElement)
        {
            LanguageElement current = classElement.FirstDetail;
            while (current != null && !IsClass(current))
                current = current.NextSibling;
            return (TypeReferenceExpression)current;
        } // GetClassBaseType

        /// <summary>
        /// Nav easy goto check availability
        /// </summary>
        private void navEasyGoto_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            LanguageElement element = ea.Element;
            if (element == null)
                return;
            _currentClass = element.GetClass();
            if (_currentClass != null)
            {
                ea.Available = true;
                ea.AddSubMenuItem(MenuItem_ClassDefinition, "Class definition");
                _currentBaseClassRef = GetClassBaseType(_currentClass);
                if (_currentBaseClassRef != null)
                {
                    ea.AddSubMenuItem(MenuItem_ClassDefinition_InheritedClass, "Base class");
                    ea.AddSubMenuItem(MenuItem_ClassDefinition_InheritedClass_Definition, "Base class definition");
                }
            } // if
        } // navEasyGoto_CheckAvailability

        /// <summary>
        /// Nav easy goto navigate
        /// </summary>
        private void navEasyGoto_Navigate(object sender, DevExpress.CodeRush.Library.NavigationEventArgs ea)
        {
            SubMenuItem selectedMenuItem = ea.SelectedSubMenuItem;
            if (selectedMenuItem != null)
            {
                CodeRush.Markers.Drop(MarkerStyle.System);
                if (selectedMenuItem.Name == MenuItem_ClassDefinition)
                    CodeRush.Caret.MoveTo(_currentClass.NameRange.Start);
                else if (selectedMenuItem.Name == MenuItem_ClassDefinition_InheritedClass)
                    CodeRush.Caret.MoveTo(_currentBaseClassRef.NameRange.Start);
                else if (selectedMenuItem.Name == MenuItem_ClassDefinition_InheritedClass_Definition)
                {
                    IElement baseClassDecl = _currentBaseClassRef.GetDeclaration();
                    if (baseClassDecl.FirstFile == null)
                    {
                        CodeRush.Caret.MoveTo(_currentBaseClassRef.NameRange.Start);
                        CodeRush.Command.Execute("Edit.GotoDefinition");
                    }
                    else
                    {
                        CodeRush.File.Activate(baseClassDecl.FirstFile.Name);
                        CodeRush.Caret.MoveTo(baseClassDecl.FirstNameRange.Start);
                    }
                } // else if
            } // if
        } // navEasyGoto_Navigate
    } // class EasyGotoPlugIn
} // namespace CR_EasyGoto

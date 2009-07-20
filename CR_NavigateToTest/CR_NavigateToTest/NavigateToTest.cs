using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using DevExpress.Refactor.Core;
using System.Collections.Generic;

namespace CR_NavigateToTest
{
    public partial class NavigateToTest : StandardPlugIn
    {
        List<ITypeElement> _classes;
        private string _searchedElement;
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

        private void navigationProvider1_CheckAvailability(object sender, CheckContentAvailabilityEventArgs ea)
        {
            if (ea.Element == null)
                return;
            IElement declaration;

            if (ea.Element.ElementType == LanguageElementType.TypeReferenceExpression)
                declaration = ea.Element.GetDeclaration();
            else
                declaration = (IElement)ea.Element.GetClass() ?? (IElement)ea.Element.GetStruct() ?? (IElement)ea.Element.GetInterface();

            if (declaration == null)
                return;

            ITypeElement typeElement = declaration as ITypeElement;
            if (typeElement == null)
                return;

            _classes = new List<ITypeElement>();
            var elements = typeElement.FindAllReferences().ToLanguageElementCollection();
            
            var testClasses = elements.OfType<LanguageElement>().Select(ele => ele.GetClass()).Distinct();
            testClasses = testClasses.Where(cls => cls != null)
                                     .Where(cls => cls.AttributeCount > 0)
                                     .Where(cls => cls.Attributes.OfType<DevExpress.CodeRush.StructuralParser.Attribute>().Count(attr => attr.Name == "TestFixture") > 0);
            if(testClasses.Count() == 0)
            {
                ea.Available = false;
                return;
            }
            ea.Available = true;
            ea.MenuCaption = String.Format("{0} Test(s)", typeElement.Name);
            
            foreach(Class testClass in testClasses)
            {
                if (_classes.Count(c => c.FullName == testClass.FullName) != 0)
                    continue;

                _classes.Add(testClass);
                ea.AddSubMenuItem(testClass.FullName, testClass.Name, typeElement.Name);
            }
        }

        private void navigationProvider1_Navigate(object sender, DevExpress.CodeRush.Library.NavigationEventArgs ea)
        {
            SubMenuItem item = ea.SelectedSubMenuItem;
            if (item == null)
                return;

            ITypeElement testClass = _classes.Where(c => c.FullName == item.Name).FirstOrDefault();
            if (testClass != null)
            {
                CodeRush.Markers.Drop();
                CodeRush.Navigation.Navigate(testClass.GetDeclaration());
            }
        }
    }
}
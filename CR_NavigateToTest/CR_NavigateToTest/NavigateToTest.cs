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
        List<LanguageElement> _classes;
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

            _classes = new List<LanguageElement>();
            var elements = typeElement.FindAllReferences().ToLanguageElementCollection();
            var elementsInTests = from element in elements.OfType<LanguageElement>()
                                  let cls = element.GetClass()
                                  where cls != null &&
                                        cls.AttributeCount > 0 &&
                                        cls.Attributes.OfType<DevExpress.CodeRush.StructuralParser.Attribute>().Count(attr => attr.Name == "TestFixture") > 0
                                  select element;

            if (elementsInTests.Count() == 0)
            {
                ea.Available = false;
                return;
            }

            _classes = elementsInTests.ToList();
            
            ea.Available = true;
            ea.MenuCaption = String.Format("{0} Test{1}", typeElement.Name, _classes.MoreThanOne() ? "s":"");

        }

        private void navigationProvider1_Navigate(object sender, DevExpress.CodeRush.Library.NavigationEventArgs ea)
        {
            var location = GetCaretPositionScreenPoint(true);
            
            var form = new frmPickTarget(_classes);
            if (form.ShowAt(CodeRush.IDE,location) == DialogResult.OK)
            {
                var selectedElement = form.SelectedElement;
                if (selectedElement != null && selectedElement.FileNode != null)
                {
                    CodeRush.Markers.Drop(MarkerStyle.System);
                    CodeRush.File.Activate(selectedElement.FileNode.Name);
                    var start = selectedElement.Range.Start;
                    CodeRush.Caret.MoveTo(start);
                    locatorBeacon1.Start(CodeRush.TextViews.Active,start.Line, start.Offset);
                }
            }
        }

        
        public static Point GetCaretPositionScreenPoint(bool newLine)
        {
            SourcePoint point2;
            SourcePoint active = CodeRush.Caret.SourcePoint; ;
            if (active == null)
            {
                return Point.Empty;
            }
            if (newLine)
            {
                point2 = new SourcePoint(active.Line + 1, active.Offset);
            }
            else
            {
                point2 = new SourcePoint(active.Line, active.Offset);
            }
            return CodeRush.TextViews.Active.ToScreenPoint(CodeRush.TextViews.Active.GetPoint(point2));
        }

    }

    public static class Extensions
    {
        public static bool MoreThanOne<T>(this IEnumerable<T> items)
        {
            return items.Count() > 1;
        }
    }
}
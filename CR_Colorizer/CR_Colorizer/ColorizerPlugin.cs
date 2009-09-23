using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Collections.Generic;

namespace CR_Colorizer
{


    public partial class Colorizer_Plugin : StandardPlugIn
    {
        internal struct ElementOptions
        {
            internal string ElementDescription;
            internal IEnumerable<MemberVisibility> Visibilities;
            internal IEnumerable<LanguageElementType> Types;
            internal Func<LanguageElement, bool> Predicate;

            internal bool ShouldColorize(LanguageElement element)
            {
                if (element == null)
                    return false;
                if (!Colorizer_Options.ElementIsEnabled(this.ElementDescription))
                    return false;

                if (element.ElementType == LanguageElementType.ElementReferenceExpression)
                    return CheckReference(element);
                return (!(element is IMemberElement) || (element is IMemberElement && Visibilities.Contains(((IMemberElement)element).Visibility))) && Types.Contains(element.ElementType) && (Predicate == null || Predicate(element));
            }
            private bool CheckReference(LanguageElement element)
            {
                return ShouldColorize(element.GetDeclaration() as LanguageElement);
            }
        }

        private List<ElementColors> _paintableElements;
        
        private List<ElementOptions> _elements;
        // DXCore-generated code...
        #region InitializePlugIn
        public override void InitializePlugIn()
        {
            _paintableElements = new List<ElementColors>();
            base.InitializePlugIn();
            _elements = new List<ElementOptions>
                        {
                            new ElementOptions 
                            { 
                                ElementDescription = "Public Methods", 
                                Types = new[] { LanguageElementType.Method, LanguageElementType.MethodReferenceExpression},
                                Visibilities = new[] { MemberVisibility.Public }
                            },
                            new ElementOptions
                            {                        
                                ElementDescription = "Private Methods", 
                                Types = new[] { LanguageElementType.Method, LanguageElementType.MethodReferenceExpression},
                                Visibilities = new[] { MemberVisibility.Internal, MemberVisibility.Private, MemberVisibility.Protected, MemberVisibility.ProtectedInternal}
                            },
                            new ElementOptions
                            {
                                ElementDescription = "Fields", 
                                Types = new[] { LanguageElementType.Variable},
                                Visibilities = new[] { MemberVisibility.Internal, MemberVisibility.Private, MemberVisibility.Protected, MemberVisibility.ProtectedInternal, MemberVisibility.Public },
                                //Predicate = (e) => (e.ElementType == LanguageElementType.Variable && e.Parent.IsClassStructOrInterface()) || 
                                //    e.ElementType == LanguageElementType.ElementReferenceExpression && (e.GetDeclaration() != null && e.GetDeclaration().ElementType == LanguageElementType.Variable && e.GetDeclaration().Parent.IsClassStructOrInterface()),
                            },
                            new ElementOptions
                            {
                                ElementDescription = "Public Properties", 
                                Types = new[] { LanguageElementType.Property, LanguageElementType.MemberInitializerExpression},
                                Visibilities = new[] { MemberVisibility.Public },
                                //Predicate = e => e.ElementType == LanguageElementType.Property || e.ElementType == LanguageElementType.ElementReferenceExpression && e.GetDeclaration() is Property
                            },
                            new ElementOptions
                            {
                                ElementDescription = "Private Properties",
                                Types = new[] { LanguageElementType.Property},
                                Visibilities = new[] { MemberVisibility.Internal, MemberVisibility.Private, MemberVisibility.Protected, MemberVisibility.ProtectedInternal },
                                //Predicate = e => e.ElementType == LanguageElementType.Property || e.ElementType == LanguageElementType.ElementReferenceExpression && e.GetDeclaration() is Property
                            },
                            new ElementOptions
                            {
                                ElementDescription = "Local Fields",
                                Types = new[] { LanguageElementType.Variable, LanguageElementType.ImplicitVariable, LanguageElementType.InitializedVariable},
                                Visibilities = new[] { MemberVisibility.Local },
                                //Predicate = e => (e.ElementType == LanguageElementType.ElementReferenceExpression && e.GetDeclaration() != null && e.GetDeclaration().ElementType != LanguageElementType.Parameter && e.GetDeclaration().ElementType.In(LanguageElementType.Variable,LanguageElementType.InitializedVariable,LanguageElementType.ImplicitVariable))
                            },
                            new ElementOptions
                            {
                                ElementDescription = "Parameters",
                                Types = new[] {LanguageElementType.Parameter},
                                Visibilities = new[] { MemberVisibility.Local },
                                //Predicate = e => e.ElementType == LanguageElementType.Parameter || (e.GetDeclaration() != null && e.GetDeclaration().ElementType == LanguageElementType.Parameter)
                            }
                        };
            _elements.First();
            Colorizer_Options.UpdateCurrentDocument += RedrawStuff;
        }

        private void RedrawStuff(object sender, EventArgs e)
        {

            _paintableElements.Clear();
            if(CodeRush.TextViews.Active != null)
                CodeRush.TextViews.Active.Repaint();
        }
        #endregion
        #region FinalizePlugIn
        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //
            Colorizer_Options.UpdateCurrentDocument -= RedrawStuff;
            base.FinalizePlugIn();
        }
        #endregion

        private void Colorizer_Plugin_EditorPaintLanguageElement(EditorPaintLanguageElementEventArgs ea)
        {
            
        }

        private void Colorizer_Plugin_DocumentActivated(DocumentEventArgs ea)
        {
            _paintableElements.Clear();
        }

        private void Colorizer_Plugin_EditorPaint(EditorPaintEventArgs ea)
        {
            if (_paintableElements.Count == 0)
                BuildElementCache();
            foreach (var colorInfo in _paintableElements)
            {
                if (ea.TextView.Lines.IsVisible(colorInfo.Element.NameRange.Start.Line))
                    ea.OverlayText(colorInfo.Element.Name, colorInfo.Element.NameRange.Start, colorInfo.Color);
            }
        }

        private void Colorizer_Plugin_AfterParse(AfterParseEventArgs ea)
        {
            BuildElementCache();
        }
        private void BuildElementCache()
        {
            _paintableElements.Clear();
            if (!Colorizer_Options.IsEnabled())
                return;

            _paintableElements.AddRange(
                from element in CodeRush.Source.ActiveFileNode.GetSourceFile().AllChildren()
                let option = _elements.FirstOrDefault(e => e.ShouldColorize(element))
                where !option.Equals(default(ElementOptions))
                select new ElementColors 
                { 
                    Color = Colorizer_Options.GetColorFor(option.ElementDescription), 
                    Element = element 
                });
        }
        
    }

    public static class Extenstions
    {
        public static bool IsClassStructOrInterface(this IElement element)
        {
            return element.ElementType == LanguageElementType.Class || element.ElementType == LanguageElementType.Struct || element.ElementType == LanguageElementType.Interface;
        }

        public static bool In<T>(this T item, params T[] args)
        {
            return args.Contains<T>(item);
        }
    }

    public struct ElementColors
    {
        public LanguageElement Element;
        public Color Color;
    }

    public static class Extensions
    {
        public static IEnumerable<LanguageElement> AllChildren(this LanguageElement element)
        {
            foreach (LanguageElement item in element.Nodes)
            {
                foreach (LanguageElement child in item.AllChildren())
                    yield return child;
                
                yield return item;
            }

            foreach (LanguageElement item in element.DetailNodes)
            {
                foreach (LanguageElement child in item.AllChildren())
                    yield return child;
                
                yield return item;
            }
        }
    }
}
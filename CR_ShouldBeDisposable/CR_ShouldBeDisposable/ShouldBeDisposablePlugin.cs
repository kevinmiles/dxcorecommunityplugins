using System;
using System.Linq;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_ShouldBeDisposable
{
    public partial class ShouldBeDisposablePlugin : StandardPlugIn
    {
        private const string SHOULD_BE_DISPOSABLE_MSG = "A class containing an IDisposable should implement IDisposable to dispose of those items correctly and avoid potential memory leaks";
        
        // DXCore-generated code...

        public IDisposable TestProperty { get; set; }

        public override void InitializePlugIn()
        {
            base.InitializePlugIn();
        }

        public override void FinalizePlugIn()
        {
            //
            // TODO: Add your finalization code here.
            //

            base.FinalizePlugIn();
        }

        void cipClassContainingDisposablesShouldBeDisposable_CheckCodeIssues(object sender, CheckCodeIssuesEventArgs ea)
        {
            var resolveScope = ea.ResolveScope();
            foreach (IClassElement type in resolveScope.GetElementEnumerator(ea.Scope,new ElementTypeFilter(LanguageElementType.Class)))
            {
                if (IsDisposable(type))
                    return;
                
                var fields = type.Members.OfType<IMemberElement>().Where(m => m is IFieldElement || m is IPropertyElement);
                
                var disposables = fields.Where(f => IsDisposable(f));
                if (disposables.Count() == 0)
                    return;

                ea.AddIssue(CodeIssueType.CodeSmell, type.FirstNameRange,SHOULD_BE_DISPOSABLE_MSG);
            }

        }

        internal bool IsDisposable(IElement element)
        {
            ITypeElement typeElement;
            if (element is IClassElement)
                typeElement = element as ITypeElement;
            else
                typeElement = GetMemberType(element);
            
            return CodeRush.Source.Implements(typeElement, "System.IDisposable");
        }
        private static ITypeElement GetMemberType(IElement element)
        {
            Member memberElement = element.ToLanguageElement() as Member;
            return memberElement.MemberTypeReference.GetDeclaration() as ITypeElement;
        }
    }
}
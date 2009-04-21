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
        private const int ISSUE_SCORE = 10; // Not sure what this is used for, but it is required by the API

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
            if (ea.IsSuppressed(ea.Scope))
                return;
            if (!(ea.Scope is SourceFile))
                return;

            SourceFile file = ea.Scope as SourceFile;
            foreach (ITypeElement type in file.AllTypes)
            {
                if (!(type is Class))
                    continue;
                var currentClass = type.GetDeclaration() as Class;
                if (IsDisposable(currentClass))
                    return;

                var fields = currentClass.AllFields.OfType<Variable>().Select(f => f.FirstDetail).ToList();
                fields.AddRange(currentClass.AllProperties.OfType<Property>().Select(f => f.FirstDetail));

                var disposables = fields.Where(f => IsDisposable(f));
                if (disposables.Count() == 0)
                    return;

                ea.AddSmell(currentClass.NameRange, SHOULD_BE_DISPOSABLE_MSG,ISSUE_SCORE);                
            }

        }

        internal bool IsDisposable(LanguageElement element)
        {
            ITypeElement refElement;
            if (element.ElementType == LanguageElementType.TypeReferenceExpression)
                refElement = ((TypeReferenceExpression)element).GetDeclaration().ToLanguageElement() as ITypeElement;
            else
                refElement = element as ITypeElement;
            // Not sure why I need both "IDisposable" and "System.IDisposable" here
            return CodeRush.Source.Implements(refElement, "IDisposable") || CodeRush.Source.Implements(refElement, "System.IDisposable");
        }
    }
}
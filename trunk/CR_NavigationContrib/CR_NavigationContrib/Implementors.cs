using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.StructuralParser;
using System.Collections;

namespace CR_NavigationContrib
{
    internal class Implementors : IEnumerable<Class>, IEnumerable
    {
        private List<Class> _implementors;

        public Implementors()
        {
            _implementors = new List<Class>();
        }

        public void Load(LanguageElement element)
        {
            Reset();

            if (!OnCall(element))
                return;

            IElement declaration = element.GetDeclaration();
            if (declaration == null)
                return;

            Class declaringType = declaration.Parent as Class;
            if (declaringType == null)
                return;
            
            if (declaringType is Interface || (declaringType.IsAbstract))
            {
                LoadImplementors(declaringType);
            }
        }

        public int Count
        {
            get { return _implementors.Count; }
        }

        private void Reset()
        {
            _implementors.Clear();
        }

        private static bool OnCall(LanguageElement element)
        {
            return (element is MethodReferenceExpression);
        }

        private void LoadImplementors(Class abstraction)
        {
            ITypeElement[] decendants = abstraction.GetDescendants();
            foreach (ITypeElement implementor in decendants)
            {
                Class implementingClass = implementor.GetDeclaration() as Class;
                if (implementingClass != null)
                {
                    _implementors.Add(implementingClass);
                }
            }
        }

        #region IEnumerable<Class> Members
        
        public IEnumerator<Class> GetEnumerator()
        {
            foreach (var item in _implementors)
            {
                yield return item;
            }
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

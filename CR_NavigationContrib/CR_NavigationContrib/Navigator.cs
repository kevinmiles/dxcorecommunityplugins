using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;
using System.Collections.Generic;
using DevExpress.Refactor.Core;

namespace CR_NavigationContrib
{
    public class Navigator
    {
        /// <summary>
        /// The source code element that the navigation was initiated on.
        /// </summary>
        private LanguageElement _elementActivated;

        /// <summary>
        /// The class that contains the implementation of the invocation of _elementActivated.
        /// </summary>
        private Class _targetClass;


        public Navigator(Class targetClass, LanguageElement element)
        {
            _elementActivated = element;
            _targetClass = targetClass;
        }

        public void Navigate()
        {
            if (_elementActivated == null || _targetClass == null)
                return;

            Method activatedMethod = _elementActivated.GetDeclaration() as Method;
            if (activatedMethod == null)
                return;

            Method matchingMethodInClass = FindMatchingMethodInClass(activatedMethod);
            if (matchingMethodInClass == null)
                return;

            CodeRush.File.Activate(matchingMethodInClass.FileNode.FilePath);

            CodeRush.Documents.ActiveTextView.Selection.Set(matchingMethodInClass.NameRange);
        }

        private Method FindMatchingMethodInClass(Method activatedMethod)
        {
            ISourceTreeResolver resolver = new SourceTreeResolver();
            Method matchingMethod = null;
            foreach (IElement node in _targetClass.Nodes)
            {
                if (node is Method)
                {
                    if (node.Name != activatedMethod.Name)
                        continue;

                    if (SignatureHelper.SignaturesMatch(resolver, activatedMethod, node as Method))
                    {
                        matchingMethod = node as Method;
                    }
                }
            }
            return matchingMethod;
        }
    }
}

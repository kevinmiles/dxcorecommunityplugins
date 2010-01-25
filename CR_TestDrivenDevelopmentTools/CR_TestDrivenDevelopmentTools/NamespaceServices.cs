using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.StructuralParser;
using System.IO;

namespace CR_TestDrivenDevelopmentTools
{
    public class NamespaceServices
    {
        public string GetNamespaceForElement(LanguageElement element)
        {
            IElement parent = element.Parent;

            while (parent != null && parent.ElementType != LanguageElementType.Namespace)
            {
                parent = parent.Parent;
            }

            return parent == null ? string.Empty : parent.Name;
        }
        public IEnumerable<NamespaceReference> BuildNamespaceReferences(LanguageElement element)
        {
            IList<NamespaceReference> namespaceReferences = new List<NamespaceReference>();
            ElementBuilder builder = new ElementBuilder();

            foreach (var reference in CodeRush.Source.NamespaceReferences.Keys)
            {
                namespaceReferences.Add(builder.AddNamespaceReference(element.FileNode, CodeRush.Source.NamespaceReferences[reference].ToString()));
            }

            return namespaceReferences;
        }
    }
}

using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace RenameXamlNamespacePrefix
{
  [Export(typeof(IVsixPluginExtension))]
  public class RenameXamlNamespacePrefixExtension : IVsixPluginExtension { }
}
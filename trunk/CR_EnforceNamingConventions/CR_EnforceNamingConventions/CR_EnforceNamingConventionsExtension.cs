using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_EnforceNamingConventions
{
  [Export(typeof(IVsixPluginExtension))]
  public class CR_EnforceNamingConventionsExtension : IVsixPluginExtension { }
}
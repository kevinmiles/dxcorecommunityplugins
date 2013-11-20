using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_MultiSelect
{
  [Export(typeof(IVsixPluginExtension))]
  public class CR_MultiSelectExtension : IVsixPluginExtension { }
}
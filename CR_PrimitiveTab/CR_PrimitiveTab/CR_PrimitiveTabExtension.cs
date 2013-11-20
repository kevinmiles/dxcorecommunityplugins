using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_PrimitiveTab
{
  [Export(typeof(IVsixPluginExtension))]
  public class CR_PrimitiveTabExtension : IVsixPluginExtension { }
}
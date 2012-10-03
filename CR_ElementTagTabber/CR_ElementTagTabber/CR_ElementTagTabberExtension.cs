using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_ElementTagTabber
{
  [Export(typeof(IVsixPluginExtension))]
  public class CR_ElementTagTabberExtension : IVsixPluginExtension { }
}
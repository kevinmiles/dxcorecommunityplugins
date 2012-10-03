using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_DrawLinesBetweenMethods
{
  [Export(typeof(IVsixPluginExtension))]
  public class CR_DrawLinesBetweenMethodsExtension : IVsixPluginExtension { }
}
using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_BlockPainterPlus
{
  [Export(typeof(IVsixPluginExtension))]
  public class CR_BlockPainterPlusExtension : IVsixPluginExtension { }
}
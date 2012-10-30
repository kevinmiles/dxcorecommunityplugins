using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_JumpToType
{
  [Export(typeof(IVsixPluginExtension))]
  public class CR_JumpToTypeExtension : IVsixPluginExtension { }
}
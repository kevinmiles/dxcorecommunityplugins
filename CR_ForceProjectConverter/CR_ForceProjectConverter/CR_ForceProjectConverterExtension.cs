using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_ForceProjectConverter
{
  [Export(typeof(IVsixPluginExtension))]
  public class CR_ForceProjectConverterExtension : IVsixPluginExtension { }
}
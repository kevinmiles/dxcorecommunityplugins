using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace DXCore_TestAppC
{
  [Export(typeof(IVsixPluginExtension))]
  public class DXCore_TestAppCExtension : IVsixPluginExtension { }
}
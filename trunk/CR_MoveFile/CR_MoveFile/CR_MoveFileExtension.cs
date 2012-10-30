using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_MoveFile
{
  [Export(typeof(IVsixPluginExtension))]
  public class CR_MoveFileExtension : IVsixPluginExtension { }
}
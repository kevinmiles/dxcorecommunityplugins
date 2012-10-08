using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_INotifyPropChangeRename
{
  [Export(typeof(IVsixPluginExtension))]
  public class CR_INotifyPropChangeRenameExtension : IVsixPluginExtension { }
}
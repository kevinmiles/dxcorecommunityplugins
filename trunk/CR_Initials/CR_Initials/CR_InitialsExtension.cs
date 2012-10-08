using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_Initials
{
  [Export(typeof(IVsixPluginExtension))]
  public class CR_InitialsExtension : IVsixPluginExtension { }
}
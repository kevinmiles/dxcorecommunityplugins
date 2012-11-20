using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_TranslationToolWindow
{
  [Export(typeof(IVsixPluginExtension))]
  public class CR_TranslationToolWindowExtension : IVsixPluginExtension { }
}
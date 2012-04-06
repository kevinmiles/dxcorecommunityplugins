using System.ComponentModel.Composition;
using DevExpress.CodeRush.Common;

namespace CR_JoinLines
{
	[Export(typeof(IVsixPluginExtension))]
	public class VsixPluginExtension : IVsixPluginExtension
	{
	}
}

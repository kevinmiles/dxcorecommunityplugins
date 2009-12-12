using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace Impromptu
{
	[UserLevel(UserLevel.NewUser)]
	public partial class ImpromptuOptions : OptionsPage
	{
		// DXCore-generated code...
		#region Initialize
		protected override void Initialize()
		{
			base.Initialize();

			//
			// TODO: Add your initialization code here.
			//
		}
		#endregion

		#region GetCategory
		public static string GetCategory()
		{
			return @"";
		}
		#endregion
		#region GetPageName
		public static string GetPageName()
		{
			return @"Impromptu";
		}
		#endregion

		public static bool ReadDisplayIcon(DecoupledStorage storage)
		{
			return storage.ReadBoolean("Preferences", "DisplayRunIcon", true);
		}

		private void ImpromptuOptions_PreparePage(object sender, OptionsPageStorageEventArgs ea)
		{
			dispalyTile.Checked = ReadDisplayIcon(ea.Storage);
		}

		private void ImpromptuOptions_CommitChanges(object sender, CommitChangesEventArgs ea)
		{
			ea.Storage.WriteBoolean("Preferences", "DisplayRunIcon", dispalyTile.Checked);
		}

	}
}
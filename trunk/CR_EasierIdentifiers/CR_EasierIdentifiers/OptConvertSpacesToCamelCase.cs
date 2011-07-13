using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_EasierIdentiers
{
	[UserLevel(UserLevel.NewUser)]
	public partial class OptConvertSpacesToCamelCase : OptionsPage
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
			return @"Editor\Auto Complete";
		}
		#endregion
		#region GetPageName
		public static string GetPageName()
		{
			return @"Spaces To CamelCase";
		}
		#endregion

		private void OptConvertSpacesToCamelCase_CommitChanges(object sender, CommitChangesEventArgs ea)
		{
			ea.Storage.WriteBoolean("SpacesToCamelCase", "Enabled", chkEnabled.Checked);
			ea.Storage.WriteBoolean("SpacesToCamelCase", "Parameters", chkEnableInParameters.Checked);
			ea.Storage.WriteBoolean("SpacesToCamelCase", "Locals", chkEnableLocalVariables.Checked);
		}

		private void OptConvertSpacesToCamelCase_PreparePage(object sender, OptionsPageStorageEventArgs ea)
		{
			chkEnabled.Checked = ea.Storage.ReadBoolean("SpacesToCamelCase", "Enabled", true);
			chkEnableInParameters.Checked = ea.Storage.ReadBoolean("SpacesToCamelCase", "Parameters", false);
			chkEnableLocalVariables.Checked = ea.Storage.ReadBoolean("SpacesToCamelCase", "Locals", false);
		}

		private void OptConvertSpacesToCamelCase_RestoreDefaults(object sender, OptionsPageEventArgs ea)
		{
			chkEnabled.Checked = true;
			chkEnableInParameters.Checked = false;
			chkEnableLocalVariables.Checked = false;
		}
	}
}
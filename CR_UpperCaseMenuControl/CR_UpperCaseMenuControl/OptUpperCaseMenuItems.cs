using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_UpperCaseMenuControl
{
	[UserLevel(UserLevel.NewUser)]
	public partial class OptUpperCaseMenuItems : OptionsPage
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
			return @"IDE";
		}
		#endregion
		#region GetPageName
		public static string GetPageName()
		{
			return @"UPPER CASE MENUS";
		}
		#endregion

		private void OptUpperCaseMenuItems_PreparePage(object sender, OptionsPageStorageEventArgs ea)
		{
			Microsoft.Win32.RegistryKey baseKey = Microsoft.Win32.Registry.CurrentUser;
			Microsoft.Win32.RegistryKey key = baseKey.OpenSubKey("Software\\Microsoft\\VisualStudio\\11.0\\General");
			object value = key.GetValue("SuppressUppercaseConversion");
			if (value == null || !(value is int) || (int)value != 1)
				rbnALLCAPS.Checked = true;
			else
				chkNormalCase.Checked = true;
		}

		private void OptUpperCaseMenuItems_CommitChanges(object sender, CommitChangesEventArgs ea)
		{
			Microsoft.Win32.RegistryKey baseKey = Microsoft.Win32.Registry.CurrentUser;
			int newValue;
			if (rbnALLCAPS.Checked)
				newValue = 0;
			else
				newValue = 1;

			using (Microsoft.Win32.RegistryKey key = baseKey.OpenSubKey("Software\\Microsoft\\VisualStudio\\11.0\\General", true))
				key.SetValue("SuppressUppercaseConversion", newValue, Microsoft.Win32.RegistryValueKind.DWord);
		}

		private void OptUpperCaseMenuItems_RestoreDefaults(object sender, OptionsPageEventArgs ea)
		{
			rbnALLCAPS.Checked = true;
		}
	}
}
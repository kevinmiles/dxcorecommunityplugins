using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace DevExpress.CodeRush.Core
{
	[UserLevel(UserLevel.NewUser)]
	public partial class OptMultiSelect : OptionsPage
	{
		public const string KEY_Appearance = "Appearance";
		public const string KEY_SelectionColor = "SelectionColor";
		public static Color DefaultSelectionColor = Color.FromArgb(0x00, 0xBE, 0x8B);
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
			return @"Editor\Selections";
		}
		#endregion
		#region GetPageName
		public static string GetPageName()
		{
			return @"Multi-Select";
		}
		#endregion

		private void OptMultiSelect_PreparePage(object sender, OptionsPageStorageEventArgs ea)
		{
			clrSelection.Color = ea.Storage.ReadColor(KEY_Appearance, KEY_SelectionColor, DefaultSelectionColor);
		}

		private void OptMultiSelect_CommitChanges(object sender, CommitChangesEventArgs ea)
		{
			ea.Storage.WriteColor(KEY_Appearance, KEY_SelectionColor, clrSelection.Color);
		}

		private void OptMultiSelect_RestoreDefaults(object sender, OptionsPageEventArgs ea)
		{
			clrSelection.Color = DefaultSelectionColor;
		}
	}
}
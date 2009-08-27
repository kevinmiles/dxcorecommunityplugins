using System;
using System.Drawing;
using DevExpress.CodeRush.Core;

namespace RedGreen
{
	[UserLevel(UserLevel.NewUser)]
	public partial class OptRedGreenPlugIn : OptionsPage
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
			return @"Editor";
		}
		#endregion
		#region GetPageName
		public static string GetPageName()
		{
			return @"Red Green";
		}
		#endregion

		public static bool ReadDrawAdHocIcon(DecoupledStorage storage)
		{
			return storage.ReadBoolean("Preferences", "DrawAdHocIcon", true);
		}

		private class Default
		{
			public const bool DrawAdHoc = true;

			public class PassColor
			{
				public static int Alpha = 50;
				public static int Red = 157;
				public static int Green = 254;
				public static int Blue = 133;
			}

			public class FailColor
			{
				public static int Alpha = 50;
				public static int Red = 255;
				public static int Green = 140;
				public static int Blue = 140;
			}

			public class SkipColor
			{
				public static int Alpha = 50;
				public static int Red = 255;
				public static int Green = 255;
				public static int Blue = 70;
			}
		}

		public static Color ReadTestPassColor(DecoupledStorage storage)
		{
			int alpha = storage.ReadInt32("Preferences", "PassAlphaComponent", Default.PassColor.Alpha);
			int red = storage.ReadInt32("Preferences", "PassRedComponent", Default.PassColor.Red);
			int blue = storage.ReadInt32("Preferences", "PassGreenComponent", Default.PassColor.Green);
			int green = storage.ReadInt32("Preferences", "PassBlueComponent", Default.PassColor.Blue);
			return Color.FromArgb(alpha, red, blue, green);
		}

		public static Color ReadTestFailColor(DecoupledStorage storage)
		{
			int alpha = storage.ReadInt32("Preferences", "FailAlphaComponent", Default.FailColor.Alpha);
			int red = storage.ReadInt32("Preferences", "FailRedComponent", Default.FailColor.Red);
			int blue = storage.ReadInt32("Preferences", "FailGreenComponent", Default.FailColor.Green);
			int green = storage.ReadInt32("Preferences", "FailBlueComponent", Default.FailColor.Blue);
			return Color.FromArgb(alpha, red, blue, green);
		}

		public static Color ReadTestSkipColor(DecoupledStorage storage)
		{
			int alpha = storage.ReadInt32("Preferences", "SkipAlphaComponent", Default.SkipColor.Alpha);
			int red = storage.ReadInt32("Preferences", "SkipRedComponent", Default.SkipColor.Red);
			int blue = storage.ReadInt32("Preferences", "SkipGreenComponent", Default.SkipColor.Green);
			int green = storage.ReadInt32("Preferences", "SkipBlueComponent", Default.SkipColor.Blue);
			return Color.FromArgb(alpha, red, blue, green);
		}

		private void OptRedGreenPlugIn_PreparePage(object sender, OptionsPageStorageEventArgs ea)
		{
			drawAdHocIcon.Checked = ReadDrawAdHocIcon(ea.Storage);
			
			passColor.BackColor = ReadTestPassColor(ea.Storage);
			passAlpha.Maximum = 100;
			passAlpha.Value = passColor.BackColor.A;
			
			failColor.BackColor = ReadTestFailColor(ea.Storage);
			failAlpha.Maximum = 100;
			failAlpha.Value = failColor.BackColor.A;
			
			skipColor.BackColor = ReadTestSkipColor(ea.Storage);
			skipAlpha.Maximum = 100;
			skipAlpha.Value = skipColor.BackColor.A;
		}

		private void OptRedGreenPlugIn_CommitChanges(object sender, CommitChangesEventArgs ea)
		{
			ea.Storage.WriteBoolean("Preferences", "DrawAdHocIcon", drawAdHocIcon.Checked);

			ea.Storage.WriteInt32("Preferences", "PassAlphaComponent", passColor.BackColor.A);
			ea.Storage.WriteInt32("Preferences", "PassRedComponent", passColor.BackColor.R);
			ea.Storage.WriteInt32("Preferences", "PassGreenComponent", passColor.BackColor.G);
			ea.Storage.WriteInt32("Preferences", "PassBlueComponent", passColor.BackColor.B);

			ea.Storage.WriteInt32("Preferences", "FailAlphaComponent", failColor.BackColor.A);
			ea.Storage.WriteInt32("Preferences", "FailRedComponent", failColor.BackColor.R);
			ea.Storage.WriteInt32("Preferences", "FailGreenComponent", failColor.BackColor.G);
			ea.Storage.WriteInt32("Preferences", "FailBlueComponent", failColor.BackColor.B);

			ea.Storage.WriteInt32("Preferences", "SkipAlphaComponent", skipColor.BackColor.A);
			ea.Storage.WriteInt32("Preferences", "SkipRedComponent", skipColor.BackColor.R);
			ea.Storage.WriteInt32("Preferences", "SkipGreenComponent", skipColor.BackColor.G);
			ea.Storage.WriteInt32("Preferences", "SkipBlueComponent", skipColor.BackColor.B);
		}

		private void OptRedGreenPlugIn_RestoreDefaults(object sender, OptionsPageEventArgs ea)
		{
			drawAdHocIcon.Checked = true;

			passColor.BackColor = Color.FromArgb(Default.PassColor.Alpha, Default.PassColor.Red, Default.PassColor.Green, Default.PassColor.Blue);
			passAlpha.Value = Default.PassColor.Alpha;

			failColor.BackColor = Color.FromArgb(Default.FailColor.Alpha, Default.FailColor.Red, Default.FailColor.Green, Default.FailColor.Blue);
			failAlpha.Value = Default.FailColor.Alpha;

			skipColor.BackColor = Color.FromArgb(Default.SkipColor.Alpha, Default.SkipColor.Red, Default.SkipColor.Green, Default.SkipColor.Blue);
			skipAlpha.Value = Default.SkipColor.Alpha;
		}

		private void passAlpha_ValueChanged(object sender, EventArgs e)
		{
			passColor.BackColor = Color.FromArgb(passAlpha.Value, passColor.BackColor.R, passColor.BackColor.G, passColor.BackColor.B);
		}

		private void failAlpha_ValueChanged(object sender, EventArgs e)
		{
			failColor.BackColor = Color.FromArgb(failAlpha.Value, failColor.BackColor.R, failColor.BackColor.G, passColor.BackColor.B);
		}

		private void skipAlpha_ValueChanged(object sender, EventArgs e)
		{
			skipColor.BackColor = Color.FromArgb(skipAlpha.Value, skipColor.BackColor.R, skipColor.BackColor.G, skipColor.BackColor.B);
		}
	}
}
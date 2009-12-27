using System;
using System.Drawing;
using DevExpress.CodeRush.Core;

namespace UnitTestErrorVisualizer
{
	[UserLevel(UserLevel.NewUser)]
	public partial class OptUnitTestVisualizer : OptionsPage
	{
		private const string kShadeAttributeKey = "ShadeAttribute";
		private const string kDrawArrowKey = "DrawArrow";
		private const string kOverlayErrorKey = "OverlayError";
		private const string kPreferencesSection = "Preferences";
		private const string kMaxContextLength = "MaxContentLength";
		private const string kShortenLongStrings = "ShortenLongStrings";
		private const string kConvertEscape = "ConvertEscape";
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
			return @"UnitTestVisualizer";
		}
		#endregion

		private class Default
		{
			public const bool ShadeAttribute = true;
			public const bool DrawArrow = true;
			public const bool ShortenLongStrings = true;
			public const string ContextLength = "40";
			public const bool ConvertEscapeCharacters = true;
			public const bool OverlayError = false;

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

		public static bool ReadShadeAttribute(DecoupledStorage storage)
		{
			return storage.ReadBoolean(kPreferencesSection, kShadeAttributeKey, Default.ShadeAttribute);
		}

		public static bool ReadDrawArrow(DecoupledStorage storage)
		{
			return storage.ReadBoolean(kPreferencesSection, kDrawArrowKey, Default.DrawArrow);
		}

		public static bool ReadShortenLongStrings(DecoupledStorage storage)
		{
			return storage.ReadBoolean(kPreferencesSection, kShortenLongStrings, Default.ShortenLongStrings);
		}

		public static string ReadMaxContextLength(DecoupledStorage storage)
		{
			return storage.ReadString(kPreferencesSection, kMaxContextLength, Default.ContextLength);
		}

		public static bool ReadConvertEscapeCharacters(DecoupledStorage storage)
		{
			return storage.ReadBoolean(kPreferencesSection, kConvertEscape, Default.ConvertEscapeCharacters);
		}

		public static bool ReadOverlayError(DecoupledStorage storage)
		{
			return storage.ReadBoolean(kPreferencesSection, kOverlayErrorKey, Default.OverlayError);
		}

		public static Color ReadTestPassColor(DecoupledStorage storage)
		{
			int alpha = storage.ReadInt32(kPreferencesSection, "PassAlphaComponent", Default.PassColor.Alpha);
			int red = storage.ReadInt32(kPreferencesSection, "PassRedComponent", Default.PassColor.Red);
			int blue = storage.ReadInt32(kPreferencesSection, "PassGreenComponent", Default.PassColor.Green);
			int green = storage.ReadInt32(kPreferencesSection, "PassBlueComponent", Default.PassColor.Blue);
			return Color.FromArgb(alpha, red, blue, green);
		}

		public static Color ReadTestFailColor(DecoupledStorage storage)
		{
			int alpha = storage.ReadInt32(kPreferencesSection, "FailAlphaComponent", Default.FailColor.Alpha);
			int red = storage.ReadInt32(kPreferencesSection, "FailRedComponent", Default.FailColor.Red);
			int blue = storage.ReadInt32(kPreferencesSection, "FailGreenComponent", Default.FailColor.Green);
			int green = storage.ReadInt32(kPreferencesSection, "FailBlueComponent", Default.FailColor.Blue);
			return Color.FromArgb(alpha, red, blue, green);
		}

		public static Color ReadTestSkipColor(DecoupledStorage storage)
		{
			int alpha = storage.ReadInt32(kPreferencesSection, "SkipAlphaComponent", Default.SkipColor.Alpha);
			int red = storage.ReadInt32(kPreferencesSection, "SkipRedComponent", Default.SkipColor.Red);
			int blue = storage.ReadInt32(kPreferencesSection, "SkipGreenComponent", Default.SkipColor.Green);
			int green = storage.ReadInt32(kPreferencesSection, "SkipBlueComponent", Default.SkipColor.Blue);
			return Color.FromArgb(alpha, red, blue, green);
		}

		private void UnitTestVisualizer_PreparePage(object sender, OptionsPageStorageEventArgs ea)
		{
			shadeAttribute.Checked = ReadShadeAttribute(ea.Storage);
			arrowToFailed.Checked = ReadDrawArrow(ea.Storage);
			shortenLongStrings.Checked = ReadShortenLongStrings(ea.Storage);
			maxContextLength.Text = ReadMaxContextLength(ea.Storage);
			convertEscape.Checked = ReadConvertEscapeCharacters(ea.Storage);
			overlayMessage.Checked = ReadOverlayError(ea.Storage);

			//Not going to put up the color options yet. Later maybe.
		}

		private void UnitTestVisualizer_CommitChanges(object sender, CommitChangesEventArgs ea)
		{
			ea.Storage.WriteBoolean(kPreferencesSection, kShadeAttributeKey, shadeAttribute.Checked);
			ea.Storage.WriteBoolean(kPreferencesSection, kDrawArrowKey, arrowToFailed.Checked);
			ea.Storage.WriteBoolean(kPreferencesSection, kShortenLongStrings, shortenLongStrings.Checked);
			ea.Storage.WriteInt32(kPreferencesSection, kMaxContextLength, Convert.ToInt32(maxContextLength.Text));
			ea.Storage.WriteBoolean(kPreferencesSection, kConvertEscape, convertEscape.Checked);
			ea.Storage.WriteBoolean(kPreferencesSection, kOverlayErrorKey, overlayMessage.Checked);
		}

		private void UnitTestVisualizer_RestoreDefaults(object sender, OptionsPageEventArgs ea)
		{
			shadeAttribute.Checked = Default.ShadeAttribute;
			arrowToFailed.Checked = Default.DrawArrow;
			shortenLongStrings.Checked = Default.ShortenLongStrings;
			maxContextLength.Text = Default.ContextLength;
			convertEscape.Checked = Default.ConvertEscapeCharacters;
			overlayMessage.Checked = Default.OverlayError;
		}

		private void arrowToFailed_CheckedChanged(object sender, EventArgs e)
		{
			shortenLongStrings.Enabled = arrowToFailed.Checked;
			maxContextLength.Enabled = arrowToFailed.Checked;
			convertEscape.Enabled = arrowToFailed.Checked;
		}

		private void contextSize_Validating(object sender, System.ComponentModel.CancelEventArgs e)
		{
			try
			{
				Convert.ToInt32(maxContextLength.Text);
			}
			catch (Exception ex)
			{
				e.Cancel = true;
			}
		}

	}
}
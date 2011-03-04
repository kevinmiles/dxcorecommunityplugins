using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_MarkerExtensions
{
  [UserLevel( UserLevel.NewUser )]
  public partial class Opt_MarkerExtensions : OptionsPage
  {
    // DXCore-generated code...
    #region Initialize
    protected override void Initialize()
    {
      base.Initialize();
      Disposed += (sender, e) =>
        {
          if ( _testBeacon != null )
          {
            _testBeacon.Dispose();
            _testBeacon = null;
          }
        };
      _testBeacon = new DevExpress.CodeRush.PlugInCore.GdiLocatorBeacon();
    }
    #endregion

    #region GetCategory
    public static string GetCategory()
    {
      return @"Editor\Navigation";
    }
    #endregion
    #region GetPageName
    public static string GetPageName()
    {
      return @"Marker Extensions";
    }
    #endregion
    #region GetStorage
    public static new DecoupledStorage GetStorage()
    {
      return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName());
    }
    #endregion

    public static void LoadSettings(PlugInSettings settings)
    {
      using ( DecoupledStorage storage = GetStorage() )
        settings.Load(storage);
    }
    public static void SaveSettings(PlugInSettings settings)
    {
      using ( DecoupledStorage storage = GetStorage() )
        settings.Save(storage);
    }

    private void ControlsToSettings()
    {
      _settings.ShowBeacon = showLocatorBeaconCheckBox.Checked;
      _settings.BeaconColor = beaconColorSelectButton.BackColor;
      _settings.BeaconDuration = beaconDurationTrackBar.Value;
      _settings.RollOverOnPrevNext = rollOverOnNextPrevCheckBox.Checked;
      _settings.SkipSelectionMarkers = skipSelectionMarkersCheckBox.Checked;
    }
    private void SettingsToControls()
    {
      showLocatorBeaconCheckBox.Checked = _settings.ShowBeacon;
      beaconColorSelectButton.BackColor = _settings.BeaconColor;
      beaconDurationTrackBar.Value = _settings.BeaconDuration;
      rollOverOnNextPrevCheckBox.Checked = _settings.RollOverOnPrevNext;
      skipSelectionMarkersCheckBox.Checked = _settings.SkipSelectionMarkers;
    }

    private DevExpress.CodeRush.PlugInCore.GdiLocatorBeacon _testBeacon;
    private PlugInSettings _settings = new PlugInSettings();

    private void Opt_MarkerExtensions_CommitChanges(object sender, CommitChangesEventArgs ea)
    {
      ControlsToSettings();
      _settings.Save(ea.Storage);
    }
    private void Opt_MarkerExtensions_PreparePage(object sender, OptionsPageStorageEventArgs ea)
    {
      _settings.Load(ea.Storage);
      SettingsToControls();
    }
    private void Opt_MarkerExtensions_RestoreDefaults(object sender, OptionsPageEventArgs ea)
    {
      _settings = new PlugInSettings();
      SettingsToControls();
    }
    private void beaconColorSelectLabel_Click(object sender, EventArgs e)
    {
      Color beaconColor = beaconColorSelectButton.BackColor;
      var customColors = new List<int>(beaconColorDialog.CustomColors);
      // this is an absolute bitch, the f'n color dialog reverses the order of the
      // RGB components AND clears the Alpha component!!!!
      customColors[0] = Color.FromArgb(0, beaconColor.B, beaconColor.G, beaconColor.R).ToArgb();
      beaconColorDialog.CustomColors = customColors.ToArray();
      beaconColorDialog.Color = beaconColor;
      if ( beaconColorDialog.ShowDialog() == DialogResult.OK )
        beaconColorSelectButton.BackColor = beaconColorDialog.Color;
    }
    private void beaconDurationTrackBar_ValueChanged(object sender, EventArgs e)
    {
      dynamicBeaconDurationLabel.Text = "(" + beaconDurationTrackBar.Value.ToString() + " ms)";
    }
    private void testBeaconButton_Click(object sender, EventArgs e)
    {
      Rectangle beaconRect = new Rectangle();
      beaconRect.Width = Math.Min(testBeaconButton.Width, testBeaconButton.Height);
      beaconRect.Height = beaconRect.Width;
      beaconRect.X = (testBeaconButton.Width - beaconRect.Width) / 2;
      beaconRect.Y = (testBeaconButton.Height - beaconRect.Height) / 2;
      if ( _testBeacon.PercentRemaining > 0f )
        _testBeacon.Stop();
      _testBeacon.Color = beaconColorSelectButton.BackColor;
      _testBeacon.Duration = beaconDurationTrackBar.Value;
      _testBeacon.Start(testBeaconButton.Handle, beaconRect);
    }
  }
}
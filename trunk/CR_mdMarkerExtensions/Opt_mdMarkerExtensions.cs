using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_mdMarkerExtensions
{
  [UserLevel( UserLevel.NewUser )]
  public partial class Opt_mdMarkerExtensions : OptionsPage
  {
    // DXCore-generated code...
    #region Initialize
    protected override void Initialize()
    {
      base.Initialize();
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
      return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage( GetCategory(), GetPageName() );
    }
    #endregion

    public static void LoadSettings( PlugInSettings settings )
    {
      using ( DecoupledStorage storage = GetStorage() )
        settings.Load( storage );
    }
    public static void SaveSettings( PlugInSettings settings )
    {
      using ( DecoupledStorage storage = GetStorage() )
        settings.Save( storage );
    }

    private void ControlsToSettings()
    {
      _settings.ShowBeacon = showLocatorBeaconCheckBox.Checked;
      _settings.BeaconColor = beaconColorSwatch.Color;
      _settings.BeaconDuration = beaconDurationTrackBar.Value;
      _settings.RollOverOnPrevNext = rollOverOnNextPrevCheckBox.Checked;
      _settings.SkipSelectionMarkers = skipSelectionMarkersCheckBox.Checked;
    }
    private void SettingsToControls()
    {
      showLocatorBeaconCheckBox.Checked = _settings.ShowBeacon;
      beaconColorSwatch.Color = _settings.BeaconColor;
      beaconDurationTrackBar.Value = _settings.BeaconDuration;
      rollOverOnNextPrevCheckBox.Checked = _settings.RollOverOnPrevNext;
      skipSelectionMarkersCheckBox.Checked = _settings.SkipSelectionMarkers;
    }

    private PlugInSettings _settings = new PlugInSettings();

    private void Opt_mdMarkerExtensions_Load( object sender, EventArgs e )
    {
      // fix the unintentional movement of the color swatch control
      beaconColorSwatch.Location = new Point( 100, 106 );
    }
    private void Opt_mdMarkerExtensions_CommitChanges( object sender, OptionsPageStorageEventArgs ea )
    {
      ControlsToSettings();
      _settings.Save( ea.Storage );
    }
    private void Opt_mdMarkerExtensions_PreparePage( object sender, OptionsPageStorageEventArgs ea )
    {
      _settings.Load( ea.Storage );
      SettingsToControls();
    }
    private void Opt_mdMarkerExtensions_RestoreDefaults( object sender, OptionsPageEventArgs ea )
    {
      _settings = new PlugInSettings();
      SettingsToControls();
    }

    private void beaconDurationTrackBar_ValueChanged( object sender, EventArgs e )
    {
      dynamicBeaconDurationLabel.Text = "(" + beaconDurationTrackBar.Value.ToString() + " ms)";
    }
    private void testBeaconButton_Click( object sender, EventArgs e )
    {
      testBeacon.Color = beaconColorSwatch.Color;
      testBeacon.Duration = beaconDurationTrackBar.Value;
      Rectangle beaconRect = new Rectangle();
      beaconRect.Width = Math.Min( testBeaconButton.Width, testBeaconButton.Height );
      beaconRect.Height = beaconRect.Width;
      beaconRect.X = (testBeaconButton.Width - beaconRect.Width) / 2;
      beaconRect.Y = (testBeaconButton.Height - beaconRect.Height) / 2;
      testBeacon.Start( testBeaconButton.Handle, beaconRect );
    }
  }
}
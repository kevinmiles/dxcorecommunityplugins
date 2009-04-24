using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_mdMarkerExtensions
{
  public class PlugInSettings
  {
    public void Load( DecoupledStorage storage )
    {
      ShowBeacon = storage.ReadBoolean( STR_Preferences, STR_ShowBeacon, ShowBeacon );
      BeaconColor = storage.ReadColor( STR_Preferences, STR_BeaconColor, BeaconColor );
      BeaconDuration = storage.ReadInt32( STR_Preferences, STR_BeaconDuration, BeaconDuration );
      RollOverOnPrevNext = storage.ReadBoolean( STR_Preferences, STR_RollOverOnPrevNext, RollOverOnPrevNext );
      SkipSelectionMarkers = storage.ReadBoolean( STR_Preferences, STR_SkipSelectionMarkers, SkipSelectionMarkers );
    }
    public void Save( DecoupledStorage storage )
    {
      storage.WriteBoolean( STR_Preferences, STR_ShowBeacon, ShowBeacon );
      storage.WriteColor( STR_Preferences, STR_BeaconColor, BeaconColor );
      storage.WriteInt32( STR_Preferences, STR_BeaconDuration, BeaconDuration );
      storage.WriteBoolean( STR_Preferences, STR_RollOverOnPrevNext, RollOverOnPrevNext );
      storage.WriteBoolean( STR_Preferences, STR_SkipSelectionMarkers, SkipSelectionMarkers );
    }

    public bool ShowBeacon
    {
      get { return _showBeacon; }
      set { _showBeacon = value; }
    }
    public Color BeaconColor
    {
      get { return _beaconColor; }
      set { _beaconColor = value; }
    }
    public int BeaconDuration
    {
      get { return _beaconDuration; }
      set { _beaconDuration = value; }
    }
    public bool RollOverOnPrevNext
    {
      get { return _rollOverOnPrevNext; }
      set { _rollOverOnPrevNext = value; }
    }
    public bool SkipSelectionMarkers
    {
      get { return _skipSelectionMarkers; }
      set { _skipSelectionMarkers = value; }
    }

    private bool _showBeacon = true;
    private Color _beaconColor = Color.SlateBlue;
    private int _beaconDuration = 200;
    private bool _rollOverOnPrevNext = false;
    private bool _skipSelectionMarkers = true;

    private const string STR_Preferences = @"Preferences";
    private const string STR_ShowBeacon = @"ShowBeacon";
    private const string STR_BeaconColor = @"BeaconColor";
    private const string STR_BeaconDuration = @"BeaconDuration";
    private const string STR_RollOverOnPrevNext = @"RollOverOnPrevNext";
    private const string STR_SkipSelectionMarkers = @"SkipSelectionMarkers";
  }
}

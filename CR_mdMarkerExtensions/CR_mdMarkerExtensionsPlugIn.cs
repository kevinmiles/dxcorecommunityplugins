using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace CR_mdMarkerExtensions
{
  public partial class CR_mdMarkerExtensionsPlugIn : StandardPlugIn
  {
    // DXCore-generated code...
    #region InitializePlugIn
    public override void InitializePlugIn()
    {
      base.InitializePlugIn();
      Opt_mdMarkerExtensions.LoadSettings( _settings );
    }
    #endregion
    #region FinalizePlugIn
    public override void FinalizePlugIn()
    {
      base.FinalizePlugIn();
    }
    #endregion

    private enum NavDestination { None, First, Prev, Next, Last, Top }

    private IMarker FindClosestMarker( List<IMarker> markers, NavDestination destination, int caretLine, int caretColumn )
    {
      IMarker closestMarker = null;
      if ( markers != null && markers.Count > 0 && destination != NavDestination.None )
      {
        IMarker firstMarker = markers[0];
        IMarker lastMarker = markers[markers.Count - 1];
        switch ( destination )
        {
          case NavDestination.First:
            closestMarker = firstMarker;
            break;
          case NavDestination.Prev:
          case NavDestination.Next:
            if ( destination == NavDestination.Prev )
              closestMarker = markers.FindLast( marker =>
                  marker.Line < caretLine || (marker.Line == caretLine && marker.Column < caretColumn) );
            else
              closestMarker = markers.Find( marker =>
                marker.Line > caretLine || (marker.Line == caretLine && marker.Column > caretColumn) );

            // if no target found and we have only a single marker, that's the new target
            if ( closestMarker == null && markers.Count == 1 )
            {
              IMarker marker = firstMarker;
              if ( marker.Line == caretLine && marker.Column == caretColumn )
                closestMarker = marker;
            }
            // if we allow "rolling over" on prev/next, do so
            if ( _settings.RollOverOnPrevNext && closestMarker == null )
            {
              if ( destination == NavDestination.Prev )
                closestMarker = lastMarker;
              else if ( destination == NavDestination.Next )
                closestMarker = firstMarker;
            }
            break;
          case NavDestination.Last:
            closestMarker = lastMarker;
            break;
          case NavDestination.Top:
            closestMarker = CodeRush.Markers.Top;
            break;
        }
      }
      return closestMarker;
    }
    private int LoadOrderedMarkerList( List<IMarker> markers )
    {
      markers.Clear();
      foreach ( IMarker marker in CodeRush.Markers )
        if ( marker.TextDocument == CodeRush.Documents.ActiveTextDocument && !marker.Hidden && !marker.Temporal )
          markers.Add( marker );

      markers.Sort( ( m1, m2 ) =>
      {
        int result = m1.Line - m2.Line;
        if ( result == 0 )
          result = m1.Column - m2.Column;
        return result;
      } );

      return markers.Count;
    }
    private void GetMarkerActionProperties( Action action, out NavDestination destination, out bool collect )
    {
      if ( action == MarkerFirstAction || action == MarkerCollectFirstAction )
        destination = NavDestination.First;
      else if ( action == MarkerPrevAction || action == MarkerCollectPrevAction )
        destination = NavDestination.Prev;
      else if ( action == MarkerNextAction || action == MarkerCollectNextAction )
        destination = NavDestination.Next;
      else if ( action == MarkerLastAction || action == MarkerCollectLastAction )
        destination = NavDestination.Last;
      else if ( action == MarkerTopAction )
        destination = NavDestination.Top;
      else
        destination = NavDestination.None;

      collect = action == MarkerCollectFirstAction || action == MarkerCollectPrevAction
        || action == MarkerCollectNextAction || action == MarkerCollectLastAction;
    }
    private IMarker GetTargetMarker( NavDestination destination )
    {
      if ( destination == NavDestination.None || CodeRush.Documents.ActiveTextDocument == null )
        return null;

      TextView activeView = CodeRush.Documents.ActiveTextDocument.ActiveView;
      if ( activeView == null )
        return null;

      if ( destination == NavDestination.Top )
        return CodeRush.Markers.Top;
      else
      {
        List<IMarker> markers = new List<IMarker>();
        LoadOrderedMarkerList( markers );
        return FindClosestMarker( markers, destination, activeView.Caret.Line, activeView.Caret.ViewColumn );
      }
    }
    private void ShineLocatorBeacon( IMarker marker )
    {
      if ( marker == null || marker.TextDocument == null || marker.TextDocument.ActiveView == null )
        return;

      if ( _settings.ShowBeacon )
      {
        locatorBeacon.Color = _settings.BeaconColor;
        locatorBeacon.Duration = _settings.BeaconDuration;
        locatorBeacon.Start( marker.TextDocument.ActiveView, marker.Line, marker.Column );
      }
    }

    private PlugInSettings _settings = new PlugInSettings();

    private void CR_mdMarkerExtensionsPlugIn_OptionsChanged( OptionsChangedEventArgs e )
    {
      if ( e.OptionsPages.Contains( typeof( Opt_mdMarkerExtensions ) ) )
        Opt_mdMarkerExtensions.LoadSettings( _settings );
    }
    private void MarkerAction_Execute( ExecuteEventArgs e )
    {
      NavDestination destination;
      bool collect;
      GetMarkerActionProperties( e.Action, out destination, out collect );
      IMarker marker = GetTargetMarker( destination );
      if ( marker != null )
      {
        marker.RestorePosition();
        ShineLocatorBeacon( marker );
        if ( collect )
          CodeRush.Markers.Remove( marker );
      }
    }
  }
}
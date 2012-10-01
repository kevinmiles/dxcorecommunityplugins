Imports System.ComponentModel.Composition
Imports DevExpress.CodeRush.Common

Namespace CR_ClearAllMarkers
  <Export(GetType(IVsixPluginExtension))> _
  Public Class CR_ClearAllMarkersExtension
	  Implements IVsixPluginExtension
  End Class
End Namespace
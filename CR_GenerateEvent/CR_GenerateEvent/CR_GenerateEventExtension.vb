Imports System.ComponentModel.Composition
Imports DevExpress.CodeRush.Common

Namespace CR_GenerateEvent
  <Export(GetType(IVsixPluginExtension))> _
  Public Class CR_GenerateEventExtension
	  Implements IVsixPluginExtension
  End Class
End Namespace
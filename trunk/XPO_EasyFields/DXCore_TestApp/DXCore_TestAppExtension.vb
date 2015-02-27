Imports System.ComponentModel.Composition
Imports DevExpress.CodeRush.Common

Namespace DXCore_TestApp
  <Export(GetType(IVsixPluginExtension))> _
  Public Class DXCore_TestAppExtension
	  Implements IVsixPluginExtension
  End Class
End Namespace
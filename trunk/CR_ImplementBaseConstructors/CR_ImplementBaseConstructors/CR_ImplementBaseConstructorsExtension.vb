Imports System.ComponentModel.Composition
Imports DevExpress.CodeRush.Common

Namespace CR_ImplementBaseConstructors
  <Export(GetType(IVsixPluginExtension))> _
  Public Class CR_ImplementBaseConstructorsExtension
	  Implements IVsixPluginExtension
  End Class
End Namespace
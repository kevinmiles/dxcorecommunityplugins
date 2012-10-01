Imports System.ComponentModel.Composition
Imports DevExpress.CodeRush.Common

Namespace CR_AttributeTabber
  <Export(GetType(IVsixPluginExtension))> _
  Public Class CR_AttributeTabberExtension
	  Implements IVsixPluginExtension
  End Class
End Namespace
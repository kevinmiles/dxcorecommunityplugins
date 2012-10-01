Imports System.ComponentModel.Composition
Imports DevExpress.CodeRush.Common

Namespace CR_DeclareParameter
  <Export(GetType(IVsixPluginExtension))> _
  Public Class CR_DeclareParameterExtension
	  Implements IVsixPluginExtension
  End Class
End Namespace
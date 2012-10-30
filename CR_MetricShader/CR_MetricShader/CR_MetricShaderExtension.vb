Imports System.ComponentModel.Composition
Imports DevExpress.CodeRush.Common

Namespace CR_MetricShader
  <Export(GetType(IVsixPluginExtension))> _
  Public Class CR_MetricShaderExtension
	  Implements IVsixPluginExtension
  End Class
End Namespace
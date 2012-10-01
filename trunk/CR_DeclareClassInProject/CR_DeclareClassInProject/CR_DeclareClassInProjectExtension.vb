Imports System.ComponentModel.Composition
Imports DevExpress.CodeRush.Common

Namespace CR_DeclareClassInProject
  <Export(GetType(IVsixPluginExtension))> _
  Public Class CR_DeclareClassInProjectExtension
	  Implements IVsixPluginExtension
  End Class
End Namespace
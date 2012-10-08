Imports System.ComponentModel.Composition
Imports DevExpress.CodeRush.Common

Namespace CR_JumpToImplementation
  <Export(GetType(IVsixPluginExtension))> _
  Public Class CR_JumpToImplementationExtension
	  Implements IVsixPluginExtension
  End Class
End Namespace
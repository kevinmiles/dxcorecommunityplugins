Imports System.ComponentModel.Composition
Imports DevExpress.CodeRush.Common

Namespace CR_InsertNewParentClass
  <Export(GetType(IVsixPluginExtension))> _
  Public Class CR_InsertNewParentClassExtension
	  Implements IVsixPluginExtension
  End Class
End Namespace
Imports System.Windows.Forms

Public Class CSharpAsVBNetViaDXCore
    Inherits AbstractTranslator
    Public Sub New()
        Loader = New CSharpLoader
        Renderer = New VBNetRenderer
    End Sub
End Class
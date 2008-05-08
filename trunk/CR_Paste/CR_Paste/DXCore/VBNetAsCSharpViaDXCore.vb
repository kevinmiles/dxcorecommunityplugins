Public Class VBNetAsCSharpViaDXCore
    Inherits AbstractTranslator
    Public Sub New()
        Loader = New VBNetLoader
        Renderer = New CSharpRenderer
    End Sub
End Class
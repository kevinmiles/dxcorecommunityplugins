Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class GenericRenderer
    Inherits BaseDXRenderer
    Protected mLanguageID As String
    Sub New(ByVal LanguageID As String)
        mLanguageID = LanguageID
    End Sub
    Public Overrides ReadOnly Property LanguageID() As String
        Get
            Return mLanguageID
        End Get
    End Property
End Class

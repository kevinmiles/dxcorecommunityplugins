Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Public MustInherit Class BaseDXLoader
    Implements IDXLoader
#Region "Fields"
    Protected mLanguageID As String = String.Empty
    Protected RootNode As LanguageElement
#End Region
#Region "Properties"
    Public ReadOnly Property TreeRoot() As LanguageElement Implements IDXLoader.TreeRoot
        Get
            Return RootNode
        End Get
    End Property
#End Region
    Protected ReadOnly Property LanguageID() As String Implements IDXOperable.LanguageID
        Get
            Return mLanguageID
        End Get
    End Property

    Public Overridable Sub Load(ByVal Text As String) Implements IDXLoader.Load
        Dim Parser = CodeRush.Language.GetParserFromLanguageID(LanguageID)
        RootNode = Parser.ParseString(Text)
    End Sub
End Class

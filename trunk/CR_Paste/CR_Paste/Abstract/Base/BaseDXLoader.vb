Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Public MustInherit Class BaseDXLoader
    Implements IDXLoader
#Region "Fields"
    Protected RootNode As LanguageElement
#End Region
#Region "Properties"
    Public ReadOnly Property TreeRoot() As LanguageElement Implements IDXLoader.TreeRoot
        Get
            Return RootNode
        End Get
    End Property
#End Region
    Protected MustOverride ReadOnly Property LanguageID() As String Implements IDXOperable.LanguageID

    Public Sub Load(ByVal Text As String) Implements IDXLoader.Load
        Dim ElementBuilder = New DevExpress.CodeRush.StructuralParser.ElementBuilder()
        Dim Parser = CodeRush.Language.GetParserFromLanguageID(LanguageID)
        RootNode = Parser.ParseString(Text)
    End Sub
End Class

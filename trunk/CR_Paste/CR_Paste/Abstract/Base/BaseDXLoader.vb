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

    Public Overridable Function Load(ByVal Text As String) As Boolean Implements IDXLoader.Load
        If LanguageID = String.Empty Then
            Call AllocateLanguageByExample(Text)
        End If
        If LanguageID = String.Empty Then
            Return False
        End If
        Dim Parser = CodeRush.Language.GetParserFromLanguageID(LanguageID)
        RootNode = Parser.ParseString(Text)
        Return True
    End Function
    Private Sub AllocateLanguageByExample(ByVal Text As String)
        If isTextOfCodeType(Text, "Basic") Then mLanguageID = "Basic"
        If isTextOfCodeType(Text, "CSharp") Then mLanguageID = "CSharp"
    End Sub
    Private Function isTextOfCodeType(ByVal Text As String, ByVal LanguageID As String) As Boolean
        Dim Parser = CodeRush.Language.GetParserFromLanguageID(LanguageID)
        Dim RootNode As LanguageElement = Parser.ParseString(Text)
        If CodeRush.Language.GenerateElement(RootNode) = Text Then
            Return True
        End If
        Return False
    End Function
End Class

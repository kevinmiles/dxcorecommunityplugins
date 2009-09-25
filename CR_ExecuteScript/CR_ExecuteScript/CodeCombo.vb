Public Class CodeCombo
    Private mImports As String
    Private mCode As String
    Private mReferences As String
    ''' <summary>
    ''' Initializes a new instance of the CodeCombo class.
    ''' </summary>
    ''' <param name="Imports"></param>
    ''' <param name="Code"></param>
    ''' <param name="References"></param>
    Public Sub New(ByVal [Imports] As String, ByVal Code As String, ByVal References As String)
        mImports = [Imports]
        mCode = Code
        mReferences = References
    End Sub
    Public ReadOnly Property [Imports]() As String
        Get
            Return mImports
        End Get
    End Property
    Public ReadOnly Property Code() As String
        Get
            Return mCode
        End Get
    End Property
    Public ReadOnly Property References() As String
        Get
            Return mReferences
        End Get
    End Property
End Class

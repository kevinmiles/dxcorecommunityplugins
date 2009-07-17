Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser

Public Class DocumentReference
#Region "Fields"
    Private mCaretX As Integer
    Private mCaretY As Integer
    Private mDisplay As String
    Private mFileWithPath As String
    Private mTopLine As Integer
#End Region
#Region "Simple Properties"
    Public ReadOnly Property CaretX() As Integer
        Get
            Return mCaretX
        End Get
    End Property
    Public ReadOnly Property CaretY() As Integer
        Get
            Return mCaretY
        End Get
    End Property
    Public ReadOnly Property Display() As String
        Get
            Return mDisplay
        End Get
    End Property
    Public ReadOnly Property FileWithPath() As String
        Get
            Return mFileWithPath
        End Get
    End Property
    Public ReadOnly Property TopLine() As Integer
        Get
            Return mTopLine
        End Get
    End Property
#End Region

    Public Sub New(ByVal Name As String, _
                    ByVal FullName As String, _
                    ByVal TopLine As Integer, _
                    ByVal CaretX As Integer, ByVal CaretY As Integer)
        mDisplay = Name
        mFileWithPath = FullName
        mTopLine = TopLine
        mCaretX = CaretX
        mCaretY = CaretY
    End Sub
    Public Sub New(ByVal Name As String, _
                   ByVal FullName As String, _
                   ByVal TopLine As Integer, _
                   ByVal CaretPosition As SourcePoint)
        Me.New(Name, FullName, TopLine, CaretPosition.Offset, CaretPosition.Line)
    End Sub
End Class

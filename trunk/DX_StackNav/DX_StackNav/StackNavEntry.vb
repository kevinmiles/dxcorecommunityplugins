Public Class StackNavEntry
#Region "Fields"
    Private mPath As String
    Private mFilename As String
    Private mLine As Integer
    Private mMethod As String
#End Region
#Region "Constructors"
    Public Sub New(ByVal PathAndFile As String, ByVal Line As Integer, ByVal Method As String)
        MyClass.New(PathAndFile.Substring(0, PathAndFile.LastIndexOf("\"c)), _
                    PathAndFile.Substring(PathAndFile.LastIndexOf("\"c) + 1), Line, Method)
    End Sub
    Public Sub New(ByVal Path As String, ByVal Filename As String, ByVal Line As Integer, ByVal Method As String)
        Me.mPath = Path
        Me.mFilename = Filename
        Me.mLine = Line
        Me.mMethod = Method
    End Sub
#End Region
#Region "Simple properties"
    Private ReadOnly Property Filename() As String
        Get
            Return mFilename
        End Get
    End Property
    Private ReadOnly Property Path() As String
        Get
            Return mPath
        End Get
    End Property
    Public ReadOnly Property Line() As Integer
        Get
            Return mLine
        End Get
    End Property
    Public ReadOnly Property Method() As String
        Get
            Return mMethod
        End Get
    End Property
#End Region
    Private ReadOnly Property Location As String
        Get
            Return String.Format("{0} - Line {1}", mFilename, mLine)
        End Get
    End Property
    Public ReadOnly Property PathAndFile As String
        Get
            Return String.Format("{0}\{1}", mPath, mFilename)
        End Get
    End Property

End Class

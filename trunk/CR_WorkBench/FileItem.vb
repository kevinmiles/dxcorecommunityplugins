Imports System.Windows.Forms
Imports System.IO
Public Class FileItem
    Inherits ListViewItem
    Private mFile As FileInfo
    Public Sub New(ByVal File As FileInfo)
        mFile = File
        Me.Text = Filename
        Me.SubItems.Add(FolderName)
    End Sub
    Public ReadOnly Property FolderName() As String
        Get
            Return mFile.FullName.Substring(0, mFile.FullName.LastIndexOf("\"c))
        End Get
    End Property
    Public ReadOnly Property Filename() As String
        Get
            Return mFile.FullName.Substring(mFile.FullName.LastIndexOf("\"c) + 1)
        End Get
    End Property
    Public ReadOnly Property Fullname() As String
        Get
            Return mFile.FullName
        End Get
    End Property
End Class

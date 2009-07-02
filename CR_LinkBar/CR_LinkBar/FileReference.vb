Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Menus

Public Class FileReference
#Region "Fields"
    Private mDisplay As String
    Private mFileWithPath As String
#End Region
    Public Sub New(ByVal Display As String, ByVal FileWithPath As String)
        mDisplay = Display
        mFileWithPath = FileWithPath
    End Sub
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
End Class

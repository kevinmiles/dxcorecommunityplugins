Imports System.IO
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices
Imports DevExpress.CodeRush.Menus
Imports System.Reflection
Imports DevExpress.CodeRush.Win32

Public Class PlugIn1
    Private Const CONST_GITPATH As String = "C:\Program Files (x86)\Git\bin\"
    Private Const CONST_GITEXE As String = "git.exe"

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        Call RefreshToolBar()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region
    Private mMenuBar As MenuBar
    Private mMainItems As New List(Of IMenuControl)
    Private mRSS As IMenuPopup
    Private Sub RefreshToolBar()
        If mMenuBar IsNot Nothing Then
            mMenuBar.Delete()
            mMenuBar = Nothing
        End If
        mMenuBar = CodeRush.Menus.Bars.Add("NewsBar")
        mMenuBar.Visible = True
        mMenuBar.Position = BarPosition.Top

        mMenuBar.AddButton("Console").AddProc(AddressOf CmdGitShell)
        mMenuBar.AddButton("Git init").AddProc(AddressOf GitInit)
        mMenuBar.AddButton("Git ignore").AddProc(AddressOf GitIgnore)
        mMenuBar.AddButton("Git GUI").AddProc(AddressOf GitGUI)
        mMenuBar.AddButton("Git add").AddProc(AddressOf GitAddAll)
        mMenuBar.AddButton("Git commit").AddProc(AddressOf GitCommit)
        mMenuBar.AddButton("Git status").AddProc(AddressOf GitStatus)
    End Sub
    Private Function GitExe() As String
        Return String.Format("{0}{1}", CONST_GITPATH, CONST_GITEXE)
    End Function
    Private Sub CmdGitShell()
        ExecuteCommand("C:\Windows\SysWOW64\cmd.exe", _
               String.Format("/c ""{0}sh.exe"" --login -i", CONST_GITPATH))
    End Sub
    Private Sub GitInit()
        ExecuteCommand(GitExe, "init")
    End Sub
    Private Sub GitIgnore()
        ExecuteCommand(GitExe(), "ignore")
    End Sub
    Private Sub GitGUI()
        ExecuteCommand(GitExe(), "gui", False, False)
    End Sub
    Private Sub GitAddAll()
        ExecuteCommand(GitExe(), "add .")
    End Sub
    Private Sub GitCommit()
        ExecuteCommand(GitExe(), "commit -am ")
    End Sub
    Private Sub GitStatus()
        ExecuteCommand(GitExe(), "status ")
    End Sub

    Private Sub ExecuteCommand(ByVal Command As String, ByVal Arguments As String, _
                               Optional ByVal CaptureStdout As Boolean = True, _
                               Optional ByVal ShowWindow As Boolean = False)
        If CodeRush.Solution.Active.Count = 0 Then
            Exit Sub
        End If
        Dim WorkingDirectory As String = "$(SolutionDir)"
        Call PreProcess(Command, Arguments, WorkingDirectory)
        Dim X As New ProcessStartInfo(Command, Arguments)
        X.WorkingDirectory = WorkingDirectory
        X.CreateNoWindow = Not ShowWindow
        'X.RedirectStandardOutput = True
        Dim P = System.Diagnostics.Process.Start(X)
        If CaptureStdout Then
            Do While Not P.HasExited
                NativeMethods.OutputDebugString(P.StandardOutput.ReadToEnd())
            Loop
        End If
    End Sub
    Private Sub PreProcess(ByRef Command As String, ByRef Arguments As String, ByRef WorkingDirectory As String)
        Command = ReplaceTokens(Command)
        Arguments = ReplaceTokens(Arguments)
        WorkingDirectory = ReplaceTokens(WorkingDirectory)
    End Sub

    Private Function ReplaceTokens(ByVal Value As String) As String
        ' Replace typical tokens in here.
        Dim SolutionFolder = New FileInfo(CodeRush.Solution.Active.FullName).DirectoryName
        Value = Value.Replace("$(SolutionDir)", SolutionFolder)
        Return Value
    End Function
End Class
Public Module IMenuControlExt
    <Extension()> _
    Public Function AddGraphic(ByVal Source As IMenuButton, ByVal GraphicName As String) As IMenuButton
        Try
            If Source.Caption = "X" Then
                Source.Style = ButtonStyle.Icon
            Else
                Source.Style = ButtonStyle.IconAndCaption
            End If
            Dim Image As TransparentBitmap = GetBitmapByName(GraphicName)
            Source.SetFace(Image.Bitmap, Image.MaskBitmap)
            Return Source
        Catch ex As Exception

        End Try
    End Function

    Public Function GetBitmapByName(ByVal BitmapName As String) As TransparentBitmap
        Dim Asm As Assembly = Assembly.GetExecutingAssembly
        Dim stream As IO.Stream = Asm.GetManifestResourceStream(String.Format("DX_NewsBar.{0}", BitmapName))
        Dim BitmapFromStream As Bitmap = CType(Bitmap.FromStream(stream), Bitmap)
        Return New TransparentBitmap(BitmapFromStream)
    End Function
    <Extension()> _
    Public Function AddPopup(ByVal Parent As MenuBar, ByVal Caption As String) As IMenuPopup
        Try
            Dim MenuItem As IMenuControl
            MenuItem = Parent.AddPopup()
            MenuItem.Caption = Caption
            Return MenuItem
        Catch ex As Exception

        End Try
    End Function
    <Extension()> _
    Public Function AddPopup(ByVal Parent As IMenuPopup, ByVal Caption As String) As IMenuPopup
        Try
            Dim MenuItem As IMenuControl
            MenuItem = Parent.AddPopup()
            MenuItem.Caption = Caption
            Return MenuItem
        Catch ex As Exception

        End Try
    End Function
    <Extension()> _
    Public Function AddCtrl(ByVal Parent As MenuBar, ByVal ControlTypeEdit As ControlType) As IMenuControl
        Try
            Return Parent.Add(ControlTypeEdit)
        Catch ex As Exception

        End Try
    End Function
    <Extension()> _
    Public Function AddButton(ByVal Parent As MenuBar, ByVal Caption As String) As IMenuButton
        Try
            Dim Button = Parent.AddButton()
            Button.Caption = Caption
            Button.TooltipText = ""
            Return Button
        Catch ex As Exception

        End Try
    End Function

    <Extension()> _
    Public Function AddButton(ByVal Parent As IMenuPopup, ByVal Caption As String, Optional ByVal StartsGroup As Boolean = False) As IMenuButton
        Try
            Dim Button = Parent.AddButton()
            Button.Caption = Caption
            Button.BeginGroup = StartsGroup
            Return Button
        Catch ex As Exception

        End Try
    End Function
    <Extension()> _
    Public Function AddProc(ByVal Source As IMenuButton, ByVal Action As System.Action) As IMenuButton
        AddHandler Source.Click, AddressOf Action.Invoke
        Return Source
    End Function

End Module

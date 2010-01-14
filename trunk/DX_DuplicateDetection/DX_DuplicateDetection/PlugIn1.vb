Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Text
Imports System.Security.Cryptography

Public Class PlugIn1

	'DXCore-generated code...
#Region " InitializePlugIn "
	Public Overrides Sub InitializePlugIn()
		MyBase.InitializePlugIn()

		'TODO: Add your initialization code here.
	End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region

    Private mMD5 As New MD5CryptoServiceProvider

#Region "Hash Functions"
    Public Function ContentHash(ByVal Method As Method) As HashList
        Return HashOf(Method.Nodes)
    End Function
    Public Function HashOf(ByVal ElementList As NodeList) As HashList
        Dim List As New HashList
        For Each Item In ElementList
            List.Add(HashOf(Item))
        Next
        Return List
    End Function
    Public Function HashOf(ByVal LanguageElement As LanguageElement) As String
        Dim MD5 As MD5CryptoServiceProvider = mMD5
        Dim TheCode As String = CodeRush.CodeMod.GenerateCode(LanguageElement, True)
        Dim CodeBytes As Byte() = Encoding.ASCII.GetBytes(TheCode)
        Dim TheHashBytes As Byte() = MD5.ComputeHash(CodeBytes)
        Return Encoding.ASCII.GetString(MD5.Hash)
    End Function
#End Region
    Private Sub FindDuplicateMethods_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles FindDuplicateMethods.Execute
        Dim ThisMethod = CodeRush.Source.ActiveMethod
        Dim Hash = ContentHash(ThisMethod)
        Dim Matches = New List(Of Method)
        For Each Method In CodeRush.Source.ActiveClass.Nodes.OfType(Of Method)()
            If Not Method Is ThisMethod _
            AndAlso Hash.EqualsOtherHashList(ContentHash((Method))) Then
                Matches.Add(Method)
            End If
        Next
        MsgBox(Matches.Count)
    End Sub
End Class

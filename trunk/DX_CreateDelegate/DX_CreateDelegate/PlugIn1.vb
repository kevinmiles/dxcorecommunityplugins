Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class PlugIn1

	'DXCore-generated code...
#Region " InitializePlugIn "
	Public Overrides Sub InitializePlugIn()
		MyBase.InitializePlugIn()
        CreateCreateDelegate()
		'TODO: Add your initialization code here.
	End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region
    ' Please ensure the following line is not missing from your plugin's InitializeComponent
    ' components = New System.ComponentModel.Container()
    Public Sub CreateCreateDelegate()
        Dim CreateDelegate As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(CreateDelegate, System.ComponentModel.ISupportInitialize).BeginInit()
        CreateDelegate.ProviderName = "CreateDelegate" ' Should be Unique
        CreateDelegate.DisplayName = "Create Delegate"
        AddHandler CreateDelegate.CheckAvailability, AddressOf CreateDelegate_CheckAvailability
        AddHandler CreateDelegate.Apply, AddressOf CreateDelegate_Execute
        CType(CreateDelegate, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub CreateDelegate_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ' This method is executed when the system checks the availability of your Code.
        Dim SourceMethod = TryCast(ea.CodeActive, Method)
        If SourceMethod Is Nothing Then
            Exit Sub
        End If
        ea.Available = True ' Change this to return true, only when your Code should be available.
    End Sub
    Private Sub CreateDelegate_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        ' This method is executed when the system executes your Code 
        Dim SourceMethod = TryCast(ea.CodeActive, Method)
        Dim D As New StructuralParser.DelegateDefinition

        D.Visibility = SourceMethod.Visibility
        D.Name = SourceMethod.Name & "Delegate"
        D.Parameters.AddRange(SourceMethod.Parameters.DeepClone)
        D.MemberType = SourceMethod.MemberType

        ' Generate Code
        Dim DelegateCode As String = CodeRush.CodeMod.GenerateCode(D, False)
        ' Insert Delegate Code
        ea.TextDocument.InsertText(SourceMethod.Range.Start, DelegateCode)
    End Sub

End Class

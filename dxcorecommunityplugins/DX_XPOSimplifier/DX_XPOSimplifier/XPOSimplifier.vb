Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class XPOSimplifier

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

    ' Please ensure the following line is not missing from your plugin's InitializeComponent
    ' components = New System.ComponentModel.Container()
    Public Sub CreateXPOSimplifier()
        Dim XPOSimplifier As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(XPOSimplifier, System.ComponentModel.ISupportInitialize).BeginInit()
        XPOSimplifier.ProviderName = "XPOSimplifier" ' Should be Unique
        XPOSimplifier.DisplayName = "Manual XPO Simplified Criteria Plugin"
        AddHandler XPOSimplifier.CheckAvailability, AddressOf XPOSimplifier_CheckAvailability
        AddHandler XPOSimplifier.Apply, AddressOf XPOSimplifier_Execute
        CType(XPOSimplifier, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub XPOSimplifier_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        ' This method is executed when the system checks the availability of your Code.
        If ea.CodeActive.ElementType = LanguageElementType.Property Then
            ea.Available = True
        End If
        ' Change this to return true, only when your Code should be available.
    End Sub
    Private Sub XPOSimplifier_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        ' This method is executed when the system executes your Code 
        If CodeRush.Source.ActiveClass IsNot Nothing Then
            Dim FieldClass As LanguageElement = Nothing
            Dim Searcher As New ElementEnumerable(CodeRush.Source.ActiveClass, GetType([Class]), True)
            Dim element As IEnumerator(Of LanguageElement) = Searcher.GetEnumerator
            element.Reset()
            While element.MoveNext
                If element.Current.ClassName = "FieldClass" Then
                    FieldClass = element.Current
                    Exit While
                End If
            End While



            CodeRush.Source.ActiveClass.FindChildByElementType(LanguageElementType.Class)
        End If
    End Sub


End Class

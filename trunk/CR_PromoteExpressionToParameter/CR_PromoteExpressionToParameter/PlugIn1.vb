Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.Refactor

Public Class PlugIn1
    Private Const PromoteExpressiontoParameter_DisplayName As String = "Promote (Expression) to Parameter"

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        CreatePromoteExpressionToParameter()
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Public Sub CreatePromoteExpressionToParameter()
        Dim PromoteExpressionToParameter As New DevExpress.Refactor.Core.RefactoringProvider(components)
        CType(PromoteExpressionToParameter, System.ComponentModel.ISupportInitialize).BeginInit()
        PromoteExpressionToParameter.ProviderName = "PromoteExpressionToParameter" ' Should be Unique
        PromoteExpressionToParameter.DisplayName = PromoteExpressiontoParameter_DisplayName
        AddHandler PromoteExpressionToParameter.CheckAvailability, AddressOf PromoteExpressionToParameter_CheckAvailability
        AddHandler PromoteExpressionToParameter.Apply, AddressOf PromoteExpressionToParameter_Execute
        CType(PromoteExpressionToParameter, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
    Private Sub PromoteExpressionToParameter_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        Dim DeclareLocalProvider As RefactoringProviderBase = CodeRush.Refactoring.Get("Introduce Local")
        If DeclareLocalProvider Is Nothing Then
            Return
        End If
        ea.Available = DeclareLocalProvider.IsAvailable
    End Sub
    Private Sub PromoteExpressionToParameter_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        Dim cpDeclareLocal As RefactoringProviderBase = CodeRush.Refactoring.Get("Introduce Local")
        If cpDeclareLocal Is Nothing Then
            Return
        End If

        ' The following two lines of code are a workaround of some bug (fixed in the 10.2.5+ version)...
        CodeRush.SmartTags.HidePopupMenu()
        CodeRush.Refactoring.MenuDeactivated()

        CodeRush.SmartTags.UpdateContext()
        If cpDeclareLocal.IsAvailable Then
            Dim compoundAction = CodeRush.TextBuffers.NewMultiFileCompoundAction(PromoteExpressiontoParameter_DisplayName, True)
            Try
                cpDeclareLocal.IsNestedProvider = True
                cpDeclareLocal.Execute()
                Dim rpPromoteToParameter = CodeRush.Refactoring.Get("Promote to Parameter")
                ea.TextDocument.ParseIfTextChanged()
                'Dim ERE As ElementReferenceExpression = TryCast(CodeRush.Source.Active, ElementReferenceExpression)
                'If ERE IsNot Nothing Then
                '    Dim Declaration = TryCast(SourceModelUtils.GetDeclaration(CodeRush.Source.Active), InitializedVariable)
                '    If Declaration IsNot Nothing Then
                '        CodeRush.Caret.MoveTo(Declaration.NameRange.Start)
                '    End If
                'End If
                CodeRush.SmartTags.UpdateContext()
                If rpPromoteToParameter.IsAvailable Then
                    Try
                        rpPromoteToParameter.IsNestedProvider = True
                        rpPromoteToParameter.Execute()
                    Finally
                        rpPromoteToParameter.IsNestedProvider = False
                    End Try
                End If
            Catch e As Exception
                Throw e
            Finally
                cpDeclareLocal.IsNestedProvider = False
                compoundAction.Close()
            End Try
        End If
    End Sub

    'Private Sub MethodName()
    '    Dim SomeLongStringName As String = "fred"

    '    Dim NewVariable As String = SomeLongStringName & "1"
    '    Dim Y As String = NewVariable

    'End Sub
End Class

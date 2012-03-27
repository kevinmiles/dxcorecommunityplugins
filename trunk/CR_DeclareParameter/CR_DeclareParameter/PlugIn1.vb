Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Public Class PlugIn1
#Region "Constants"
    Private Const DeclareParameter_DisplayName As String = "Declare Parameter"
#End Region
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        CreateDeclareParameter()
    End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region

#Region "CreateDeclareParameter"
    Public Sub CreateDeclareParameter()
        Dim DeclareParameter As New DevExpress.CodeRush.Core.CodeProvider(components)
        CType(DeclareParameter, System.ComponentModel.ISupportInitialize).BeginInit()
        DeclareParameter.ProviderName = "DeclareParameter" ' Should be Unique
        DeclareParameter.DisplayName = "Declare Parameter"
        AddHandler DeclareParameter.CheckAvailability, AddressOf DeclareParameter_CheckAvailability
        AddHandler DeclareParameter.Apply, AddressOf DeclareParameter_Execute
        CType(DeclareParameter, System.ComponentModel.ISupportInitialize).EndInit()
    End Sub
#End Region
#Region "DeclareParameter_CheckAvailability"
    Private Sub DeclareParameter_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        Dim DeclareLocalProvider As RefactoringProviderBase = CodeRush.CodeMod.Get("Declare Local")
        If DeclareLocalProvider Is Nothing Then
            Return
        End If

        ' Remove once supported by DXCore.
        Dim Expression = TryCast(CodeRush.Source.Active, Expression)
        Dim Method As Method = CodeRush.Source.ActiveMethod
        If Method IsNot Nothing AndAlso ReferencesLocals(Expression, GetLocals(Method)) Then
            Return
        End If
        ea.Available = DeclareLocalProvider.IsAvailable
    End Sub
#End Region
#Region "DeclareParameter_Execute"
    Private Sub DeclareParameter_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)
        Dim cpDeclareLocal As RefactoringProviderBase = CodeRush.CodeMod.Get("Declare Local")
        If cpDeclareLocal Is Nothing Then
            Return
        End If

        ' The following two lines of code are a workaround of some bug (fixed in the 10.2.5+ version)...
        CodeRush.SmartTags.HidePopupMenu()
        CodeRush.Refactoring.MenuDeactivated()

        CodeRush.SmartTags.UpdateContext()
        If cpDeclareLocal.IsAvailable Then
            Dim compoundAction = CodeRush.TextBuffers.NewMultiFileCompoundAction(DeclareParameter_DisplayName, True)
            Try
                cpDeclareLocal.IsNestedProvider = True
                cpDeclareLocal.Execute()
                Dim rpPromoteToParameter = CodeRush.Refactoring.Get("Promote to Parameter")
                'CodeRush.Source.Active
                ea.TextDocument.ParseIfTextChanged()
                Dim ERE As ElementReferenceExpression = TryCast(CodeRush.Source.Active, ElementReferenceExpression)
                If ERE IsNot Nothing Then
                    Dim Declaration = TryCast(SourceModelUtils.GetDeclaration(CodeRush.Source.Active), InitializedVariable)
                    If Declaration IsNot Nothing Then
                        CodeRush.Caret.MoveTo(Declaration.NameRange.Start)
                    End If
                End If
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
#End Region

#Region "Helper Methods"
    Private Shared Function GetLocals(ByVal Method As Method) As IEnumerable(Of Variable)
        Dim Locals As New List(Of Variable)
        For Each Variable As Variable In Method.AllVariables
            If Variable.IsLocal Then
                Locals.Add(Variable)
            End If
        Next
        Return Locals
    End Function
    Private Function ReferencesLocals(ByVal Expression As Expression, ByVal Locals As IEnumerable(Of Variable)) As Boolean
        Dim ElementExpression = TryCast(Expression, ElementReferenceExpression)
        If ElementExpression IsNot Nothing Then
            ' Direct Reference
            Dim Variable = TryCast(ElementExpression.GetDeclaration(True), BaseVariable)
            If Variable IsNot Nothing AndAlso Variable.IsLocal Then
                Return True
            End If
        End If
        For Each Node In Expression.Nodes
            ' Sub Expression References
            Dim SubExpression = TryCast(Node, Expression)
            If SubExpression IsNot Nothing AndAlso ReferencesLocals(SubExpression, Locals) Then
                Return True
            End If
        Next
        For Each Node In Expression.DetailNodes
            ' Sub Expression References
            Dim SubExpression = TryCast(Node, Expression)
            If SubExpression IsNot Nothing AndAlso ReferencesLocals(SubExpression, Locals) Then
                Return True
            End If
        Next
        Return False
    End Function
#End Region

End Class

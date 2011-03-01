Imports System.ComponentModel
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser

Public Class PlugIn1

	'DXCore-generated code...
#Region " InitializePlugIn "
	Public Overrides Sub InitializePlugIn()
		MyBase.InitializePlugIn()
        CreateConvertToTryParse()
		'TODO: Add your initialization code here.
	End Sub
#End Region
#Region " FinalizePlugIn "
	Public Overrides Sub FinalizePlugIn()
		'TODO: Add your finalization code here.

		MyBase.FinalizePlugIn()
	End Sub
#End Region

    Public Sub CreateConvertToTryParse()
        Dim ConvertToTryParse As New DevExpress.Refactor.Core.RefactoringProvider(components)
        CType(ConvertToTryParse, ISupportInitialize).BeginInit()
        ConvertToTryParse.ProviderName = "ConvertToTryParse" ' Should be Unique
        ConvertToTryParse.DisplayName = "Convert to TryParse"
        AddHandler ConvertToTryParse.CheckAvailability, AddressOf ConvertToTryParse_CheckAvailability
        AddHandler ConvertToTryParse.Apply, AddressOf ConvertToTryParse_Execute
        CType(ConvertToTryParse, ISupportInitialize).EndInit()
    End Sub
    Private Sub ConvertToTryParse_CheckAvailability(ByVal sender As Object, ByVal ea As CheckContentAvailabilityEventArgs)
        Dim InitializedVar = TryCast(ea.CodeActive, InitializedVariable)
        If InitializedVar Is Nothing Then
            Exit Sub
        End If
        ' Require Assignment is to Int32
        If Not InitializedVar.MemberTypeReference.Is("System.Int32") Then
            Exit Sub
        End If

        Dim MCE = TryCast(InitializedVar.Expression, MethodCallExpression)
        If MCE Is Nothing Then
            Exit Sub
        End If

        ' Require MethodCall is to Int32.Parse
        If Not MCE.GetDeclaration.FullName = "System.Int32.Parse" Then
            Exit Sub
        End If
        ea.Available = True ' Change this to return true, only when your refactoring should be available.
    End Sub
    Private Sub ConvertToTryParse_Execute(ByVal Sender As Object, ByVal ea As ApplyContentEventArgs)

        Dim Doc = CodeRush.Documents.ActiveTextDocument
        Using Doc.NewCompoundAction("Convert to Tryparse")
            Dim InitializedVar = TryCast(ea.CodeActive, InitializedVariable)
            Dim Builder = New ElementBuilder
            Dim Range = InitializedVar.Range
            Dim MethodCall = TryCast(InitializedVar.Expression, MethodCallExpression)
            Dim Parent = ea.CodeActive.GetParentMethodOrAccessor
            Dim NewVariable = Builder.AddVariable(Parent, "System.Int32", InitializedVar.Name)
            Dim NewMethodCall = CreateMethodCall("Int32.TryParse", _
                                                 MethodCall.Arguments(0), _
                                                 GetOutArgument(InitializedVar))
            Dim NewInitVar = Builder.AddInitializedVariable(Parent, _
                                   "System.Boolean", _
                                   "Success", _
                                   NewMethodCall)


            Dim TryParseCode As String = CodeRush.CodeMod.GenerateCode(NewVariable, False) _
                                       & CodeRush.CodeMod.GenerateCode(NewInitVar)


            Doc.Format(Doc.SetText(Range, TryParseCode))
        End Using




    End Sub
    Private Shared Function CreateMethodCall(ByVal MethodName As String, _
                                             ByVal ParamArray Arguments() As Expression) As MethodCallExpression
        Dim Args As New ExpressionCollection()
        For Each item In Arguments
            Args.Add(item)
        Next
        Return (New ElementBuilder).BuildMethodCallExpression(MethodName, Args)
    End Function
    Private Shared Function GetOutArgument(ByVal InitializedVar As InitializedVariable) As ArgumentDirectionExpression
        Return New ArgumentDirectionExpression(ArgumentDirection.Out, New ElementReferenceExpression(InitializedVar.Name))
    End Function

End Class

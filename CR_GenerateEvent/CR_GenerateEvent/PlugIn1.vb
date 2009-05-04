Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports System.Runtime.CompilerServices

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

    Private Sub AddEventHandler_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs) Handles AddEventHandler.CheckAvailability
        If Not (ea.ActiveLanguage = DevExpress.DXCore.Constants.Str.Language.CSharp) Then
            ' We are not writing C#.
            Return ' Implicit ea.Available = False
        End If
        Dim Assignment = TryCast(ea.Element, Assignment)
        If Assignment Is Nothing Then
            Return ' Our Caret is not placed on an assignment node.
        End If
        If Not Assignment.Expression Is Nothing Then
            Return ' This Assignment already has a right hand side.
        End If
        Dim EventDec = Assignment.LeftSide.GetDeclaration().AsIEventElement
        If EventDec Is Nothing Then
            Return ' The left hand side is not an Event.
        End If
        ea.Available = True
    End Sub
    Private Sub AddEventHandler_Apply(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.Core.ApplyContentEventArgs) Handles AddEventHandler.Apply
        CodeRush.Source.ParseIfTextChanged()
        Dim InsertionPoint = StartOfLine(CodeRush.Source.ActiveMethod.Range.End, 1)
        ' Locate Event Element
        Dim Assignment As Assignment = ea.Element.AsAssignment()
        Dim EventReference As LanguageElement = Assignment.LeftSide
        Dim MethodName = TryCast(EventReference, QualifiedElementReference).FullSignature.Replace(".", "_")
        Dim Range As SourceRange = Assignment.Range
        ' Get the declaration of the event
        Dim EventDec As IEventElement = EventReference.GetDeclaration.AsIEventElement
        ' Generate a Method Name from the Event if possible
        Assignment.Expression = New ElementReferenceExpression(MethodName)
        Dim ActiveDoc As TextDocument = CodeRush.Documents.ActiveTextDocument
        ActiveDoc.Replace(Range, Assignment.GenerateCode, "")
        ' Assemble Method
        Dim NewMethod As Method = GenerateHandlerStubMethod(MethodName, EventDec)
        ' Insert the code at the insertionpoint picked
        Dim Range2 As SourceRange = ActiveDoc.ExpandText(InsertionPoint, ControlChars.CrLf & NewMethod.GenerateCode & ControlChars.CrLf)
        Call ActiveDoc.Format(Range2)
    End Sub
    Public Function GenerateHandlerStubMethod(ByVal MethodName As String, ByVal EventDec As IEventElement) As Method
        ' Create Method
        Dim Method As New Method(MethodName)
        Method.MethodType = MethodTypeEnum.Void
        Method.Visibility = MemberVisibility.Private
        ' Get Delegate Declaration
        Dim TheDelegate As IDelegateElement = CType(EventDec.Type.GetDeclaration, IDelegateElement)
        ' Create Method Params
        For Each Param As IParameterElement In TheDelegate.Parameters
            Dim NewParam As New Param(Param.Type.FullSignature, Param.Name)
            NewParam.Direction = Param.Direction
            Method.Parameters.Add(NewParam)
        Next
        ' Add Method Body
        Dim TheThrow = BuildThrow("System.NotImplementedException").GenerateCode
        Method.AddNode(New SnippetCodeElement("«Caret»" & TheThrow & "«BlockAnchor»" & ControlChars.CrLf))
        Return Method
    End Function
    Private Function BuildThrow(ByVal ExceptionType As String) As SP.Throw
        Return (New ElementBuilder).BuildThrow(New ObjectCreationExpression(New TypeReferenceExpression(ExceptionType)))
    End Function
    Private Function StartOfLine(ByVal Point As SourcePoint, Optional ByVal LineOffset As Integer = 0) As SourcePoint
        Return New SourcePoint(Point.Line + LineOffset, 1)
    End Function
End Class
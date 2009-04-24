Option Strict On
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports Sp = DevExpress.CodeRush.StructuralParser
Imports Microsoft.VisualBasic
Imports System.Reflection
Imports System.Runtime.Serialization
Public Module PluginLogic
#Region "Utils"
    Private Function GetMethodName(ByVal le As LanguageElement, _
                                   ByVal DefaultName As String, _
                                   ByVal completeStatement As Boolean) As String
        Dim objectname As String
        'get the name of the class the event needs to be handled to use this name to create a methodname
        objectname = GetObjectName(le)
        ' default the name of the method stub to create is the name of the element
        Dim methodName As String = DefaultName
        'when we are at the event and the action is executed we need to know the name of the class so we prompt the user for a method name
        If completeStatement Then
            If objectname = "" Then
                methodName = InputBox("Name of the new method:", "")
                If methodName = "" Then
                    Return methodName
                End If
            Else
                methodName = objectname & "_" & le.Name
            End If
        End If
        Return methodName
    End Function
    Private Function GetObjectName(ByVal le As LanguageElement) As String
        For Each theNode As LanguageElement In le.Nodes
            If TypeOf theNode Is ElementReferenceExpression Then
                Return theNode.Name
            ElseIf TypeOf theNode Is ThisReferenceExpression Then
                Return theNode.GetClass.Name
            End If
            Return GetObjectName(theNode)
        Next
        ' TODO: Correct possability of funtion returning nothing.
        ' Should function return String.Empty or "Unknown"?
        ' Added "return Nothing" to represent what would actually happen here.
        Return Nothing
    End Function
    Private Function GenerateWithoutCRLF(ByVal LanguageElement As LanguageElement) As String
        Dim RawCode As String = CodeRush.Language.GenerateElement(LanguageElement)
        Return RawCode.Substring(0, RawCode.Length - ControlChars.CrLf.Length)
    End Function
    Public Function GetThrowStatement(ByVal ExceptionType As String) As Sp.Throw
        Dim TheThrow As New Sp.Throw
        TheThrow.Expression = New ObjectCreationExpression(New TypeReferenceExpression(ExceptionType))
        Return TheThrow
    End Function

    Private Function GetNearEvent(ByVal CaretElement As LanguageElement) As LanguageElement
        ' Depending on where the caret is, we are at the event node or the method node
        Dim [Event] As LanguageElement = CaretElement
        If Not TypeOf CaretElement Is QualifiedElementReference Then
            [Event] = CaretElement.Parent.PreviousCodeSibling
        End If
        Return [Event]
    End Function

    Private Sub EnsureAddressOfClause(ByVal CompleteStatement As Boolean, ByVal MethodName As String)
        If CompleteStatement Then
            CodeRush.Documents.ActiveTextDocument.ExpandText(CodeRush.Caret.SourcePoint, ", Addressof " & MethodName)
        End If
    End Sub
#End Region
    Public Sub CreateMethodStub(ByVal CaretElement As LanguageElement, _
                                ByVal InsertionPoint As SourcePoint, _
                                ByVal completeStatement As Boolean)
        ' Parse if the source is chaged
        CodeRush.Source.ParseIfTextChanged()
        ' Locate Event Element
        Dim NearEventElement As LanguageElement = GetNearEvent(CaretElement)
        ' Get the declaration of the event
        Dim EventDec As IEventElement = CType(NearEventElement.GetDeclaration, IEventElement)
        ' Generate a Method Name from the Event if possible
        Dim MethodName As String = GetMethodName(NearEventElement, CaretElement.Name, completeStatement)
        ' Assemble Method
        Dim NewMethod As Method = GenerateHandlerStubMethod(MethodName, EventDec)
        ' Generate Code for Method
        Dim MethodCode As String = CodeRush.Language.GenerateElement(NewMethod)
        EnsureAddressOfClause(completeStatement, MethodName)
        ' Insert the code at the insertionpoint picked
        CodeRush.Documents.ActiveTextDocument.ExpandText(InsertionPoint, ControlChars.CrLf & MethodCode)
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
        Dim Exc As String = "System.NotImplementedException"
        Dim ThrowStatement As [Throw] = GetThrowStatement(Exc)
        Dim ThrowWithoutCRLF As String = GenerateWithoutCRLF(ThrowStatement)
        Dim NewSnippetCodeElement As New SnippetCodeElement("«Caret»" & ThrowWithoutCRLF & "«BlockAnchor»" & ControlChars.CrLf)
        Method.AddNode(NewSnippetCodeElement)
        Return Method
    End Function
End Module

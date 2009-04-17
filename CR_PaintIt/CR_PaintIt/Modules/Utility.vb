Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Diagnostics
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.UserControls
Imports SP = DevExpress.CodeRush.StructuralParser
Imports ENV = System.Environment
Public Module Utility
#Region "Caret Funcs..."
    Public Function CaretOnType(ByVal ParamArray ElementTypes() As LanguageElementType) As Boolean
        Return ElementInTypes(ElementAtCaret, ElementTypes)
    End Function
    Public Function CaretInRange(ByVal Range As SourceRange) As Boolean
        Return Range.Surrounds(CodeRush.Caret.SourcePoint) OrElse PointsAreSame(Range.Start, CodeRush.Caret.SourcePoint)
    End Function
    Public Function CaretOnFirstLine(ByVal Element As LanguageElement) As Boolean
        Return CodeRush.Caret.SourcePoint.Line = Element.NameRange.Start.Line
    End Function
#End Region
#Region "ElementFuncs"
    Public Function ElementAtCaret() As DevExpress.CodeRush.StructuralParser.LanguageElement
        Return TextDocument.Active.GetNodeAt(CodeRush.Caret.SourcePoint)
    End Function
    Public Function ElementAtCaretIfType(ByVal ParamArray ElementTypes() As LanguageElementType) As LanguageElement
        Dim Selected As LanguageElement = ElementAtCaret()
        If ElementInTypes(Selected, ElementTypes) Then
            Return Selected
        End If
        Return Nothing
    End Function
    Public Function ElementInTypes(ByVal Element As LanguageElement, ByVal ParamArray ElementTypes() As LanguageElementType) As Boolean
        If Element Is Nothing Then
            Return False
        End If
        For Each ElementType As LanguageElementType In ElementTypes
            If Element.ElementType = ElementType Then
                Return True
            End If
        Next
    End Function
    Public Function HasAncestor(ByVal Element As LanguageElement, ByVal Type As LanguageElementType, ByRef Ancestor As LanguageElement) As Boolean
        Do Until Element.ElementType = Type OrElse Element.Parent Is Nothing
            Element = Element.Parent
        Loop
        If Element.ElementType = Type Then
            Ancestor = Element
            Return True
        Else
            Return False
        End If
    End Function
    Public Function HasAncestor(ByVal Element As LanguageElement, ByVal Type As Type, ByRef Ancestor As LanguageElement) As Boolean
        Do Until Element.GetType.IsSubclassOf(Type) _
            OrElse Element.Parent Is Nothing
            Element = Element.Parent
        Loop
        If Element.GetType.IsSubclassOf(Type) Then
            Ancestor = Element
            Return True
        Else
            Return False
        End If
    End Function
#End Region
    Public Function isSubclassofType(ByVal Obj As Object, ByVal ParamArray Types() As Type) As Boolean
        For Each Type As Type In Types
            If Obj.GetType.IsSubclassOf(Type) Then
                Return True
            End If
        Next
    End Function
    Public Function BoolToAvail(ByVal BooleanValue As Boolean) As RefactoringAvailability
        If BooleanValue Then
            Return RefactoringAvailability.Available
        Else
            Return RefactoringAvailability.NotAvailable
        End If
    End Function
    Public Function PointsAreSame(ByVal Point1 As SourcePoint, ByVal Point2 As SourcePoint) As Boolean
        Return Point1.Line = Point2.Line _
        AndAlso Point1.Offset = Point2.Offset
    End Function
    Public Function GetNamespaceReferences(ByVal File As SourceFile) As LanguageElementCollection
        Dim nsc As New LanguageElementCollection
        For Each LE As LanguageElement In File.Nodes
            Select Case LE.ElementType
                Case LanguageElementType.NamespaceReference
                    nsc.Add(LE)
                Case LanguageElementType.Class
                    Return nsc
                Case Else
            End Select
        Next
        Return nsc ' Return after we have been through everything
    End Function

End Module

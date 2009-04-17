Option Strict On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Drawing
' Miscellanious routines designed to make code more readable.
' As soon as Unused Variables is working properly, I'm gonna run it here first :)
Public Module Elements
    Public Delegate Function LanguageElementEvaluatorDelegate(ByVal Element As LanguageElement) As Boolean
#Region "Convert..."
    Public Function RectToRectF(ByVal Rect As Rectangle) As RectangleF
        Return New RectangleF(Rect.Left, Rect.Top, Rect.Width, Rect.Height)
    End Function
#End Region
#Region "Equal..."
    Public Function ElementsAreSame(ByVal Element1 As LanguageElement, ByVal Element2 As LanguageElement) As Boolean
        If Element1.Name <> Element2.Name Then
            Return False
        End If
        If Not Element1.SameStartPoint(Element2) Then
            Return False
        End If
        If Not Element1.SameEndPoint(Element2) Then
            Return False
        End If
        Return True
    End Function
#End Region
#Region "Get..."
    Public Function GetNearestNodeAfterPoint(ByVal point As SourcePoint) As LanguageElement
        If (point.IsEmpty) Then
            Return Nothing
        End If
        Dim Start As LanguageElement = CodeRush.Source.GetNodeAt(point)
        If Start Is Nothing OrElse Start.Range.Start.Equals(point) Then
            Return Start
        End If
        Dim ChildAfter As LanguageElement = Start.GetChildAfter(point)
        If Not (ChildAfter Is Nothing) Then
            Return ChildAfter
        Else
            Return Start
        End If
    End Function
    Public Function GetNearestNodeAfterCaret() As LanguageElement
        Dim caret As SourcePoint = CodeRush.TextViews.Active.Caret.SourcePoint
        Return GetNearestNodeAfterPoint(caret)
    End Function
    Public Function GetFirstNodeInLine(ByVal Line As Integer) As LanguageElement
        Return GetNearestNodeAfterPoint(New SourcePoint(Line, 1))
    End Function
    Public Function GetFirstNodeInFile() As LanguageElement
        Return GetFirstNodeInFile(CodeRush.Documents.ActiveTextDocument)
    End Function
    Public Function GetFirstNodeInFile(ByVal Document As TextDocument) As LanguageElement
        Return Document.FileNode.GetChildAfter(New SourcePoint(1, 1))
    End Function
    Public Function GetActiveSourceFile() As SourceFile
        Return CodeRush.Source.GetFileNode(CodeRush.Documents.ActiveTextDocument).GetSourceFile
    End Function
#End Region
#Region "Is..."
    Public Function isUndeclaredERE(ByVal Element As LanguageElement) As Boolean
        Return isUndeclaredX(Element, LanguageElementType.ElementReferenceExpression)
    End Function
    Public Function isUndeclaredX(ByVal Element As LanguageElement, ByVal ElementType As LanguageElementType) As Boolean
        If isUndeclared(Element) AndAlso Element.ElementType = ElementType Then
            Return True
        End If
    End Function
    Public Function isUndeclared(ByVal Element As LanguageElement) As Boolean
        If Element Is Nothing Then
            Return False
        End If
        Return Element.GetDeclaration Is Nothing
    End Function
    Public Function isMethodParam(ByVal LE As LanguageElement) As Boolean
        If Not LE.ElementType = LanguageElementType.Parameter Then
            Return False
        End If
        Dim Param As Param = CType(LE, Param)
        If Param.Parent.ElementType <> LanguageElementType.Method Then
            Return False
        End If
        Return True
    End Function
    Public Function isLocalVariable(ByVal LE As LanguageElement) As Boolean
        If Not LE.ElementType = LanguageElementType.Variable Then
            Return False
        End If
        ' Needs to be in a method
        If Not LE.Parent.ElementType = LanguageElementType.Method Then
            Return False
        End If
        Return True
    End Function
    Public Function isVarReference(ByVal LE As LanguageElement) As Boolean
        If LE.ElementType = LanguageElementType.TypeReferenceExpression Then
            Return True
        End If
    End Function
    Public Function isAbstractMethod(ByVal LE As LanguageElement) As Boolean
        If LE Is Nothing Then
            Return False
        End If
        Return isMethodDeclaration(LE) _
            AndAlso CType(LE, Method).IsAbstract
    End Function
    Public Function isMethodDeclaration(ByVal LE As LanguageElement) As Boolean
        If LE Is Nothing Then
            Return False
        End If
        Return LE.ElementType = LanguageElementType.Method
    End Function
    Public Function isDelegateParam(ByVal LE As LanguageElement) As Boolean
        If LE Is Nothing Then
            Return False
        End If
        If LE.Parent Is Nothing Then
            Return False
        End If
        If LE.Parent.ElementType = LanguageElementType.Delegate Then
            Return True
        End If
        Return False
    End Function
    Public Function isVariableDeclaration(ByVal LE As LanguageElement) As Boolean
        If LE Is Nothing Then
            Return False
        End If
        Return isElementType(LE, ElementTypes.VarDeclarationTypes)
    End Function
    Public Function isInterface(ByVal LE As LanguageElement) As Boolean
        If LE Is Nothing Then
            Return False
        End If
        Return LE.ElementType = LanguageElementType.Interface
    End Function

    Public Function isPrivateOrLocal(ByVal Member As IMemberElement) As Boolean
        If Member Is Nothing Then
            Return False
        End If
        Return Member.Visibility = MemberVisibility.Local _
            OrElse Member.Visibility = MemberVisibility.Private
    End Function
    Public Function isElementType(ByVal Element As IElement, ByVal ParamArray ElementTypes() As LanguageElementType) As Boolean
        For Each ElementType As LanguageElementType In ElementTypes
            If Element.ElementType = ElementType Then
                Return True
            End If
        Next
    End Function
#End Region

End Module

Option Strict On
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Interop.OLE.Helpers
Imports System.Collections

<DesignerCategory("")> <ToolboxItem(True)> _
Public Class TryCatchGenerator
	Implements IComponent


#Region "Constructors"
	''' <summary>
	''' called by the component design surface
	''' </summary>
	''' <param name="container">contianer for disposal linking</param>
	''' <remarks></remarks>
	Public Sub New(ByVal container As System.ComponentModel.IContainer)
		MyClass.New()
		If container IsNot Nothing Then container.Add(Me)
	End Sub

	Public Sub New()
		MyBase.New()
	End Sub

#End Region

#Region "Try Catch wrapping methods"
    ''' <summary>
    ''' Inserts a Try Catch block around the textDocument active Selection 
    ''' </summary>
    ''' <param name="handlers">set of exceptions to provide catch blocks for.</param>
    ''' <remarks></remarks>
	Friend Sub WrapSelectionInTryCatch(ByVal handlers As IEnumerable(Of ExceptionHandler))

        Dim ActiveDoc = CodeRush.Documents.ActiveTextDocument

        Dim selectionStartLine = CodeRush.TextViews.Active.Selection.Range.Top.Line
        Dim startLine = ActiveDoc.GetLine(selectionStartLine)
        Dim leadingWhiteSpace = CodeRush.StrUtil.GetLeadingWhiteSpace(startLine)

        Dim code = WrapInTryCatch(CodeRush.TextViews.Active.Selection.Text, leadingWhiteSpace, handlers)

        Dim sr = ActiveDoc.ActiveView.Selection.Range
        If Not sr.StartPrecedesEnd Then 'swap the start and end to ensure correct replace
            sr = New SourceRange(sr.End, sr.Start)
        End If

        Using CompoundAction = ActiveDoc.NewCompoundAction("Add Exception Handlers", True)
            ActiveDoc.DeleteText(sr)
            ' Using ExpandText to expand "«Caret»" and "«Marker»", added by WrapInTryCatch - Mark.
            Dim insertedTextRange = ActiveDoc.ExpandText(sr.Start, code)
            ActiveDoc.Format(insertedTextRange)
        End Using
    End Sub


    ''' <summary>
    ''' Wraps the given text in a Try Catch block
    ''' </summary>
    ''' <param name="selectionText">the text to wrap in the Try Catch clock</param>
    ''' <param name="handlers">set of exceptions to provide Catch blocks for</param>
    ''' <returns>the text wrapped in the Try Catch</returns>
    ''' <remarks></remarks>
	Friend Function WrapInTryCatch(ByVal selectionText As String, ByVal leadingWhiteSpace As String, ByVal handlers As IEnumerable(Of ExceptionHandler)) As String
        Dim listOfHandlers As List(Of ExceptionHandler) = SortExceptions(handlers)
        Dim lang As LanguageServices = CodeRush.Language
        Dim eb As ElementBuilder = lang.GetActiveElementBuilder
        Dim tryElem As LanguageElement = eb.AddTry(Nothing)

        eb.AddSnippetCodeElement(tryElem, selectionText)
        For Each item As ExceptionHandler In listOfHandlers
            Dim ce As [Catch] = eb.AddCatch(Nothing, item.ExceptionName, "ex")
            Dim throwersAlreadyDocumented As Hashtable = New Hashtable()     ' Make sure we document multiple callers only once.
            For Each exMethInfo As ExceptionInfo In item.ExceptionMethodInfos
                Dim exceptionThrower As String = exMethInfo.TypeName
                If Not throwersAlreadyDocumented.ContainsKey(exceptionThrower) Then
                    throwersAlreadyDocumented.Add(exceptionThrower, Nothing)

                    'AddComment(ce, String.Format("Thrown from {0}.", exceptionThrower))
                    ' TODO: Convert <cref> and other XML doc tags in exMethInfo.Description to plain text. -- Suggested by Mark.
                    'AddComment(ce, String.Format("Message: {0}.", exMethInfo.Description))

                    ' Use the technique above to add comments -- Mark

                    '				eb.AddStatement(ce, lang.GetComment(" thrown by : " & exMethInfo.TypeName).TrimEnd)
                    '				eb.AddStatement(ce, lang.GetComment(" description :" & exMethInfo.Description).TrimEnd)
                End If
            Next

            ' To add an empty line, use this code (the call to AddStatement with an empty string generated lines consisting of semi-colons in C# -- Mark.
            eb.AddSnippetCodeElement(ce, leadingWhiteSpace & "«Marker»" & Environment.NewLine)

            ce.AddNode(New [Throw])
            ' Use the code above (instead of the code below) to add a new throw statement. We'll add an AddThrow method to ElementBuilder as well -- Mark.
            'eb.AddStatement(ce, lang.CreateLanguageElement(LanguageElementType.Throw))
        Next

		Return eb.GenerateCode()

	End Function
    Private Sub AddComment(ByVal parentElement As LanguageElement, ByVal commentText As String)
        Dim eb As New ElementBuilder
        Dim CommentLines As List(Of String) = ConvertToMaxWidthParagraph(commentText, 80)
        For Each line As String In CommentLines
            ' eb.BuildComment(line, CommentType.SingleLine)
            parentElement.AddNode(New Comment() With {.Name = " " & line})
        Next
    End Sub
    Private Function ConvertToMaxWidthParagraph(ByVal Comment As String, ByVal MaxWidth As Integer) As List(Of String)
        Dim Segment As String
        Comment &= " "
        If Comment.Length <= MaxWidth AndAlso Not Comment.Contains(Chr(10)) Then
            Return New String() {Comment}.ToList
        End If
        Dim List As New List(Of String)
        For Each InnerComment As String In Comment.Split(Chr(10))
            If InnerComment.Trim <> String.Empty AndAlso InnerComment.Trim <> "." Then
                Do
                    Segment = InnerComment.Substring(0, Math.Min(MaxWidth, InnerComment.Length))
                    Segment = InnerComment.Substring(0, Segment.LastIndexOf(" "))

                    List.Add(Segment)
                    ' Reduce SourceComment 
                    InnerComment = InnerComment.Substring(Segment.Length)
                Loop Until InnerComment.Length < 10 OrElse Segment = String.Empty
            End If

        Next
        Return List
    End Function


#End Region


#Region "utility methods"


	''' <summary>
	''' Creates an ExceptionHandlerCollection based on a list of MethodExceptionInfos
	''' </summary>
	''' <param name="methods">The MethodExceptionInfos to combine</param>
	''' <returns>An ExceptionHandlerCollection</returns>
	''' <remarks></remarks>
	Friend Shared Function GetExceptionHandlerCollection(ByVal methods As IEnumerable(Of MethodExceptionInfo)) As ExceptionHandlerCollection
		Dim handlers As New ExceptionHandlerCollection
		For Each item As MethodExceptionInfo In methods
			For Each ex As ExceptionInfo In item.Exceptions
				handlers.Add(ex.TypeName, item.MethodDescriptor, item.NameRanges, ex.Description)
			Next
		Next
		Return handlers
	End Function


#End Region


#Region "(private) sorting of exceptions"

	'Private m_Exceptions As New Dictionary(Of String, Int32)

	Private Function SortExceptions(ByVal handlers As IEnumerable(Of ExceptionHandler)) As List(Of ExceptionHandler)
		Dim listOfHandlers As New List(Of ExceptionHandler)
		For Each item As ExceptionHandler In handlers
			listOfHandlers.Add(item)
		Next
		listOfHandlers.Sort(New Comparison(Of ExceptionHandler)(AddressOf HandlerComparer))
		' only cache exception depth per call per thread
		Me.ExceptionDepths.Clear()
		Return listOfHandlers
	End Function


	Private Function HandlerComparer(ByVal x As ExceptionHandler, ByVal y As ExceptionHandler) As Int32
		Return GetExceptionDepth(y).CompareTo(GetExceptionDepth(x))
	End Function


	Private Function GetExceptionDepth(ByVal ex As ExceptionHandler) As Int32
		Dim value As Int32 = 0
		If Me.ExceptionDepths.TryGetValue(ex.ExceptionName, value) Then Return value

		Dim typ As Type = Type.GetType(ex.ExceptionName, False, False)

		If typ Is Nothing Then
			For Each ass As AssemblyReference In CodeRush.Documents.Active.ProjectElement.AssemblyReferences
				Dim assembly As Reflection.Assembly = Reflection.Assembly.ReflectionOnlyLoadFrom(ass.FilePath)
				typ = assembly.GetType(ex.ExceptionName, False, False)
				If typ IsNot Nothing Then Exit For
			Next
		End If

		If typ Is Nothing Then
			' must be a live code declaration
			Dim el As ITypeElement = TryCast(CodeRush.Documents.Active.ProjectElement.FindElementByFullName(ex.ExceptionName, True, True), ITypeElement)
			While el IsNot Nothing
				el = el.GetBaseType
				value += 1
			End While

		Else
			Do While typ IsNot Nothing
				typ = typ.BaseType
				value += 1
			Loop
		End If

		Me.ExceptionDepths.Add(ex.ExceptionName, value)

		Return value
	End Function


	' thread safe storage of exceptions per call per thread
	Private m_ExceptionDepths As New My.MyProject.ThreadSafeObjectProvider(Of Dictionary(Of String, Int32))

	Private ReadOnly Property ExceptionDepths() As Dictionary(Of String, Int32)
		Get
			Return m_ExceptionDepths.GetInstance
		End Get
	End Property


#End Region


#Region "IComponent Interface"
	<System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId:="0#")> <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1725:ParameterNamesShouldMatchBaseDeclaration", MessageId:="0#")> _
	Public Event Disposed As EventHandler Implements System.ComponentModel.IComponent.Disposed

	Private m_Site As ISite

	Private Property Site() As System.ComponentModel.ISite Implements System.ComponentModel.IComponent.Site
		Get
			Return Me.m_Site
		End Get
		Set(ByVal value As System.ComponentModel.ISite)
			Me.m_Site = value
		End Set
	End Property

	Private disposedValue As Boolean		' To detect redundant calls

	' IDisposable
	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		If Not Me.disposedValue Then
			If disposing Then
				' free unmanaged resources when explicitly called
			End If

			' free shared unmanaged resources
		End If
		Me.disposedValue = True
		RaiseEvent Disposed(Me, EventArgs.Empty)
	End Sub

#End Region

#Region " IDisposable Support "

	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub
#End Region

End Class


Option Strict On
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser


''' <summary>
'''  uses a dictionary to store unique method and exception information per exception
''' </summary>
''' <remarks></remarks>
Friend Class ExceptionHandlerCollection
	Inherits Dictionary(Of String, ExceptionHandler)

	Public Overloads Sub Add(ByVal exceptionName As String, ByVal methodName As String, ByVal ranges As SourceRangeCollection, ByVal exceptionDescription As String)
		Dim exHandler As ExceptionHandler = Nothing
		If TryGetValue(exceptionName, exHandler) Then
			If exHandler.NameRanges IsNot ranges Then
				For Each range As SourceRange In ranges
					If exHandler.NameRanges.IndexOf(range) < 0 Then
						exHandler.NameRanges.Add(range)
					End If
				Next
			End If
		Else
			exHandler = New ExceptionHandler(exceptionName, ranges)
			Me.Add(exceptionName, exHandler)
		End If
		exHandler.ExceptionMethodInfos.Add(New ExceptionInfo(methodName, exceptionDescription))
	End Sub

End Class



Friend Class ExceptionHandler

	Public ExceptionName As String
	Public NameRanges As SourceRangeCollection

	Public Sub New(ByVal name As String, ByVal ranges As SourceRangeCollection)
		Me.ExceptionName = name
		Me.NameRanges = ranges
	End Sub


	Private m_ExceptionMethodInfos As New ChainedList(Of ExceptionInfo)

	Public ReadOnly Property ExceptionMethodInfos() As ChainedList(Of ExceptionInfo)
		Get
			Return m_ExceptionMethodInfos
		End Get
	End Property

	Public Overrides Function ToString() As String
		Return Me.ExceptionName
	End Function

End Class
















Imports System
Imports DevExpress.CodeRush.StructuralParser

''' <summary>
''' A wrapper structure to hold some info about an exception.
''' </summary>
''' <remarks>we use this to hold the exception name and description in one usage, 
''' and in another we hold the method name </remarks>
Friend Structure ExceptionInfo

	Public TypeName As String
	Public Description As String
	
	Public Sub New(ByVal typename As String, ByVal description As String)
		Me.TypeName = typename
		Me.Description = description
	End Sub

End Structure

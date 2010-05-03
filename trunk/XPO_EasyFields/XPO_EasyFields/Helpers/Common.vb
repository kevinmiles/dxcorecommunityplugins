Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Namespace Helpers
    Namespace Common
        Public Module Common

            Public Function Classes(ByVal Scope As LanguageElement) As IEnumerable(Of IClassElement)
                Return New ElementEnumerable(Scope, GetType(IClassElement), True).OfType(Of IClassElement)()
            End Function

            Public Function IsElementOfType(ByVal DeclaredTypeExpression As ITypeReferenceExpression, ByVal type As String) As Boolean
                Try
                    If DeclaredTypeExpression Is Nothing Then
                        Return Nothing
                    End If
                    Dim DeclaredTypeElement As ITypeElement = TryCast(Source.ResolveExpression(DeclaredTypeExpression), ITypeElement)

                    Return IsElementOfType(DeclaredTypeElement, type)
                Catch ex As Exception
                    Trace.WriteLine("Exception in IsElementOfType Function: " & ex.Message)
                End Try
                Return False
            End Function

            Public Function GetExpressionCollection(ByVal ParamArray ExpressionList() As Expression) As ExpressionCollection
                Dim result As New ExpressionCollection
                For Each Expression As Expression In ExpressionList
                    result.Add(Expression)
                Next
                Return result
            End Function

            Public Function IsElementOfType(ByVal DeclaredTypeElement As ITypeElement, ByVal type As String) As Boolean
                If DeclaredTypeElement Is Nothing Then
                    Return False
                End If
                If DeclaredTypeElement.FullName.Replace("`1", "") = type OrElse DeclaredTypeElement.DescendsFrom(type) Then 'dodgy hack to get around generic
                    Return True
                End If
            End Function

            Public Sub ShowError(ByVal ErrorText As String)
                Using Feedback As New BigFeedback()
                    CodeRush.Hints.Settings.FeedbackFillColor = Color.Red
                    CodeRush.Hints.Settings.FeedbackBorderColor = Color.DarkRed
                    Feedback.Text = ErrorText
                End Using
            End Sub

            Public Sub ShowMessage(ByVal Message As String)
                Using Feedback As New BigFeedback()
                    CodeRush.Hints.Settings.FeedbackFillColor = Color.Green
                    CodeRush.Hints.Settings.FeedbackBorderColor = Color.DarkGreen
                    Feedback.Text = Message
                    Feedback.Show()
                End Using
            End Sub
        End Module
    End Namespace
End Namespace

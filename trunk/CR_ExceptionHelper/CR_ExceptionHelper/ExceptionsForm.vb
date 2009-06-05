Option Strict On
Option Explicit On

Imports System
Imports System.Windows
Imports System.Drawing
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.PlugInCore
Imports System.Collections



Public Class ExceptionsForm
	Private m_Highlighter As Underline = New DevExpress.CodeRush.Core.Underline()		' To preview method calls contributing to the selected exception.
	Private m_LastSelectedItem As Object
	Private m_ActiveView As TextView

	Friend Function FilterExceptions(ByVal exceptions As ExceptionHandlerCollection) As ExceptionHandlerCollection
		Me.lbExceptions.Items.Clear()

		With Me.lbExceptions.Items
			For Each exHandler As ExceptionHandler In exceptions.Values
				.Add(exHandler, True)
			Next
		End With

		Dim result As Forms.DialogResult = Me.ShowDialog()

		Dim handlers As New ExceptionHandlerCollection

		If result = Forms.DialogResult.OK Then
			For Each item As ExceptionHandler In Me.lbExceptions.CheckedItems
				handlers.Add(item.ExceptionName, item)
			Next
		End If

		Return handlers
	End Function


	Private Sub btnOkay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOkay.Click
		Me.DialogResult = Forms.DialogResult.OK
		Me.Close()
	End Sub

	Private Sub btnCancel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
		Me.DialogResult = Forms.DialogResult.Cancel
		Me.Close()
	End Sub

	Private Sub ExceptionsForm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
		PositionForm()
		m_ActiveView = CodeRush.TextViews.Active
	End Sub

	Public startPoint As Point


	Friend Sub PositionForm()
		' TODO: startPoint should be set to the bottom of the selection, so the NameRange previews are all visible. - Mark
		' TODO: Add code to set height to optimally show the number of exceptions in the list, up to a height that runs from the Top of the form to the bottom of the working screen. - Mark
		Dim pt As Point = Me.startPoint
		Dim view As TextView = CodeRush.TextViews.Active
		Dim scr As Forms.Screen = Forms.Screen.FromPoint(pt)
		Dim rect As Rectangle = scr.WorkingArea
		If pt.X + Me.Width < rect.Right Then
			Me.Left = Math.Max(pt.X, 0)
		Else
			Me.Left = Math.Min(pt.X - Me.Width, rect.Right - Me.Width)
		End If
		If pt.Y + Me.Height + view.LineHeight < rect.Bottom Then
			Me.Top = Math.Min(pt.Y, rect.Bottom - Me.Height)
		Else
			Me.Top = Math.Max(pt.Y - Me.Height, 0)
		End If
	End Sub
	''' <summary>Invalidates a slightly inflated rectangle around the specified 
	''' ExceptionInfo item on the active TextView.</summary>
	Private Sub InvalidateCaller(ByVal activeView As TextView, ByVal range As SourceRange)
		Dim previewCallerRect As Rectangle = activeView.GetRectangleFromRange(range)
		Dim inflationAmount As Integer = CInt(Math.Ceiling(activeView.LineHeight / 4))
		previewCallerRect.Inflate(inflationAmount, inflationAmount)	 ' We'll be painting just a little outside the calling method, so make sure that part is invalidated.
		activeView.Invalidate(previewCallerRect)
	End Sub

	Private Sub InvalidateCallers(ByVal m_LastSelectedItem As Object)
		If m_LastSelectedItem Is Nothing Then
			Return
		End If
		Dim selectedHandler As ExceptionHandler = TryCast(m_LastSelectedItem, ExceptionHandler)
		If selectedHandler Is Nothing Then
			Return
		End If

		If m_ActiveView Is Nothing Then
			Return
		End If

		For Each range As SourceRange In selectedHandler.NameRanges
			InvalidateCaller(m_ActiveView, range)
		Next
	End Sub
	Private Sub lbExceptions_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lbExceptions.SelectedIndexChanged
		Dim thisSelectedItem As Object = Me.lbExceptions.SelectedItem
		Dim ex As ExceptionHandler = TryCast(thisSelectedItem, ExceptionHandler)
		Dim sb As New System.Text.StringBuilder
		If ex IsNot Nothing Then
			Dim throwersAlreadyDocumented As Hashtable = New Hashtable()	 ' Make sure we document multiple callers only once.
			For Each item As ExceptionInfo In ex.ExceptionMethodInfos
				Dim exceptionThrower As String = item.TypeName
				If Not throwersAlreadyDocumented.ContainsKey(exceptionThrower) Then
					throwersAlreadyDocumented.Add(exceptionThrower, Nothing)
					sb.AppendLine(String.Format("Thrown from {0}.", item.TypeName))
					sb.AppendLine(String.Format("Message: {0}", item.Description))
				End If
			Next
		End If
		Me.ToolTip1.SetToolTip(Me.lbExceptions, sb.ToString)

		If m_LastSelectedItem IsNot thisSelectedItem Then
			InvalidateCallers(m_LastSelectedItem)		' Erase old highlighting.
			m_LastSelectedItem = thisSelectedItem
			InvalidateCallers(m_LastSelectedItem)		' Paint new highlighting.
		End If
	End Sub

	Private Sub DxCoreEvents1_EditorPaintForeground(ByVal ea As DevExpress.CodeRush.Core.EditorPaintEventArgs) Handles DxCoreEvents1.EditorPaintForeground
		Dim fillBrush As Brush = New SolidBrush(Color.FromArgb(70, Color.FromArgb(&H1, &HFF, &H9C)))
		Dim outlinePen As Pen = New Pen(Color.FromArgb(170, Color.FromArgb(&H0, &HEC, &HBD)))
		Dim selectedHandler As ExceptionHandler = TryCast(Me.lbExceptions.SelectedItem, ExceptionHandler)
		If selectedHandler Is Nothing Then
			Return
		End If
		'Dim borderColor As Color = Color.FromArgb(192, Color.White)
		For Each range As SourceRange In selectedHandler.NameRanges
			'TextHighlighter.PaintFocused(ea.TextView, range, borderColor)
			m_Highlighter.Paint(ea.Graphics, ea.TextView, range, outlinePen, fillBrush)
		Next

		fillBrush.Dispose()
		outlinePen.Dispose()
	End Sub
End Class
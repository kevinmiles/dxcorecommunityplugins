Option Strict On

Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

''' <summary>
''' The plugin class
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class ExceptionHelperWizard

#Region " Private Members "

	Private m_frmExceptions As ExceptionsForm

#End Region

#Region " Plugin Events and Overrides "

	''' <summary>
	''' Any initialization code goes here.
	''' </summary>
	''' <remarks></remarks>
	Public Overrides Sub InitializePlugIn()
		MyBase.InitializePlugIn()

		'TODO: Investigate viability of 'prelocating' framework xml docs.
		'ScanForFrameworkAssemblies()
	End Sub

	''' <summary>
	''' Any cleanup code goes here
	''' </summary>
	''' <remarks></remarks>
	Public Overrides Sub FinalizePlugIn()
		If m_frmExceptions IsNot Nothing Then m_frmExceptions.Dispose()
		MyBase.FinalizePlugIn()
	End Sub



#End Region

#Region " Actions "


	''' <summary>
	''' Invoked when the plugin is activated.
	''' </summary>
	''' <param name="ea">Eventargs</param>
	''' <remarks></remarks>
	Private Sub Action1_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles Action1.Execute
		Dim pt As Point = Windows.Forms.Cursor.Position
		Dim activeView As TextView = CodeRush.TextViews.Active
		If activeView Is Nothing Then
			Return
		End If

		activeView.Selection.ExtendToWholeLines()

		Dim selectedMethods As New List(Of MethodExceptionInfo)
		Dim methodInfo As MethodExceptionInfo = Nothing

		'discover all the method calls in the selection
		For Each element As LanguageElement In CodeRush.Source.GetSelectedNodes
			methodInfo = MethodExceptionInfo.Create(element)

			If Not methodInfo Is Nothing Then
				selectedMethods.Add(methodInfo)
			End If
		Next

		'Only show the ui if there's any methods located.
		If selectedMethods.Count <= 0 Then Return

		' create list of exception handlers
		Dim handlers As ExceptionHandlerCollection = TryCatchGenerator.GetExceptionHandlerCollection(selectedMethods)

		If handlers.Count <= 0 Then Return
		If Me.m_frmExceptions Is Nothing Then Me.m_frmExceptions = New ExceptionsForm
		m_frmExceptions.startPoint = pt
		handlers = m_frmExceptions.FilterExceptions(handlers)


		' Stop the EditorPaint event handler from continuing to update the screen:
		Me.m_frmExceptions.Dispose()
		Me.m_frmExceptions = Nothing


		If handlers.Count <= 0 Then Return

		Me.HandlersGenerator1.WrapSelectionInTryCatch(handlers.Values)

	End Sub


#End Region

#Region " clear cache on references changed"

	Private Sub ExceptionHelper_ReferenceAdded(ByVal ea As DevExpress.CodeRush.Core.ReferenceEventArgs) Handles Me.ReferenceAdded, Me.ReferenceChanged, Me.ReferenceRemoved
		ClearCache()
	End Sub

	Private Sub ExceptionHelperWizard_SolutionRenamed(ByVal oldName As String) Handles Me.SolutionRenamed
		ClearCache()
	End Sub

	Private Sub ExceptionHelperWizard_SolutionOpened() Handles Me.SolutionOpened
		ClearCache()
	End Sub

	Private Sub ExceptionHelperWizard_SolutionConfigurationChanged(ByVal ea As DevExpress.CodeRush.Core.SolutionConfigurationChangedEventArgs) Handles Me.SolutionConfigurationChanged
		ClearCache()
	End Sub

	Private Sub ExceptionHelperWizard_SolutionItemAdded(ByVal projectItem As EnvDTE.ProjectItem) Handles Me.SolutionItemAdded, Me.SolutionItemRemoved, Me.ProjectItemAdded, Me.ProjectItemRemoved
		ClearCache()
	End Sub


	Private Sub ExceptionHelperWizard_ProjectItemRenamed(ByVal projectItem As EnvDTE.ProjectItem, ByVal oldName As String) Handles Me.ProjectItemRenamed, Me.SolutionItemRenamed
		ClearCache()
	End Sub

	Private Sub ExceptionHelperWizard_ProjectRenamed(ByVal project As EnvDTE.Project, ByVal oldName As String) Handles Me.ProjectRenamed
		ClearCache()
	End Sub

	Private Sub ExceptionHelperWizard_ProjectRemoved(ByVal project As EnvDTE.Project) Handles Me.ProjectAdded, Me.ProjectRemoved
		ClearCache()
	End Sub

	Private Sub ClearCache()
		MethodExceptionInfo.Clear()
	End Sub

#End Region



End Class

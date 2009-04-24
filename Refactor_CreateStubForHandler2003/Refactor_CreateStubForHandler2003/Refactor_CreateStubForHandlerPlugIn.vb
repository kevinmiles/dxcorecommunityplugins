Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports Sp = DevExpress.CodeRush.StructuralParser
Imports Microsoft.VisualBasic

Namespace Refactor_CreateStubForHandler
    ''' <summary>
    ''' Summary description for Refactor_CreateStubForHandlerPlugIn.
    ''' </summary>
    Public Class Refactor_CreateStubForHandlerPlugIn
        Inherits StandardPlugIn
#Region "Generated Code"
        Friend WithEvents RefactorProvider As DevExpress.Refactor.Core.RefactoringProvider
#Region " private fields... "
        Private components As System.ComponentModel.IContainer
#End Region

        ' constructor...
#Region " Refactor_CreateStubForHandlerPlugIn "
        ''' <summary>
        ''' Required for Windows.Forms Class Composition Designer support
        ''' </summary>
        Public Sub New()
            InitializeComponent()
        End Sub
#End Region

        ' CodeRush-generated code
#Region " InitializePlugIn "
        Public Overrides Sub InitializePlugIn()
            MyBase.InitializePlugIn()

            '
            ' TODO: Add your initialization code here.
            ''
        End Sub
#End Region
#Region " FinalizePlugIn "
        Public Overrides Sub FinalizePlugIn()
            '
            ' TODO: Add your finalization code here.
            '

            MyBase.FinalizePlugIn()
        End Sub
#End Region

#Region " Component Designer generated code "
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Friend WithEvents RefectorAction As DevExpress.CodeRush.Core.Action
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Refactor_CreateStubForHandlerPlugIn))
            Me.RefactorProvider = New DevExpress.Refactor.Core.RefactoringProvider(Me.components)
            Me.RefectorAction = New DevExpress.CodeRush.Core.Action(Me.components)
            CType(Me.RefactorProvider, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.RefectorAction, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'RefactorProvider
            '
            Me.RefactorProvider.ActionHintText = "Crate method stub for event handler"
            Me.RefactorProvider.AutoActivate = True
            Me.RefactorProvider.AutoUndo = False
            Me.RefactorProvider.Description = "Crate method stub for event handler"
            Me.RefactorProvider.DisplayName = "Create method stub"
            Me.RefactorProvider.ProviderName = "Refactor_CreateMethodStubForEventHandler"
            Me.RefactorProvider.Register = True
            '
            'RefectorAction
            '
            Me.RefectorAction.ActionName = "AddHandler completion"
            Me.RefectorAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
            Me.RefectorAction.Description = "Complate addhandler statement and create methodstub"
            Me.RefectorAction.Image = CType(resources.GetObject("RefectorAction.Image"), System.Drawing.Bitmap)
            Me.RefectorAction.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
            CType(Me.RefactorProvider, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.RefectorAction, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
#End Region
#End Region
#Region "Fields"
        Private _fromAction As Boolean = False
        Private _CaretElement As LanguageElement
#End Region
        Friend WithEvents picker As New DevExpress.CodeRush.PlugInCore.TargetPicker
#Region "Refactor Trigger"
        'check the refactoring availability
        Private Sub RefactorProvider_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.Refactor.Core.CheckAvailabilityEventArgs) Handles RefactorProvider.CheckAvailability
            CodeRush.Source.ParseIfTextChanged()
            Dim CaretElement As LanguageElement = ea.Element
            If Not CaretElement Is Nothing Then
                If TypeOf CaretElement Is ElementReferenceExpression _
                    AndAlso DirectCast(CaretElement, ElementReferenceExpression).GetDeclaration Is Nothing _
                    AndAlso Not DirectCast(CaretElement, ElementReferenceExpression).Parent Is Nothing _
                    AndAlso TypeOf DirectCast(CaretElement, ElementReferenceExpression).Parent Is AddressOfExpression Then
                    Me._CaretElement = CaretElement
                    ea.Availability = RefactoringAvailability.Available
                End If
            End If
        End Sub
        Private Sub RefactorProvider_Apply(ByVal sender As Object, ByVal ea As DevExpress.Refactor.Core.ApplyRefactoringEventArgs) Handles RefactorProvider.Apply
            Try
                _fromAction = False
                picker.Start()
            Catch ex As Exception

            End Try
        End Sub
#End Region
#Region "Action Trigger"
        'invoke the refactoring when the action as executed
        Private Sub RefectorAction_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles RefectorAction.Execute
            CodeRush.Source.ParseIfTextChanged()
            _CaretElement = CodeRush.Source.Active
            If Not _CaretElement Is Nothing _
                AndAlso Not _CaretElement.Parent Is Nothing _
                AndAlso (TypeOf _CaretElement.Parent Is [AddHandler] _
                    OrElse TypeOf _CaretElement.Parent Is [RemoveHandler]) Then
                _fromAction = True
                picker.Start()
            End If
        End Sub
#End Region
        'when the target is selected invoke the main method for the refactoring
        Private Sub picker_TargetSelected(ByVal sender As Object, ByVal ea As DevExpress.CodeRush.PlugInCore.TargetSelectedEventArgs) Handles picker.TargetSelected
            PluginLogic.CreateMethodStub(_CaretElement, ea.Location.InsertionPoint, _fromAction)
        End Sub
    End Class
End Namespace

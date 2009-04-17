Option Strict On
Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Collections
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports SP = DevExpress.CodeRush.StructuralParser
Imports CR_PaintIt.Controls
Imports CR_PaintIt.Painting
''' <summary>
''' Summary description for PaintIt.
''' </summary>
Public Class PaintIt
    Inherits StandardPlugIn
    Const STR_PaintItMemberColorCache As String = "PaintItMemberColorCache"
#Region "Standard Plugin Goodness :)"
#Region " Standard private fields... "
    Private components As System.ComponentModel.IContainer
#End Region

    ' constructor...
#Region " PaintIt "
    ''' <summary>
    ''' Required for Windows.Forms Class Composition Designer support
    ''' </summary>
    Public Sub New()
        InitializeComponent()
        Call LoadSettings()
    End Sub
#End Region
    Private Painter As Painter
    ' CodeRush-generated code
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        '
        ' TODO: Add your initialization code here.
        '
        Painter = New Painter
        '        Addhandler Coderush.Documents.ActiveTextDocument.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        '
        ' TODO: Add your finalization code here.
        '
        Painter.Dispose()
        MyBase.FinalizePlugIn()
    End Sub
#End Region

#Region " Component Designer generated code "
    ''' <summary>
    ''' Required method for Designer support - do not modify
    ''' the contents of this method with the code editor.
    ''' </summary>
    Friend WithEvents actRefreshCache As DevExpress.CodeRush.Core.Action
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(PaintIt))
        Me.actRefreshCache = New DevExpress.CodeRush.Core.Action(Me.components)
        CType(Me.actRefreshCache, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        '
        'actRefreshCache
        '
        Me.actRefreshCache.ActionName = "RefreshCachePaintIt"
        Me.actRefreshCache.ButtonText = "PaintIt Refresh Cache"
        Me.actRefreshCache.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
        Me.actRefreshCache.Description = "Refreshes the internal PaintIt cache and forces a refresh"
        Me.actRefreshCache.Image = CType(resources.GetObject("actRefreshCache.Image"), System.Drawing.Bitmap)
        Me.actRefreshCache.ImageBackColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(254, Byte), CType(0, Byte))
        '
        'PaintIt
        '
        CType(Me.actRefreshCache, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

    End Sub
#End Region

#Region " Specialised private fields... "
    Private Settings As New PaintItSettings
    Private SelectedMember As LanguageElement
    Private CurrentReferences As New LanguageElementCollection
#End Region
#End Region

#Region "Utility"
    Private ReferenceTypes() As LanguageElementType = { _
                                                LanguageElementType.InitializedVariable, _
                                                LanguageElementType.Variable, _
                                                LanguageElementType.Parameter, _
                                                LanguageElementType.Method, _
                                                LanguageElementType.Property}
    Private Function IsScopePaintable(ByVal Element As LanguageElement) As Boolean
        Return isElementType(Element, AllDeclarationTypes) _
        OrElse isElementType(Element, AllReferenceTypes)
    End Function

    Private Function IsHighlightCandidate(ByVal Element As LanguageElement) As Boolean
        If isElementType(Element, AllDeclarationTypes) _
        OrElse isElementType(Element, AllReferenceTypes) Then
            Return True
        End If
    End Function
    Private Function GetDeclarationOrNothing(ByVal Element As LanguageElement) As LanguageElement
        Dim DeclarationLite As IElement = Element.GetDeclaration
        If DeclarationLite Is Nothing Then
            Return Nothing
        End If
        Dim Declaration As LanguageElement = DeclarationLite.ToLanguageElement
        If Declaration Is Nothing Then
            Return Nothing
        End If
        Return Declaration
    End Function
#End Region
#Region "Fields"
    Private mCache As New PaintOptionsCache
#End Region
#Region "Properties"
    Private ReadOnly Property Cache() As PaintOptionsCache
        Get
            If mCache Is Nothing Then
                RegenerateCache()
            End If
            Return mCache
        End Get
    End Property
#End Region
#Region "Trigger Events "
    Private Sub PaintIt_EditorPaint(ByVal ea As DevExpress.CodeRush.Core.EditorPaintEventArgs) Handles MyBase.EditorPaint
        Call Painter.PaintCachedElements(Cache, ea)
        Call HighlightElements(ea)
    End Sub
    Private Sub PaintIt_AfterParse(ByVal ea As DevExpress.CodeRush.Core.AfterParseEventArgs) Handles MyBase.AfterParse
        Call RegenerateCache()
    End Sub
    Private Sub PaintIt_EditorMouseHover(ByVal ea As DevExpress.CodeRush.Core.EditorEventArgs) Handles MyBase.EditorMouseHover
        Try
            If Not Settings.CurrentMemberSettings.Enabled Then
                Return
            End If
            If Settings.CurrentMemberSettings.DetectionMethod = DetectionMethod.CurrentIsUnderCursor Then
                Call RegenerateReferenceList(ea.TextView.GetLanguageElementUnderMouse)
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Throw
        End Try
    End Sub
    Private Sub PaintIt_LanguageElementActivated(ByVal ea As DevExpress.CodeRush.Core.LanguageElementActivatedEventArgs) Handles MyBase.LanguageElementActivated
        Try
            If Not Settings.CurrentMemberSettings.Enabled Then
                Return
            End If
            If Settings.CurrentMemberSettings.DetectionMethod = DetectionMethod.CurrentIsUnderCaret Then
                Call RegenerateReferenceList(ea.Element)
            End If
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Throw
        End Try
    End Sub
    Private Sub PaintIt_DocumentActivated(ByVal ea As DevExpress.CodeRush.Core.DocumentEventArgs) Handles MyBase.DocumentActivated
        Try
            mCache = Caching.GetCacheFromDocument(ea.Document, STR_PaintItMemberColorCache)
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Throw
        End Try

    End Sub
#End Region
    Private Sub RegenerateCache()
        Try
            mCache = New PaintOptionsCache
            If CodeRush.Documents.ActiveTextDocument Is Nothing Then
                Exit Sub
            End If
            mCache.AddByDelegate(GetActiveSourceFile, AddressOf IsScopePaintable, AddressOf GetPaintOptionsForElement)
            Caching.SaveCacheToDocument(CodeRush.Documents.ActiveTextDocument, mCache, STR_PaintItMemberColorCache)
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Throw
        End Try
    End Sub
#Region "Settings Options "
    Private Sub PaintIt_OptionsChanged(ByVal ea As DevExpress.CodeRush.Core.OptionsChangedEventArgs) Handles MyBase.OptionsChanged
        If ea.OptionsPages.Contains(GetType(OptPaintIt)) Then
            Call LoadSettings()
        End If
    End Sub
    Private Sub LoadSettings()
        Dim Storage As DecoupledStorage = OptPaintIt.Storage
        With Settings.CurrentMemberSettings
            If Storage.ReadBoolean(OptPaintIt.SECTION_CURRENT_MEMBER, "CurrentUnderCaret", True) Then
                .DetectionMethod = DetectionMethod.CurrentIsUnderCaret
            Else
                .DetectionMethod = DetectionMethod.CurrentIsUnderCursor
            End If
            .Enabled = Storage.ReadBoolean(OptPaintIt.SECTION_CURRENT_MEMBER, "Enabled", True)
            LoadStyle(Storage, OptPaintIt.SECTION_CURRENT_MEMBER, "", .CurrentMemberExactOptions, OptPaintIt.DefaultHighlightStyle)
            LoadStyle(Storage, OptPaintIt.SECTION_CURRENT_MEMBER, "", .CurrentMemberSiblingOptions, OptPaintIt.DefaultHighlightStyle)
            .CurrentMemberExactOptions.Enabled = Storage.ReadBoolean("CurrentMember", "HighlightCurrentItem", True)
        End With

        'LoadBasicScopeSettings(Settings.VarSettings)
        Call LoadBasicScopeSettings(Settings.VarSettings, Storage, OptPaintIt.DefaultMemberStyle.Color1)
        Call LoadBasicScopeSettings(Settings.MethodSettings, Storage, OptPaintIt.DefaultMemberStyle.Color1)
        Call LoadBasicScopeSettings(Settings.PropertySettings, Storage, OptPaintIt.DefaultMemberStyle.Color1)
        Call LoadExtendedVarSettings(Settings.VarSettings, Storage, OptPaintIt.DefaultMemberStyle.Color1)
        'KillAllCaches()
        If Not CodeRush.Documents.ActiveTextDocument Is Nothing Then
            Call RegenerateCache()
        End If
        Storage.Dispose()
    End Sub
    Private Sub LoadBasicScopeSettings(ByVal Settings As MemberSettings, ByVal Storage As DecoupledStorage, ByVal DefaultFontColor As IPaintColor)
        LoadIColor(Storage, OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_COLOR_PUBLIC, Settings.Public.Color1, DefaultFontColor)
        LoadIColor(Storage, OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_COLOR_PROTECTED, Settings.Protected.Color1, DefaultFontColor)
        LoadIColor(Storage, OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_COLOR_PROTECTED_INTERNAL, Settings.ProtectedInternal.Color1, DefaultFontColor)
        LoadIColor(Storage, OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_COLOR_INTERNAL, Settings.Internal.Color1, DefaultFontColor)
        LoadIColor(Storage, OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_COLOR_PRIVATE, Settings.Private.Color1, DefaultFontColor)
        Settings.Public.Enabled = Storage.ReadBoolean(OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_ENABLED_PUBLIC, True)
        Settings.Protected.Enabled = Storage.ReadBoolean(OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_ENABLED_PROTECTED, True)
        Settings.ProtectedInternal.Enabled = Storage.ReadBoolean(OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_ENABLED_PROTECTED_INTERNAL, True)
        Settings.Internal.Enabled = Storage.ReadBoolean(OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_ENABLED_INTERNAL, True)
        Settings.Private.Enabled = Storage.ReadBoolean(OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_ENABLED_PRIVATE, True)
    End Sub
    Private Sub LoadExtendedVarSettings(ByVal VarSettings As VarSettings, ByVal Storage As DecoupledStorage, ByVal DefaultFontColor As IPaintColor)
        LoadIColor(Storage, OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_COLOR_LOCAL, VarSettings.Local.Color1, DefaultFontColor)
        LoadIColor(Storage, OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_COLOR_PARAM_IN, VarSettings.ParamIn.Color1, DefaultFontColor)
        LoadIColor(Storage, OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_COLOR_PARAM_OUT, VarSettings.ParamOut.Color1, DefaultFontColor)
        LoadIColor(Storage, OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_COLOR_PARAM_REF, VarSettings.ParamRef.Color1, DefaultFontColor)
        LoadIColor(Storage, OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_COLOR_PARAM_ARRAY, VarSettings.ParamArray.Color1, DefaultFontColor)
        VarSettings.Local.Enabled = Storage.ReadBoolean(OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_ENABLED_LOCAL, True)
        VarSettings.ParamIn.Enabled = Storage.ReadBoolean(OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_ENABLED_PARAM_IN, True)
        VarSettings.ParamOut.Enabled = Storage.ReadBoolean(OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_ENABLED_PARAM_OUT, True)
        VarSettings.ParamRef.Enabled = Storage.ReadBoolean(OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_ENABLED_PARAM_REF, True)
        VarSettings.ParamArray.Enabled = Storage.ReadBoolean(OptPaintIt.SECTION_MEMBER_COLORS, OptPaintIt.SETTING_ENABLED_PARAM_ARRAY, True)
    End Sub
#End Region
#Region "Member Painting"
    Private Function GetPaintOptionsForElement(ByVal Element As LanguageElement) As PaintOptions
        Select Case Element.ElementType
            Case LanguageElementType.Variable, _
                 LanguageElementType.InitializedVariable, _
                 LanguageElementType.Parameter
                ' Variable Declarations
                Return Settings.VarSettings.GetScopedOptions(CType(Element, BaseVariable))
            Case LanguageElementType.ElementReferenceExpression, _
                 LanguageElementType.ParameterReference
                ' Variable References
                Dim Declaration As LanguageElement = GetDeclarationOrNothing(Element)
                Return Settings.VarSettings.GetScopedOptions(CType(Declaration, IMemberElement))
            Case LanguageElementType.Method
                ' Method Declaration
                Return Settings.MethodSettings.GetScopedOptions(CType(Element, Method))
            Case LanguageElementType.MethodCall, LanguageElementType.MethodCallExpression
                ' Method References
                Dim Declaration As LanguageElement = GetDeclarationOrNothing(Element)
                Return Settings.MethodSettings.GetScopedOptions(CType(Declaration, Method))
            Case LanguageElementType.Property
                ' Property Declaration
                Return Settings.MethodSettings.GetScopedOptions(CType(Element, [Property]))
        End Select
    End Function
#End Region
#Region "Current Member Painting"
    Private Sub ClearElements()
        If CurrentReferences.Count = 0 Then
            Return
        End If
        InvalidateElements()
        CurrentReferences.Clear()
    End Sub
    Private Sub RegenerateReferenceList(ByVal StartElement As LanguageElement)
        Try
            ClearElements()
            If Not IsHighlightCandidate(StartElement) Then
                Return
            End If
            SelectedMember = StartElement
            Dim DeclarationLite As IElement
            If isElementType(StartElement, AllDeclarationTypes) Then
                DeclarationLite = StartElement
            Else
                'DeclarationLite = StartElement.GetDeclaration
                If Not ParserServices.HasSourceTreeResolver Then
                    ParserServices.RegisterSourceTreeResolver(New SourceTreeResolver)
                End If
                Call CType(StartElement.Document, TextDocument).ParseIfNeeded()
                DeclarationLite = ParserServices.SourceModelService.GetDeclaration(StartElement)

            End If
            Dim Declaration As LanguageElement = DeclarationLite.ToLanguageElement
            If Not Declaration Is Nothing Then
                CurrentReferences.Add(Declaration)
            End If
            CurrentReferences.AddRange(DeclarationLite.FindAllReferences.ToLanguageElementCollection)
            For Each Element As LanguageElement In CurrentReferences
                If ElementsAreSame(Element, StartElement) Then
                    CurrentReferences.Remove(Element)
                    Exit For
                End If
            Next
            InvalidateElements()
        Catch ex As Exception
            Debug.WriteLine(ex.Message)
            Throw
        End Try
    End Sub
    Private Sub InvalidateElements()
        Dim ActiveView As TextView = CodeRush.TextViews.Active
        Call InvalidateElement(SelectedMember, ActiveView)
        For Each Element As LanguageElement In CurrentReferences
            InvalidateElement(Element, ActiveView)
        Next
    End Sub
    Private Sub InvalidateElement(ByVal Element As LanguageElement, ByVal ActiveView As TextView)
        ActiveView.Invalidate(LargerRect(Element, ActiveView))
    End Sub
    Private Sub HighlightElements(ByVal PaintArgs As EditorPaintEventArgs)
        If CurrentReferences.Count = 0 Then
            Return
        End If
        Dim ExactOptions As PaintOptions = Settings.CurrentMemberSettings.CurrentMemberExactOptions
        Call Painter.PaintElementName(SelectedMember, PaintArgs, ExactOptions, True)
        Dim SiblingOptions As PaintOptions = Settings.CurrentMemberSettings.CurrentMemberSiblingOptions
        For Each Element As LanguageElement In CurrentReferences
            Call Painter.PaintElementName(Element, PaintArgs, SiblingOptions)
        Next
    End Sub
    Private Function LargerRect(ByVal LE As LanguageElement, ByVal View As TextView) As Rectangle
        LargerRect = View.GetRectangleFromLanguageElement(LE)
        LargerRect.Inflate(New Size(4, 4))
    End Function
#End Region

    Private Sub actRefreshCache_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles actRefreshCache.Execute
        CodeRush.Source.ParseIfNeeded()
        Call RegenerateCache()
        With CodeRush.Documents.ActiveTextDocument
            For Each CurrentTextView As TextView In .GetTextViews
                CurrentTextView.Invalidate()
            Next
        End With
    End Sub
End Class
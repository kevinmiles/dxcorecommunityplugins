Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Menus

Public Class PlugIn1

    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        LoadSettings()
        Call CreateMenus()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region
#Region "Settings"
    Private mPluginSettings As PluginSettings
    Private Sub PlugIn1_OptionsChanged(ByVal ea As DevExpress.CodeRush.Core.OptionsChangedEventArgs) Handles Me.OptionsChanged
        If ea.OptionsPages.Contains(GetType(Options1)) Then
            Call LoadSettings()
        End If
    End Sub
    Private Sub LoadSettings()
        mPluginSettings = PluginSettingsRepo.LoadSettings(Options1.Storage)
    End Sub
#End Region

    Private Sub CreateMenus()
        Dim RootMenu = RegisterMenuItem(CodeRush.Menus.Main, mPluginSettings.ChosenMenuName)
        Call RegisterMenuLinkItem(RootMenu, "Intro Video", "http://devexpress.com/Support/Webinars/details.xml?id=CodeRushIntroQandA")

        Call InitializeMarkersMenu(RootMenu)
        Call InitializeQuickNavMenu(RootMenu)
        Call InitializeTestingMenu(RootMenu)
        Call InitializeOptionsMenu(RootMenu)
        Call InitializeHelpLocalMenu(RootMenu)
        Call InitializeHelpOnlineMenu(RootMenu)
        Call InitializeVideosMenu(RootMenu)
        Call InitializeExtrasMenu(RootMenu)

    End Sub
    Private Sub InitializeMarkersMenu(ByVal RootMenu As IMenuPopup)
        Dim MarkerMenu = RegisterMenuItem(RootMenu, "Markers", True)
        Call RegisterMenuItem(MarkerMenu, "Drop Marker", CodeRush.Actions.Item("MarkerDrop"))
        Call RegisterMenuItem(MarkerMenu, "Collect Marker", CodeRush.Actions.Item("MarkerCollect"))
        Call RegisterMenuItem(MarkerMenu, "Collect Marker and Paste", CodeRush.Actions.Item("MarkerCollectAndPaste"))
        Call RegisterMenuItem(MarkerMenu, "Swap Caret and Marker", CodeRush.Actions.Item("MarkerSwap"))
    End Sub
    Private Sub InitializeQuickNavMenu(ByVal RootMenu As IMenuPopup)
        Dim FindMenu = RegisterMenuItem(RootMenu, "QuickNav (Find)")
        Call RegisterMenuItem(FindMenu, "QuickNav", CodeRush.Actions.Item("QuickNav"), "AllTypes,AllMembers,AllVisibilities,CurrentSolution")
        Call RegisterMenuItem(FindMenu, "QuickNav - Types", CodeRush.Actions.Item("QuickNav"), "AllTypes,,AllVisibilities,CurrentSolution")
        Call RegisterMenuItem(FindMenu, "QuickNav - Members", CodeRush.Actions.Item("QuickNav"), ",AllMembers,AllVisibilities,CurrentSolution")
        Call RegisterMenuItem(FindMenu, "QuickNav - Files", CodeRush.Actions.Item("QuickFileNav"))
        Call RegisterMenuItem(FindMenu, "Find All References", CodeRush.Actions.Item("ShowReferences"))
    End Sub
    Private Sub InitializeTestingMenu(ByVal RootMenu As IMenuPopup)
        Dim TestingMenu = RegisterMenuItem(RootMenu, "Testing")
        Call RegisterMenuItem(TestingMenu, "Test Runner", CodeRush.Actions.Item("UnitTestsShowRunner"))

        Call RegisterMenuItem(TestingMenu, "Debug Single Test", CodeRush.Actions.Item("UnitTestsDebugAtCursor"), "", True)
        Call RegisterMenuItem(TestingMenu, "Run Single Test", CodeRush.Actions.Item("UnitTestsRunAtCursor"))

        Call RegisterMenuItem(TestingMenu, "Run Class Tests", CodeRush.Actions.Item("UnitTestsRunCurrentClass"))
        Call RegisterMenuItem(TestingMenu, "Run File Tests", CodeRush.Actions.Item("UnitTestsRunFile"))
        Call RegisterMenuItem(TestingMenu, "Run Project Tests", CodeRush.Actions.Item("UnitTestsRunProject"))
        Call RegisterMenuItem(TestingMenu, "Run Solution Tests", CodeRush.Actions.Item("UnitTestsRunSolution"))
    End Sub

    Private Sub InitializeOptionsMenu(ByVal ParentMenu As IMenuPopup)
        Dim OptionsMenu = RegisterMenuItem(ParentMenu, "Options", True)
        Call RegisterMenuItem(OptionsMenu, "General...", CodeRush.Actions.Item("Options"), "", True)
        Call RegisterMenuItem(OptionsMenu, "Templates...", CodeRush.Actions.Item("Options"), "Editor\Templates")
        Call RegisterMenuItem(OptionsMenu, "Shortcuts...", CodeRush.Actions.Item("Options"), "IDE\Shortcuts")
        Call RegisterMenuItem(OptionsMenu, "Embeddings...", CodeRush.Actions.Item("Options"), "Editor\Selections\Embedding")
        Dim TestingOptionsMenu = RegisterMenuItem(OptionsMenu, "Testing", True)
        Call RegisterMenuItem(TestingOptionsMenu, "Test Runner...", CodeRush.Actions.Item("Options"), "Unit Testing\Test Runner")
        Call RegisterMenuItem(TestingOptionsMenu, "Test Runner Window...", CodeRush.Actions.Item("Options"), "Unit Testing\Test Runner Window")

        Dim CodeStyleOptionsMenu = RegisterMenuItem(OptionsMenu, "Code Style", True)
        Call RegisterMenuItem(CodeStyleOptionsMenu, "Identifiers...", CodeRush.Actions.Item("Options"), "Editor\Code Style\Identifiers")
        Call RegisterMenuItem(CodeStyleOptionsMenu, "Scope (Code-Behind)...", CodeRush.Actions.Item("Options"), "Editor\Code Style\Code-behind Scope")
        Call RegisterMenuItem(CodeStyleOptionsMenu, "Scope (Declarations)...", CodeRush.Actions.Item("Options"), "Editor\Code Style\Scope")
        Dim CodeFormattingOptionsMenu = RegisterMenuItem(OptionsMenu, "Code Formatting")
        Call RegisterMenuItem(CodeFormattingOptionsMenu, "General...", CodeRush.Actions.Item("Options"), "Editor\Code Formatting\General")
        Call RegisterMenuItem(CodeFormattingOptionsMenu, "Indentation...", CodeRush.Actions.Item("Options"), "Editor\Code Formatting\Indentation")
        Call RegisterMenuItem(CodeFormattingOptionsMenu, "Line Breaks...", CodeRush.Actions.Item("Options"), "Editor\Code Formatting\Line Breaks")
        Call RegisterMenuItem(CodeFormattingOptionsMenu, "Spacing...", CodeRush.Actions.Item("Options"), "Editor\Code Formatting\Spacing")
        Call RegisterMenuItem(CodeFormattingOptionsMenu, "Blank Lines...", CodeRush.Actions.Item("Options"), "Editor\Code Formatting\Blank Lines")
        Call RegisterMenuItem(CodeFormattingOptionsMenu, "Wrap Alignment...", CodeRush.Actions.Item("Options"), "Editor\Code Formatting\Wrapping Alignment")
        Call RegisterMenuItem(CodeFormattingOptionsMenu, "Other / Language Specific...", CodeRush.Actions.Item("Options"), "Editor\Code Formatting\Other/Language Specific")
        Dim CodeCleanupOptionsMenu = RegisterMenuItem(OptionsMenu, "Code Cleanup...", CodeRush.Actions.Item("Options"), "Editor\Code Cleanup\Rules")

        Dim VisualMenu = RegisterMenuItem(OptionsMenu, "Visual")
        Call RegisterMenuItem(VisualMenu, "Arrows...", CodeRush.Actions.Item("Options"), "Hinting\Action Hints")
        Call RegisterMenuItem(VisualMenu, "Billboard...", CodeRush.Actions.Item("Options"), "Hinting\Billboard Messages")
        Call RegisterMenuItem(VisualMenu, "Shortcut Hints...", CodeRush.Actions.Item("Options"), "Hinting\Billboard Messages")
        Call RegisterMenuItem(OptionsMenu, "DXV2...", CodeRush.Actions.Item("Options"), Options1.GetCategory & "\" & Options1.GetPageName)

    End Sub
    Private Sub InitializeHelpOnlineMenu(ByVal ParentMenu As IMenuPopup)
        Dim HelpOnlineMenu = RegisterMenuItem(ParentMenu, "Help (Online)")
        Call RegisterMenuLinkItem(HelpOnlineMenu, "Online Documentation", "http://documentation.devexpress.com/#CodeRush", True)
        Call RegisterMenuLinkItem(HelpOnlineMenu, "Forums", "http://community.devexpress.com/forums/115.aspx")
        Call RegisterMenuLinkItem(HelpOnlineMenu, "Search", "http://search.devexpress.com")
        Dim BlogsMenu = RegisterMenuItem(HelpOnlineMenu, "Blogs", True)
        Call RegisterMenuLinkItem(BlogsMenu, "Mark Miller", "http://devexpress.com/Mark")
        Call RegisterMenuLinkItem(BlogsMenu, "Rory Becker", "http://devexpress.com/Rory")
        Call RegisterMenuLinkItem(BlogsMenu, "Alex Skorkin", "http://Skorkin.com/")
    End Sub
    Private Sub InitializeHelpLocalMenu(ByVal ParentMenu As IMenuPopup)
        Dim HelpLocalMenu = RegisterMenuItem(ParentMenu, "Help (Local)", True)
        Call RegisterMenuItem(HelpLocalMenu, "Training Window", CodeRush.Actions.Item("CodeRushTrainingWindowToggle"))
        Call RegisterMenuItem(HelpLocalMenu, "User Guide", CodeRush.Actions.Item("ShowUserGuide"))
    End Sub
    Private Sub InitializeVideosMenu(ByVal ParentMenu As IMenuPopup)
        Dim VideoMenu = RegisterMenuItem(ParentMenu, "Videos")
        Call RegisterMenuLinkItem(VideoMenu, "Intro Video", "http://devexpress.com/Support/Webinars/details.xml?id=CodeRushIntroQandA")
        Call RegisterMenuLinkItem(VideoMenu, "CodeRush Training Videos", "http://community.devexpress.com/blogs/markmiller/archive/2011/05/04/get-your-coderush-training-here.aspx", True)
        Call RegisterMenuLinkItem(VideoMenu, "Feature Workshop Videos", "http://www.devexpress.com/Support/Webinars/series-details.xml?series=CodeRush%20Feature%20Workshop")
        Call RegisterMenuLinkItem(VideoMenu, "Archive Videos", "http://tv.devexpress.com/#;CodeRush%20with%20Refactor!%20Pro.product;1")
    End Sub
    Private Sub InitializeExtrasMenu(ByVal ParentMenu As IMenuPopup)
        Dim ExtrasMenu = RegisterMenuItem(ParentMenu, "Extras", True)
        Call RegisterMenuDelegateItem(ExtrasMenu, "Plugin Folder...", Function() Process.Start(CodeRush.Options.Paths.CommunityPlugInsPath))
        Call RegisterMenuDelegateItem(ExtrasMenu, "Settings Folder...", Function() Process.Start(CodeRush.Options.Paths.SettingsPreferredPath))
    End Sub
#Region "CreateMenus"
    Private Function RegisterMenuItem(ByVal ParentMenu As IMenuContainer, ByVal MenuName As String, Optional ByVal BeginGroup As Boolean = False) As IMenuPopup
        Dim FoundMenu = TryCast(ParentMenu.FindByCaption(MenuName), IMenuPopup)
        If FoundMenu IsNot Nothing Then
            Return FoundMenu
        End If
        Dim NewButton = ParentMenu.AddPopup
        NewButton.Caption = MenuName
        NewButton.BeginGroup = BeginGroup
        Return NewButton
    End Function
    Private Function RegisterMenuItem(ByVal ParentMenu As IMenuContainer, ByVal MenuName As String, ByVal Action As Action, Optional ByVal Paramlist As String = "", Optional ByVal BeginGroup As Boolean = False) As IMenuButton
        Dim NewButton = ParentMenu.AddButton
        NewButton.Caption = MenuName
        NewButton.BeginGroup = BeginGroup
        AddHandler NewButton.Click, AddressOf New ActionExecutor(Action, Paramlist).ExecuteAction
        Return NewButton
    End Function
    Private Function RegisterMenuDelegateItem(ByVal ParentMenu As IMenuPopup, ByVal Caption As String, ByVal [Delegate] As System.Action, Optional ByVal BeginGroup As Boolean = False) As IMenuButton
        Dim NewButton = ParentMenu.AddButton
        NewButton.Caption = Caption
        NewButton.BeginGroup = BeginGroup
        AddHandler NewButton.Click, AddressOf [Delegate].Invoke
        Return NewButton
    End Function
    Private Function RegisterMenuLinkItem(ByVal ParentMenu As IMenuContainer, ByVal MenuName As String, ByVal Link As String, Optional ByVal BeginGroup As Boolean = False) As IMenuButton
        Dim NewButton = ParentMenu.AddButton
        NewButton.Caption = MenuName
        NewButton.BeginGroup = BeginGroup
        AddHandler NewButton.Click, AddressOf New LinkExecutor(Link).Launch
        Return NewButton
    End Function
#End Region
End Class

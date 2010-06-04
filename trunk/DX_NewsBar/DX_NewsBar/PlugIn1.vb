Option Infer On
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports DevExpress.CodeRush.Menus
Imports System.Runtime.CompilerServices
Imports System.Net
Imports System.Reflection

Public Class PlugIn1
    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()
        Call RefreshToolBar()
        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Private mMenuBar As MenuBar
    Private mMainItems As New List(Of IMenuControl)
    Private mRSS As IMenuPopup
    Private Sub RefreshToolBar()
        If mMenuBar IsNot Nothing Then
            mMenuBar.Delete()
            mMenuBar = Nothing
        End If
        mMenuBar = CodeRush.Menus.Bars.Add("NewsBar")
        mMenuBar.Visible = True
        mMenuBar.Position = BarPosition.Top
        Dim Refresh = mMenuBar.AddButton("Refresh").AddProc(AddressOf RefreshToolBar)
        'Dim Searchbox = mMenuBar.AddEdit()
        Dim DevExpress = mMenuBar.AddButton("X").AddGraphic("devexpress-logo.png").AddUrl("http://devexpress.com")
        Dim SearchButton = mMenuBar.AddButton("Search").AddUrl("http://search.devexpress.com")
        'SearchButton.AddGraphic("DX.png")

        mRSS = mMenuBar.AddPopup("RSS")
        Call RefreshRSSMenu()
        Dim Community = mMenuBar.AddPopup("Community")
        Community.AddButton("Blogs").AddUrl("http://community.devexpress.com/blogs/")
        Community.AddButton("Forums").AddUrl("http://community.devexpress.com/forums/")
        Community.AddButton("DevExpress Channel").AddUrl("http://tv.devexpress.com/")
        Community.AddButton("DevExpress On FaceBook", True).AddUrl("http://www.facebook.com/DevExpress")

        Dim Support = mMenuBar.AddPopup("Support")
        Support.AddButton("Search the Knowledge Base").AddUrl("http://search.devexpress.com/?p=T4|P6|0&d=447")
        Support.AddButton("Report a Bug", True).AddUrl("http://devexpress.com/Support/Center/CreateIssue.aspx?issuetype=BugReport")
        Support.AddButton("Make a Suggestion").AddUrl("http://devexpress.com/Support/Center/CreateIssue.aspx?issuetype=Suggestion")
        Support.AddButton("Ask a Question").AddUrl("http://devexpress.com/Support/Center/CreateIssue.aspx?issuetype=Question")
        Support.AddButton("My Issues", True).AddUrl("http://devexpress.com/Support/Center/MyIssues.aspx")
        Support.AddButton("Online Documentation", True).AddUrl("http://devexpress.com/Support/OnlineHelp.xml")

        Dim DevExpressCom = mMenuBar.AddPopup("DevExpress.com")
        DevExpressCom.AddButton("Buy Products or Upgrade").AddUrl("http://devexpress.com/Order/")
        DevExpressCom.AddButton("Download Demos and Evals").AddUrl("http://devexpress.com/Downloads/")
        DevExpressCom.AddButton("Download Registered Products").AddUrl("https://www.devexpress.com/ClientCenter/Default.aspx")
        DevExpressCom.AddButton("Case Studies", True).AddUrl("http://devexpress.com/Home/DeveloperStories/")
        DevExpressCom.AddButton("Customer Comments").AddUrl("http://devexpress.com/Home/Comments.xml")
        DevExpressCom.AddButton("Contact Us", True).AddUrl("http://devexpress.com/Home/ContactUs.xml")


    End Sub
    Private Sub RefreshRSSMenu()
        For Each Item In mRSS.ToList
            Item.Delete()
        Next
        mRSS.AddButton("Refresh").AddProc(AddressOf RefreshRSSMenu)
        Call AddRSSFeedPlusItems(mRSS, "News", "http://www.devexpress.com/rss/news/news20.xml", 20)
        Call AddRSSFeedPlusItems(mRSS, "DevExpress TV", "http://tv.devexpress.com/rss.ashx", 20)
        Call AddRSSFeedPlusItems(mRSS, "Forums", "http://community.devexpress.com/forums/aggregaterss.aspx", 20)
        Call AddRSSFeedPlusItems(mRSS, "Blogs", "http://community.devexpress.com/blogs/MainFeed.aspx", 20)
    End Sub
    Private Sub AddRSSFeedPlusItems(ByVal RSS As IMenuPopup, ByVal Caption As String, ByVal Url As String, ByVal Count As Integer)
        Call AddRSSItemsToPopup(RSS.AddPopup(Caption), Url, Count)
    End Sub

End Class
Public Module IMenuControlExt
    <Extension()> _
    Public Sub AddRSSItemsToPopup(ByVal Source As IMenuPopup, _
                                  ByVal RSSUrl As String, _
                                  ByVal Count As Integer)
        For Each Item In GetRSSItems(RSSUrl, Count)
            Source.AddButton(Item.<title>.Value).AddUrl(Item.<link>.Value)
        Next
    End Sub
    Private Function GetRSSItems(ByVal RssUrl As String, ByVal TopN As Integer) As IEnumerable(Of XElement)
        Using Client As New WebClient()
            Return XDocument.Parse(Client.DownloadString(RssUrl)).<rss>.<channel>.<item>.Take(TopN)
        End Using
    End Function

    '<Extension()> _
    'Public Function AddGraphic(ByVal Source As IMenuPopup, ByVal GraphicName As String) As IMenuButton
    '    Try
    '        If Source.Caption = "X" Then
    '            Source.Style = ButtonStyle.Icon
    '        Else
    '            Source.Style = ButtonStyle.IconAndCaption
    '        End If
    '        Dim Image As TransparentBitmap = GetBitmapByName(GraphicName)
    '        Source.SetFace(Image.Bitmap, Image.MaskBitmap)
    '        Return Source
    '    Catch ex As Exception

    '    End Try
    'End Function
    <Extension()> _
    Public Function AddGraphic(ByVal Source As IMenuButton, ByVal GraphicName As String) As IMenuButton
        Try
            If Source.Caption = "X" Then
                Source.Style = ButtonStyle.Icon
            Else
                Source.Style = ButtonStyle.IconAndCaption
            End If
            Dim Image As TransparentBitmap = GetBitmapByName(GraphicName)
            Source.SetFace(Image.Bitmap, Image.MaskBitmap)
            Return Source
        Catch ex As Exception

        End Try
    End Function

    Public Function GetBitmapByName(ByVal BitmapName As String) As TransparentBitmap
        Dim Asm As Assembly = Assembly.GetExecutingAssembly
        Dim stream As IO.Stream = Asm.GetManifestResourceStream(String.Format("DX_NewsBar.{0}", BitmapName))
        Dim BitmapFromStream As Bitmap = CType(Bitmap.FromStream(stream), Bitmap)
        Return New TransparentBitmap(BitmapFromStream)
    End Function
    <Extension()> _
    Public Function AddPopup(ByVal Parent As MenuBar, ByVal Caption As String) As IMenuPopup
        Try
            Dim MenuItem As IMenuControl
            MenuItem = Parent.AddPopup()
            MenuItem.Caption = Caption
            Return MenuItem
        Catch ex As Exception

        End Try
    End Function
    <Extension()> _
    Public Function AddPopup(ByVal Parent As IMenuPopup, ByVal Caption As String) As IMenuPopup
        Try
            Dim MenuItem As IMenuControl
            MenuItem = Parent.AddPopup()
            MenuItem.Caption = Caption
            Return MenuItem
        Catch ex As Exception

        End Try
    End Function
    <Extension()> _
    Public Function AddCtrl(ByVal Parent As MenuBar, ByVal ControlTypeEdit As ControlType) As IMenuControl
        Try
            Return Parent.Add(ControlTypeEdit)
        Catch ex As Exception

        End Try
    End Function
    <Extension()> _
    Public Function AddButton(ByVal Parent As MenuBar, ByVal Caption As String) As IMenuButton
        Try
            Dim Button = Parent.AddButton()
            Button.Caption = Caption
            Button.TooltipText = ""
            Return Button
        Catch ex As Exception

        End Try
    End Function

    <Extension()> _
    Public Function AddButton(ByVal Parent As IMenuPopup, ByVal Caption As String, Optional ByVal StartsGroup As Boolean = False) As IMenuButton
        Try
            Dim Button = Parent.AddButton()
            Button.Caption = Caption
            Button.BeginGroup = StartsGroup
            Return Button
        Catch ex As Exception

        End Try
    End Function
    <Extension()> _
    Public Function AddProc(ByVal Source As IMenuButton, ByVal Action As System.Action) As IMenuButton
        AddHandler Source.Click, AddressOf Action.Invoke
        Return Source
    End Function
    <Extension()> _
    Public Function AddUrl(ByVal Source As IMenuButton, ByVal Url As String) As IMenuButton
        Source.AddProc(AddressOf New UrlWrapper(Url).Visit)
        Return Source
    End Function

End Module
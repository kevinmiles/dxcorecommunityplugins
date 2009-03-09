Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Text
Imports System.IO

Public Class CR_SortCodePlugIn

    Private Shared _layoutFile As String
    Public Shared Property LayoutFile() As String
        Get
            Return _layoutFile
        End Get
        Set(ByVal value As String)
            _layoutFile = value
        End Set
    End Property
    'DXCore-generated code...
#Region " InitializePlugIn "
    Public Overrides Sub InitializePlugIn()
        MyBase.InitializePlugIn()

        'TODO: Add your initialization code here.
    End Sub
#End Region
#Region " FinalizePlugIn "
    Public Overrides Sub FinalizePlugIn()
        'TODO: Add your finalization code here.

        MyBase.FinalizePlugIn()
    End Sub
#End Region

    Dim FilterList As List(Of NodeFilter) = New List(Of NodeFilter)()
    Dim Regions As New List(Of Object)
    Private LayoutList As List(Of Object)
    Private _GlobalOtherFilter As NodeFilter = Nothing
    Private Function getSpecials(ByVal dr As DataRow) As String
        Dim tmpSpecials As String = ""
        If CType(dr("Abstract"), Boolean) = True Then
            tmpSpecials += "Abstract"
        End If
        If CType(dr("Extern"), Boolean) = True Then
            tmpSpecials += "Extern"
        End If
        If CType(dr("Overload"), Boolean) = True Then
            tmpSpecials += "Overloads"
        End If
        If CType(dr("Override"), Boolean) = True Then
            tmpSpecials += "Override"
        End If
        If CType(dr("Virtual"), Boolean) = True Then
            tmpSpecials += "Virtual"
        End If
        Return tmpSpecials
    End Function

    Private Function getSpecials(ByVal theMember As Member) As String
        Dim tmpSpecials As String = ""
        If theMember.IsAbstract Then
            tmpSpecials += "Abstract"
        End If
        If theMember.IsExtern Then
            tmpSpecials += "Extern"
        End If
        If theMember.IsOverloads Then
            tmpSpecials += "Overloads"
        End If
        If theMember.IsOverride Then
            tmpSpecials += "Override"
        End If
        If theMember.IsVirtual Then
            tmpSpecials += "Virtual"
        End If
        Return tmpSpecials
    End Function

    Private Sub FindFilter(ByVal layoutList As List(Of Object), ByVal Visibility As String, ByVal memberType As String, ByVal memberName As String, ByVal specials As String)
        Static FilterScore As Integer
        Dim currentScore As Integer = 0
        If layoutList Is Nothing Then
            thefilter = Nothing
        End If
        If thefilter Is Nothing Then
            FilterScore = 0
        End If
        If FilterScore = 20 Then
            Return
        End If
        For Each item As Object In layoutList
            currentScore  = 0
            If TypeOf item Is NodeFilter Then
                Dim theNodeFilter As NodeFilter = CType(item, NodeFilter)
                If theNodeFilter.Visibility = Visibility Then
                    currentScore += 5
                ElseIf theNodeFilter.Visibility = "" orelse theNodeFilter.Visibility ="All" Then
                    currentScore += 1
                End If
                If theNodeFilter.NodeType = memberType Then
                    currentScore += 5
                ElseIf theNodeFilter.NodeType = "" Then
                    currentScore += 1
                End If
                If (memberName = theNodeFilter.MemberName) Then
                    currentScore += 5
                ElseIf theNodeFilter.MemberName = "" Then
                    currentScore += 1
                End If
                If specials <> "" AndAlso specials = theNodeFilter.Specials Then
                    currentScore += 5
                ElseIf theNodeFilter.Specials = "" Then
                    currentScore += 1
                End If
                If currentScore > FilterScore Then
                    thefilter = theNodeFilter
                    FilterScore = currentScore
                End If
            End If
            If TypeOf item Is LayoutNode Then
                Dim theRegion As LayoutNode = CType(item, LayoutNode)
                FindFilter(theRegion.LayoutList, Visibility, memberType, memberName, specials)
            End If
        Next
    End Sub

    Private _Code As StringBuilder = New StringBuilder()

    Private Sub AddComment(ByVal description As String, ByVal level As Integer)
        Dim comment As String = CodeRush.Documents.ActiveTextDocument.LanguageExtension.SingleLineCommentBegin & description
        _Code.AppendLine(comment.PadLeft(comment.Length + level,vbtab))
        _Code.AppendLine()
    End Sub

    Private Sub GenerateCode(ByVal theDocument As TextDocument, ByVal layoutList As List(Of Object), ByVal initialGeneration As Boolean)
        Static Level As Integer=0
        If initialGeneration Then
            Level=0
        Else
            Level+=1
        End If
        For Each item As Object In layoutList
            If TypeOf item Is LayoutNode AndAlso DirectCast(item, LayoutNode).HasCode = True Then
                Dim theRegion As LayoutNode = CType(item, LayoutNode)
                If Not theRegion.IsComment Then
                    AddRegionHeader(theRegion.Description)
                Else
                    AddComment( theRegion.Description,level)
                End If
                GenerateCode(theDocument, theRegion.LayoutList, False)
                If Not theRegion.IsComment Then
                    AddRegionFooter()
                End If
            ElseIf TypeOf item Is NodeFilter Then
                Dim theFilter As NodeFilter = CType(item, NodeFilter)
                theFilter.GenerateCode(theDocument, _Code)
            End If
        Next
        Level-=1
    End Sub

    Private Function GetMemberType(ByVal themember As Member) As String
        If TypeOf themember Is [Const] Then
            Return "Const"
        ElseIf TypeOf themember Is DelegateDefinition Then
            Return "Delegate"
        ElseIf TypeOf themember Is Variable Then
            Return "Variable"
        ElseIf TypeOf themember Is Method Then
            Dim theMethod As Method = CType(themember, Method)
            If theMethod.IsConstructor Then
                Return "Constructor"
            End If
            Return "Method"
        ElseIf TypeOf themember Is [Property] Then
            Return "Property"
        ElseIf TypeOf themember Is [Event] Then
            Return "Event"
        End If
        Return "Unknown"
    End Function


    Private thefilter As CR_SortCode.NodeFilter

    Private Sub SortClass(ByVal TheType As TypeDeclaration, Optional ByVal showDialog As Boolean = False)
        RemoveRegions(CodeRush.Source.ActiveFileNode, TheType)
        Dim ActiveDocument As TextDocument = CodeRush.Documents.ActiveTextDocument
        For Each theMember As Member In TheType.AllMembers
            If Not ShouldSkipMember(theMember) Then
                thefilter = Nothing
                Dim memberType As String
                memberType = GetMemberType(theMember)

                FindFilter(LayoutList, theMember.Visibility.ToString, memberType, theMember.Name, getSpecials(theMember))
                If thefilter Is Nothing Then
                    thefilter = _GlobalOtherFilter
                End If
                If Not thefilter Is Nothing Then
                    Dim theSortMember As CR_SortCode.SortMember
                    theSortMember = New SortMember(theMember)
                    thefilter.AddMember(theSortMember)
                    ActiveDocument.QueueDelete(theMember.GetFullBlockCutRange())
                End If
            End If
        Next
        StartCodeGeneration(TheType, ActiveDocument)
        If showDialog Then
            MessageBox.Show("Done")
        End If
    End Sub



    Private Sub SortFile()
        Dim theDocument As TextDocument = CodeRush.Documents.ActiveTextDocument
        theDocument.ParseIfTextChanged()
        Dim treeEnum As New SourceTreeEnumerator(CodeRush.Source.ActiveFileNode)
        PopulateFilters()
        While treeEnum.MoveNext
            Dim theNode As Object
            theNode = treeEnum.Current
            If TypeOf theNode Is Comment Then
                If LayoutNode._CommentList.Contains(DirectCast(theNode, Comment).ToString) Then
                    theDocument.QueueDelete(theNode)
                End If
            End If
        End While
        theDocument.ApplyQueuedEdits("Organize Members Part 1")

        Dim oldText As String
        oldText = theDocument.Text
        theDocument.SetText(theDocument.StartPoint.Line, theDocument.StartPoint.LineCharOffset, theDocument.EndPoint.Line, theDocument.EndPoint.LineCharOffset, oldText)
        theDocument.ParseIfNeeded()
        treeEnum = New SourceTreeEnumerator(CodeRush.Source.ActiveFileNode)
        While treeEnum.MoveNext
            Dim theNode As Object
            theNode = treeEnum.Current
            If TypeOf theNode Is TypeDeclaration Then
                PopulateFilters()
                SortClass(theNode, False)
            End If
        End While

        theDocument.ApplyQueuedEdits("Organize Members Part 2")

        Dim newtext As String
        newtext = ""
        Dim splitText As String()
        splitText = theDocument.Text.Split(vbCrLf)
        For j As Integer = 0 To splitText.Length - 2 Step 1
            If Not (splitText(j) = vbLf AndAlso splitText(j + 1) = vbLf) Then
                newtext += splitText(j) & vbCrLf
            End If
        Next

        Clipboard.SetText(newtext.Replace(vbLf + vbLf, vbLf))

        theDocument.Save()

        theDocument.Format()
        Dim mydte As EnvDTE.DTE
        mydte = CodeRush.ApplicationObject
        mydte.ActiveDocument.Selection.SelectAll()
        mydte.ActiveDocument.Selection.Delete()
        mydte.ExecuteCommand("Edit.Paste")
        Try
            Dim regions As IEnumerable
            regions = DirectCast(CodeRush.Source.ActiveFileNode, SourceFile).AllRegions
            For Each item As System.Object In regions
                DirectCast(item, RegionDirective).Collapse()
            Next
        Catch ex As Exception

        End Try
    End Sub
    Private Sub SortCodeAction_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles SortCodeAction.Execute
        Try
            Dim saveCursor As Cursor = Cursor.Current
            Try
                Cursor.Current = Cursors.WaitCursor
                If Not SortCodeOptions.Storage.ValueExists("SortCode", "LayoutFile") Then
                    MessageBox.Show("Please go to the settings page and select the layout file that will be used to sort the code")
                    Return
                Else
                    Dim filename As String
                    filename = SortCodeOptions.Storage.ReadString("SortCode", "LayoutFile")
                    If Not File.Exists(filename) Then
                        MessageBox.Show(String.Format("The file {0} does not exists. Please go to the options page and fix it.", filename))
                        Return
                    End If
                    LayoutFile = filename
                End If

                SortFile()
                MessageBox.Show("Done")
            Finally
                Cursor.Current = saveCursor
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub StartCodeGeneration(ByVal TheType As TypeDeclaration, ByVal theDocument As TextDocument)
        _Code = New StringBuilder
        GenerateCode(theDocument, LayoutList, True)
        Dim start As SourcePoint = TheType.BlockCodeRange.Start
        start = theDocument.GetStartEmptyLinePoint(start)
        Dim newcode As String
        newcode = _Code.ToString()
        Dim resultRange As QueuedInsert
        resultRange = theDocument.QueueInsert(start, newcode)
    End Sub

    Private Sub AddRegionHeader(ByVal headerName As String)
        _Code.AppendLine(CodeRush.Language.GetRegionHeader(headerName))
        _Code.AppendLine()
    End Sub
    Private Sub AddRegionFooter()
        _Code.AppendLine(CodeRush.Language.GetRegionFooter)
        _Code.AppendLine()
    End Sub


    Private Function ShouldSkipMember(ByVal member As Member) As Boolean
        If member Is Nothing Then
            Return True
        End If
        If Not (TypeOf member Is Variable) Then
            Return False
        End If
        Dim var As Variable = DirectCast(member, Variable)
        If Not var.IsInDeclarationList Then
            Return False
        End If
        Return Not var.IsStart
    End Function

    Private AllNodes As Dictionary(Of String, Object)

    Private Sub AddRegion(ByVal ParentID As String, ByVal ID As String, ByVal Description As String, ByVal IsComment As Boolean)
        Dim ParentRegion As CR_SortCode.LayoutNode = Nothing
        Dim tmpRegion As CR_SortCode.LayoutNode
        tmpRegion = New LayoutNode(ID, Description, IsComment)
        If ParentID <> Guid.Empty.ToString Then
            ParentRegion = AllNodes(ParentID)
            ParentRegion.LayoutList.Add(tmpRegion)
        Else
            LayoutList.Add(tmpRegion)
        End If
        AllNodes.Add(ID, tmpRegion)
    End Sub

    Private Sub AddFilter(ByVal ParentID As String, ByVal ID As String, ByVal Visibility As String, ByVal nodeType As String, ByVal membername As String, ByVal specials As String)
        Dim ParentRegion As CR_SortCode.LayoutNode = Nothing
        Dim tmpFilter As CR_SortCode.NodeFilter
        tmpFilter = New NodeFilter(Visibility, nodeType, membername, specials)
        If Visibility = "All" AndAlso nodeType = "" AndAlso membername = "" Then
            _GlobalOtherFilter = tmpFilter
        End If
        If ParentID <> Guid.Empty.ToString Then
            ParentRegion = AllNodes(ParentID)
            ParentRegion.LayoutList.Add(tmpFilter)
        Else
            LayoutList.Add(tmpFilter)
        End If
        AllNodes.Add(ID, tmpFilter)
    End Sub

    Private Sub PopulateFilters()
        Dim ds As DataSet = New DataSet()
        ds.ReadXml(LayoutFile, XmlReadMode.ReadSchema)
        LayoutList = New List(Of Object)
        LayoutNode._CommentList.Clear()
        'Dim NewRegion As LayoutNode = Nothing

        AllNodes = New Dictionary(Of String, Object)()
        For Each dr As DataRow In ds.Tables(0).Rows
            If dr("Type") = "Region" OrElse dr("Type") = "Comment" Then
                AddRegion(dr("ParentID"), dr("ID"), dr("Name"), dr("Type") = "Comment")
            Else
                Dim specials As String
                specials = getSpecials(dr)
                AddFilter(dr("ParentID"), dr("ID"), dr("Visibility"), dr("Type"), dr("Name"), specials)
            End If
        Next

    End Sub

    Private Shared Sub RemoveRegions(ByVal theSourceFile As SourceFile, ByVal theCLass As TypeDeclaration)
        If theSourceFile Is Nothing Then
            Return
        End If
        Dim regions As IEnumerable
        regions = theSourceFile.AllRegions
        For Each item As System.Object In regions
            If DirectCast(item, RegionDirective).StartLine > theCLass.StartLine AndAlso DirectCast(item, RegionDirective).StartLine < theCLass.EndLine Then
                CodeRush.Documents.ActiveTextDocument.QueueDelete(New SourceRange(DirectCast(item, RegionDirective).StartLine, 1, DirectCast(item, RegionDirective).StartLine, CodeRush.Documents.ActiveTextDocument.GetLineLength(DirectCast(item, RegionDirective).StartLine) + 1))
                CodeRush.Documents.ActiveTextDocument.QueueDelete(New SourceRange(DirectCast(item, RegionDirective).EndLine, 1, DirectCast(item, RegionDirective).EndLine, CodeRush.Documents.ActiveTextDocument.GetLineLength(DirectCast(item, RegionDirective).EndLine) + 1))
            End If
        Next
        CodeRush.Documents.ActiveTextDocument.ApplyQueuedEdits("Delete regions")
    End Sub

End Class

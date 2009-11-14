Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Collections
Imports System.IO
Imports System.Reflection
Imports System.GACManagedAccess


Namespace Refactor_Resolve
    ''' <summary>
    ''' Summary description for Refactor_ResolvePlugIn.
    ''' </summary>
    Public Class Refactor_ResolvePlugIn
        Inherits StandardPlugIn
        Private mnuColCaseInsensitive As ArrayList
        Private mnuCol As ArrayList
        Friend WithEvents ResolveAction As DevExpress.CodeRush.Core.Action
        Private _usingMenu As New System.Windows.Forms.ContextMenu

#Region " private fields... "
        Private components As System.ComponentModel.IContainer
        Private _ChangedStartLine As Integer = -1
        Private _ChangedEndLine As Integer = -1
        Private Shared _isEnabled As Boolean
        Private Shared _enhanced As Boolean
        Friend WithEvents QuickResolveAction As DevExpress.CodeRush.Core.Action
        Private Shared _ignoreCase As Boolean

#End Region

#Region "Private types"
        Private Structure NameSpaceItem
            Public NameSpaceName As String
            Public NewName As String
            Public IsGAC As Boolean
            Public Dependencies As ArrayList
        End Structure
#End Region

#Region "Public properties"

        Public Shared Property IgnoreCase() As Boolean
            Get
                Return _ignoreCase
            End Get
            Set(ByVal Value As Boolean)
                _ignoreCase = Value
            End Set
        End Property

        Public Shared Property Enhanced() As Boolean
            Get
                Return _enhanced
            End Get
            Set(ByVal Value As Boolean)
                _enhanced = Value
            End Set
        End Property

        Public Shared Property IsEnabled() As Boolean
            Get
                Return _isEnabled
            End Get
            Set(ByVal Value As Boolean)
                _isEnabled = Value
            End Set
        End Property
#End Region
        ' constructor...
#Region " Refactor_ResolvePlugIn "
        Public Sub New()
            InitializeComponent()
            Dim opt As String
            'TODO: Add your initialization code here.
            If OptRefactor_Resolve.Storage.ValueExists("Resolve", "Options") Then
                opt = OptRefactor_Resolve.Storage.ReadString("Resolve", "Options")
                If String.Compare(opt.Substring(0, 1), "1", False) = 0 Then
                    IsEnabled = True
                Else
                    IsEnabled = False
                End If
                If String.Compare(opt.Substring(1, 1), "1", False) = 0 Then
                    Enhanced = True
                Else
                    Enhanced = False
                End If
                If String.Compare(opt.Substring(2, 1), "1", False) = 0 Then
                    IgnoreCase = True
                Else
                    IgnoreCase = False
                End If
            Else
                IsEnabled = True
                Enhanced = True
                IgnoreCase = True
            End If
            AddHandler ResolveProvider.CheckAvailability, AddressOf ResolveProvider_CheckAvailability
        End Sub
#End Region

        ' CodeRush-generated code
#Region " InitializePlugIn "
        Public Overrides Sub InitializePlugIn()
            MyBase.InitializePlugIn()

            '
            ' TODO: Add your initialization code here.
            ''
            AddHandler AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve, AddressOf LoadOnDemand
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
        Friend WithEvents ResolveProvider As DevExpress.Refactor.Core.RefactoringProvider
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Refactor_ResolvePlugIn))
            Me.ResolveProvider = New DevExpress.Refactor.Core.RefactoringProvider(Me.components)
            Me.ResolveAction = New DevExpress.CodeRush.Core.Action(Me.components)
            Me.QuickResolveAction = New DevExpress.CodeRush.Core.Action(Me.components)
            CType(Me.ResolveProvider, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ResolveAction, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.QuickResolveAction, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'ResolveProvider
            '
            Me.ResolveProvider.ActionHintText = ""
            Me.ResolveProvider.AutoActivate = True
            Me.ResolveProvider.AutoUndo = False
            Me.ResolveProvider.Description = "Try to resove the unknown type and add the namspace"
            Me.ResolveProvider.DisplayName = "Resolve"
            Me.ResolveProvider.Image = CType(resources.GetObject("ResolveProvider.Image"), System.Drawing.Bitmap)
            Me.ResolveProvider.NeedsSelection = False
            Me.ResolveProvider.ProviderName = "Refactor_Resolve"
            Me.ResolveProvider.Register = True
            Me.ResolveProvider.SupportsAsyncMode = False
            '
            'ResolveAction
            '
            Me.ResolveAction.ActionName = "RefactorResolveAction"
            Me.ResolveAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
            Me.ResolveAction.Description = "Try to resove the unknown type and add the namspace"
            Me.ResolveAction.Image = CType(resources.GetObject("ResolveAction.Image"), System.Drawing.Bitmap)
            Me.ResolveAction.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
            '
            'QuickResolveAction
            '
            Me.QuickResolveAction.ActionName = "QuickResolveAction"
            Me.QuickResolveAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
            Me.QuickResolveAction.Description = "Add the most possible namespace to the using (imports) statements."
            Me.QuickResolveAction.Image = Nothing
            Me.QuickResolveAction.ImageBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(254, Byte), Integer), CType(CType(0, Byte), Integer))
            '
            'Refactor_ResolvePlugIn
            '
            CType(Me.ResolveProvider, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ResolveAction, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.QuickResolveAction, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
#End Region

#Region "Private functions"

        Private Shared Function GetTheElement() As LanguageElement
            Dim le As LanguageElement
            If CodeRush.Documents.ActiveTextDocument.GetText(CodeRush.Caret.SourcePoint.Line, CodeRush.Caret.SourcePoint.Offset - 1, CodeRush.Caret.SourcePoint.Line, CodeRush.Caret.SourcePoint.Offset) = " " Then
                le = CodeRush.Documents.ActiveTextDocument.GetNodeBefore(CodeRush.Caret.SourcePoint) 'CodeRush.Source.Active
            Else
                le = CodeRush.Source.Active
            End If
            Return le
        End Function
        Private Function IsAvailable() As Boolean
            CodeRush.Source.ParseIfTextChanged(CodeRush.Documents.ActiveTextDocument)

            Dim le As LanguageElement
            le = GetTheElement()

            If le Is Nothing OrElse (Not TypeOf le Is TypeReferenceExpression AndAlso Not TypeOf le Is ElementReferenceExpression AndAlso Not TypeOf le Is Statement) Then
                Return False
            End If
            Return HasProblem(le)
        End Function

        Private Sub ApplyFix()
            Dim le As LanguageElement = CodeRush.Source.Active
            ResolveForElement(le)
        End Sub

        Private Sub GatherNamespaces(ByVal le As LanguageElement)
            mnuCol = New ArrayList
            mnuColCaseInsensitive = New ArrayList
            Dim nameSpaceItem As NameSpaceItem
            Dim prj As ProjectElement = CodeRush.Source.ActiveProject
            Dim theNodelist As NodeList = prj.AssemblyReferences
            If CodeRush.ApplicationObject.Version.StartsWith("7") Then
                theNodelist.Add(New AssemblyReference("mscorlib.dll"))
            End If
            For Each leRef As AssemblyReference In theNodelist
                Dim lowername As Object
                If IgnoreCase Then
                    lowername = le.Name.ToLower
                Else
                    lowername = le.Name
                End If
                Dim theTypeName As String
                Dim theTypeNameToLower As String

                If Not leRef.IsProjectReference Then
                    Dim asm As System.Reflection.Assembly
                    Try
                        If leRef.FilePath = "mscorlib.dll" Then
                            asm = System.Reflection.Assembly.Load(leRef.FilePath)
                        Else
                            asm = GetAssemblyFromFile(leRef.FilePath)
                        End If
                        For Each theType As Type In asm.GetTypes
                            If theType.IsPublic Then
                                theTypeName = theType.Name
                                theTypeNameToLower = theTypeName.ToLower
                                If theTypeName = le.Name OrElse (IgnoreCase AndAlso theTypeNameToLower = lowername) Then 'OrElse (theType.Name.EndsWith(le.Name)) OrElse (theType.Name.StartsWith(le.Name)) Then
                                    nameSpaceItem = New NameSpaceItem
                                    nameSpaceItem.NameSpaceName = theType.Namespace
                                    If Not (theTypeName = le.Name) Then
                                        nameSpaceItem.NewName = theTypeName
                                    End If

                                    mnuCol.Add(nameSpaceItem)
                                ElseIf (Enhanced AndAlso theTypeNameToLower.IndexOf(lowername) > -1) Then
                                    nameSpaceItem = New NameSpaceItem
                                    nameSpaceItem.NameSpaceName = theType.Namespace
                                    nameSpaceItem.NewName = theTypeName
                                    mnuColCaseInsensitive.Add(nameSpaceItem)
                                End If
                            End If
                        Next
                    Catch
                        'ignore
                    Finally
                        asm = Nothing
                    End Try
                Else
                    Dim theProj As ProjectElement = GetProject(prj.Solution, leRef.SourceProjectFullName)
                    Dim res As ArrayList = GetLanguageElementNameSpaces(theProj)
                    For Each strNameSpace As String In res
                        If Not strNameSpace.IndexOf(".") > -1 Then
                            strNameSpace = "." + strNameSpace
                        End If
                        theTypeName = strNameSpace.Substring(strNameSpace.LastIndexOf(".") + 1)

                        theTypeNameToLower = theTypeName.ToLower
                        If (theTypeName = le.Name) OrElse (IgnoreCase AndAlso theTypeNameToLower = lowername) Then
                            Dim strNameSpaceFound As String = strNameSpace.Substring(0, strNameSpace.LastIndexOf("."))
                            nameSpaceItem = New NameSpaceItem
                            nameSpaceItem.NameSpaceName = strNameSpaceFound
                            If Not (theTypeName = le.Name) Then
                                nameSpaceItem.NewName = theTypeName
                            End If
                            mnuCol.Add(nameSpaceItem)
                        ElseIf (Enhanced AndAlso theTypeNameToLower.IndexOf(lowername) > -1) Then
                            Dim strNameSpaceFound As String = strNameSpace.Substring(0, strNameSpace.LastIndexOf("."))
                            nameSpaceItem = New NameSpaceItem
                            nameSpaceItem.NameSpaceName = strNameSpaceFound
                            nameSpaceItem.NewName = theTypeName
                            mnuColCaseInsensitive.Add(nameSpaceItem)
                        End If
                    Next
                End If
            Next
            If mnuCol.Count = 0 Then
                If MessageBox.Show("Could not resolve element, continue searching the GAC?", "Resolve", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                    GatherNamespacesFromGAC(le)
                End If
            End If
        End Sub

        Private Function GetDependencies(ByVal theType As Type) As ArrayList
            Dim results As New ArrayList
            Dim basetype As Type = theType.BaseType
            While Not basetype Is Nothing
                If Not results.Contains(basetype.Assembly.FullName) AndAlso Not basetype.Assembly.FullName.ToLower.StartsWith("mscorlib") Then
                    results.Add(basetype.Assembly.FullName)
                End If
                basetype = basetype.BaseType
            End While
            Return results
        End Function
        Private Sub GatherNamespacesFromGAC(ByVal le As LanguageElement)
            Dim nameSpaceItem As NameSpaceItem
            Dim prj As ProjectElement = CodeRush.Source.ActiveProject
            Dim lowername As Object
            If IgnoreCase Then
                lowername = le.Name.ToLower
            Else
                lowername = le.Name
            End If

            Dim e As New System.GACManagedAccess.AssemblyCacheEnum(Nothing)
            Dim asmName As String = e.GetNextAssembly()
            Dim asm As System.Reflection.Assembly
            While Not asmName Is Nothing
                Try
                    If asmName.ToLower.StartsWith("system.") Then
                        asm = Assembly.ReflectionOnlyLoad(asmName)
                        If Not asm Is Nothing Then
                            For Each theType As Type In asm.GetTypes
                                Dim theTypeName As String
                                Dim theTypeNameToLower As String
                                If theType.IsPublic Then
                                    theTypeName = theType.Name
                                    theTypeNameToLower = theTypeName.ToLower
                                    If theTypeName = le.Name OrElse (IgnoreCase AndAlso theTypeNameToLower = lowername) Then 'OrElse (theType.Name.EndsWith(le.Name)) OrElse (theType.Name.StartsWith(le.Name)) Then
                                        nameSpaceItem = New NameSpaceItem
                                        nameSpaceItem.NameSpaceName = theType.Namespace
                                        If Not (theTypeName = le.Name) Then
                                            nameSpaceItem.NewName = theTypeName
                                        End If
                                        nameSpaceItem.IsGAC = True
                                        nameSpaceItem.Dependencies = GetDependencies(theType)
                                        mnuCol.Add(nameSpaceItem)
                                        CodeRush.Project.Active.AddReference(asm.FullName)
                                        If Not Enhanced Then
                                            Return
                                        End If
                                    ElseIf (Enhanced AndAlso theTypeNameToLower.IndexOf(lowername) > -1) Then
                                        nameSpaceItem = New NameSpaceItem
                                        nameSpaceItem.NameSpaceName = theType.Namespace
                                        nameSpaceItem.NewName = theTypeName
                                        nameSpaceItem.Dependencies = GetDependencies(theType)
                                        mnuColCaseInsensitive.Add(nameSpaceItem)
                                        Return
                                    End If
                                End If
                            Next
                        End If
                    End If

                Catch ex As Exception
                End Try
                asmName = e.GetNextAssembly()
            End While
        End Sub

        Private Function LoadOnDemand(ByVal sender As Object, ByVal args As ResolveEventArgs) As Assembly
            Return System.Reflection.Assembly.ReflectionOnlyLoad(args.Name)
        End Function

        Private Function GetAssemblyFromFile(ByVal LeRefFilePath As String) As Assembly
            Dim fs As FileStream
            Try
                fs = New FileStream(LeRefFilePath, FileMode.Open, FileAccess.Read)
                Dim b(fs.Length) As Byte
                fs.Read(b, 0, fs.Length)
                Dim asm As Assembly
                asm = System.Reflection.Assembly.Load(b)
                Return asm
            Finally
                fs.Dispose()
            End Try
        End Function


        Private Sub ResolveForElement(ByVal le As LanguageElement, Optional ByVal refreshNamespases As Boolean = True)
            Dim doc As TextDocument = CodeRush.Documents.ActiveTextDocument
            Dim nameSpaceItem As NameSpaceItem
            CodeRush.Source.ParseIfNeeded(doc)
            If Not doc Is Nothing Then
                RemoveMenuEventhandlers()
                _usingMenu.MenuItems.Clear()
                If le Is Nothing Then
                    Return
                End If
                If refreshNamespases Then
                    GatherNamespaces(le)
                End If
                If mnuCol.Count = 0 AndAlso mnuColCaseInsensitive.Count = 0 Then
                    MessageBox.Show("Nothing found!")
                    Return
                End If
                For Each nameSpaceItem In mnuCol
                    AddMenuItem(le, nameSpaceItem)
                Next
                If mnuCol.Count > 0 Then
                    _usingMenu.MenuItems.Add("-")
                End If
                For Each nameSpaceItem In mnuCol
                    AddMenuItem(le, nameSpaceItem, False)
                Next

                If mnuColCaseInsensitive.Count > 0 Then
                    _usingMenu.MenuItems.Add("-")
                End If

                For Each nameSpaceItem In mnuColCaseInsensitive
                    AddMenuItem(le, nameSpaceItem)
                Next

                If mnuColCaseInsensitive.Count > 0 Then
                    _usingMenu.MenuItems.Add("-")
                End If

                For Each nameSpaceItem In mnuColCaseInsensitive
                    AddMenuItem(le, nameSpaceItem, False)
                Next

                CodeRush.TextViews.Active.ShowMenu(_usingMenu, CodeRush.TextViews.Active.GetRectangleFromLanguageElement(le).X, CodeRush.TextViews.Active.GetRectangleFromLanguageElement(le).Y + CodeRush.TextViews.Active.LineHeight)
            End If
        End Sub

        Private Sub AddMenuItem(ByVal le As LanguageElement, ByVal nameSpaceItem As NameSpaceItem, Optional ByVal AddNameSpace As Boolean = True)
            Dim usingName As String
            If CodeRush.Documents.ActiveTextDocument.Language = "Basic" Then
                usingName = "Add 'Imports "
            Else
                usingName = "Add 'Using "
            End If
            Dim mnuItem As New PluginMenuitem
            If AddNameSpace Then
                If nameSpaceItem.NewName <> "" Then
                    mnuItem.Text = usingName & nameSpaceItem.NameSpaceName & "' statement and rename '" & le.Name & "' to '" & nameSpaceItem.NewName & "'"
                    mnuItem.NewName = nameSpaceItem.NewName
                Else
                    mnuItem.Text = usingName & nameSpaceItem.NameSpaceName & "' statement"
                End If
                mnuItem.AddNameSpace = True
            Else
                If nameSpaceItem.NewName <> "" Then
                    mnuItem.Text = "Change '" & le.Name & "' to '" & nameSpaceItem.NameSpaceName & "." & nameSpaceItem.NewName & "'"
                    mnuItem.NewName = nameSpaceItem.NewName
                Else
                    mnuItem.Text = "Change '" & le.Name & "' to '" & nameSpaceItem.NameSpaceName & "." & le.Name & "'"
                End If
                mnuItem.AddNameSpace = False
            End If
            mnuItem.theElement = le
            mnuItem.TheNameSpace = nameSpaceItem.NameSpaceName
            mnuItem.Tag = nameSpaceItem
            _usingMenu.MenuItems.Add(mnuItem)
            AddHandler mnuItem.Click, AddressOf handleMenuClick
        End Sub

        Private Sub RemoveMenuEventhandlers()
            For Each mnuItem As MenuItem In _usingMenu.MenuItems
                RemoveHandler mnuItem.Click, AddressOf handleMenuClick
            Next
        End Sub

        Private Sub handleMenuClick(ByVal sender As Object, ByVal e As EventArgs)
            RemoveMenuEventhandlers()
            Dim newCode As String
            Dim doc As TextDocument = CodeRush.Documents.ActiveTextDocument
            Dim theMenu As PluginMenuitem = DirectCast(sender, PluginMenuitem)
            Dim startline As Integer = theMenu.theElement.StartLine
            Dim startOffset As Integer = theMenu.theElement.StartOffset
            If theMenu.NewName <> "" Then
                newCode = theMenu.NewName
                doc.QueueReplace(theMenu.theElement.NameRange, newCode)
                doc.ApplyQueuedEdits()
            End If
            If Not theMenu.AddNameSpace Then
                If Not doc Is Nothing Then
                    newCode = theMenu.TheNameSpace + "."
                    doc.QueueInsert(New SourcePoint(startline, startOffset), newCode)
                    doc.ApplyQueuedEdits()
                End If
            Else
                newCode = theMenu.TheNameSpace
                Dim test As SortedList
                test = CodeRush.Source.NamespaceReferences()
                CodeRush.Source.DeclareNamespaceReference(newCode)
            End If
            Dim deps As ArrayList = DirectCast(theMenu.Tag, NameSpaceItem).Dependencies
            For Each depString As String In deps
                CodeRush.Project.Active.AddReference(depString)
            Next
            If Not CodeRush.TextViews.Active Is Nothing Then
                CodeRush.TextViews.Active.Invalidate()
                _ChangedStartLine = -1
                _ChangedEndLine = -1
            End If
        End Sub

        Private Sub GetProjectNameSpaces(ByVal theLanguageElement As LanguageElement, ByVal theList As ArrayList)
            Dim tempListProject As ArrayList
            Dim prj As ProjectElement = DirectCast(theLanguageElement, ProjectElement)
            For Each filenode As LanguageElement In prj.AllFiles
                If TypeOf filenode Is SourceFile Then
                    If Not filenode.Document Is Nothing Then
                        CodeRush.Source.ParseIfTextChanged(filenode.Document)
                    End If
                    tempListProject = GetLanguageElementNameSpaces(filenode)
                    For Each strNameSpace As String In tempListProject
                        If Not theList.Contains(strNameSpace) Then
                            theList.Add(strNameSpace)
                        End If
                    Next
                End If
            Next
        End Sub

        Private Sub GetReferenceNameSpaces(ByVal theLanguageElement As LanguageElement, ByVal theList As ArrayList)
            Dim typesCol As DevExpress.CodeRush.StructuralParser.LanguageElementCollection
            typesCol = DevExpress.CodeRush.StructuralParser.SourceModelUtils.GetTypes(theLanguageElement)
            If Not typesCol Is Nothing Then
                For Each le As LanguageElement In typesCol
                    If (TypeOf le Is [Class] AndAlso DirectCast(le, [Class]).Visibility = MemberVisibility.Public) OrElse (TypeOf le Is Struct AndAlso DirectCast(le, Struct).Visibility = MemberVisibility.Public) Then
                        Dim tempList As ArrayList
                        Dim strFullNameSpace As String = GetFullnameSpace(le)
                        If le.Project.RootNamespace <> "" Then
                            If strFullNameSpace <> "" Then
                                strFullNameSpace = le.Project.RootNamespace + "." + strFullNameSpace
                            Else
                                strFullNameSpace = le.Project.RootNamespace + "."
                            End If
                        End If
                        If Not theList.Contains(strFullNameSpace) Then
                            theList.Add(strFullNameSpace)
                        End If
                        tempList = GetLanguageElementNameSpaces(le)
                        For Each strNameSpace As String In tempList
                            If Not theList.Contains(strNameSpace) Then
                                theList.Add(strNameSpace)
                            End If
                        Next
                    End If

                Next
            End If
        End Sub

        Private Sub GetChildNameSpaceNameSpaces(ByVal theList As ArrayList, ByVal namespacesCol As DevExpress.CodeRush.StructuralParser.LanguageElementCollection)
            For Each leNamesSpace As LanguageElement In namespacesCol
                Dim tempList As ArrayList
                tempList = GetLanguageElementNameSpaces(leNamesSpace)
                For Each strNameSpace As String In tempList
                    If Not theList.Contains(strNameSpace) Then
                        theList.Add(strNameSpace)
                    End If
                Next
            Next
        End Sub

        Private Function GetLanguageElementNameSpaces(ByVal theLanguageElement As LanguageElement) As ArrayList
            Dim theList As ArrayList = New ArrayList
            Dim namespacesCol As DevExpress.CodeRush.StructuralParser.LanguageElementCollection
            namespacesCol = DevExpress.CodeRush.StructuralParser.SourceModelUtils.GetNamespaces(theLanguageElement)
            If TypeOf theLanguageElement Is ProjectElement Then
                GetProjectNameSpaces(theLanguageElement, theList)
            End If
            If TypeOf theLanguageElement Is [Namespace] OrElse TypeOf theLanguageElement Is [Class] OrElse TypeOf theLanguageElement Is SourceFile Then
                GetReferenceNameSpaces(theLanguageElement, theList)
            End If
            If Not namespacesCol Is Nothing Then
                GetChildNameSpaceNameSpaces(theList, namespacesCol)
            End If
            Return theList
        End Function

        Private Function GetProject(ByVal sol As SolutionElement, ByVal projectFile As String) As ProjectElement
            Dim e As IEnumerator = sol.AllProjects.GetEnumerator
            While e.MoveNext
                If DirectCast(e.Current, ProjectElement).FullName = projectFile Then
                    Return DirectCast(e.Current, ProjectElement)
                End If
            End While
            Return Nothing
        End Function

        Private Function GetFullnameSpace(ByVal le As LanguageElement) As String
            Try
                Dim strNameSpace As String = ""
                If TypeOf le Is [Namespace] OrElse TypeOf le Is [Class] OrElse TypeOf le Is [Struct] Then
                    strNameSpace = le.Name
                End If
                If Not le.Parent Is Nothing AndAlso (TypeOf le.Parent Is [Namespace] OrElse TypeOf le.Parent Is [Class]) Then
                    If strNameSpace = "" Then
                        strNameSpace = GetFullnameSpace(le.Parent)
                    Else
                        strNameSpace = GetFullnameSpace(le.Parent) + "." + strNameSpace
                    End If
                Else
                    Dim parentLe As LanguageElement = Nothing

                    If Not le.GetClass Is Nothing Then
                        parentLe = le.GetClass
                    ElseIf Not le.GetStruct Is Nothing Then
                        parentLe = le.GetStruct
                    End If
                    If Not parentLe Is Nothing AndAlso (parentLe.Range.Start.Line <> le.Range.Start.Line OrElse parentLe.Range.Start.Offset <> le.Range.Start.Offset) Then
                        If strNameSpace = "" Then
                            strNameSpace = GetFullnameSpace(parentLe)
                        Else
                            strNameSpace = GetFullnameSpace(parentLe) + "." + strNameSpace
                        End If
                    End If
                End If
                Return strNameSpace
            Catch ex As Exception

            End Try
            Return ""
        End Function
#End Region

        Private Sub ResolveAction_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles ResolveAction.Execute
            If CodeRush.Documents.ActiveTextDocument Is Nothing Then
                Return
            End If
            Try
                If IsAvailable() Then
                    ApplyFix()
                End If
            Catch ex As Exception

            End Try
        End Sub
        Private Function HasProblem(ByVal theElement As LanguageElement) As Boolean
            Dim declarationElement As IElement
            If theElement Is Nothing OrElse theElement.NodeCount > 0 OrElse (Not TypeOf theElement Is Statement AndAlso ((theElement.NameRange.Start.Line <> theElement.Range.Start.Line OrElse theElement.NameRange.Start.Offset <> theElement.Range.Start.Offset) OrElse (Not TypeOf theElement Is TypeReferenceExpression AndAlso Not TypeOf theElement Is ElementReferenceExpression AndAlso Not TypeOf theElement Is DevExpress.CodeRush.StructuralParser.Attribute) OrElse (Not theElement.InsideClass AndAlso Not theElement.InsideStruct AndAlso Not theElement.InsideNamespace AndAlso Not TypeOf theElement Is DevExpress.CodeRush.StructuralParser.Attribute) OrElse theElement.Name.ToLower = "object")) Then
                Return False
            End If
            declarationElement = theElement.GetDeclaration

            If declarationElement Is Nothing Then
                Return True
            End If
        End Function

        Private Sub Refactor_ResolvePlugIn_EditorPaintLanguageElement(ByVal ea As DevExpress.CodeRush.Core.EditorPaintLanguageElementEventArgs) Handles MyBase.EditorPaintLanguageElement
            Try
                If Not IsEnabled Then
                    Return
                End If
                CodeRush.Source.ParseIfTextChanged(CodeRush.Documents.ActiveTextDocument)

                Dim theElement As LanguageElement = ea.LanguageElement
                If HasProblem(theElement) Then
                    ea.PaintArgs.DrawLine(theElement.NameRange.Start.Line, theElement.NameRange.Start.Offset, theElement.NameRange.End.Offset - theElement.NameRange.Start.Offset, Color.BlueViolet, LineStyle.SolidUnderline)
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Function ResolveType(ByVal active As IElement) As IElement
            Dim resolver As ISourceTreeResolver = ParserServices.SourceTreeResolver
            If TypeOf active Is IMemberElement Then
                Return resolver.ResolveElementType(active)
            Else
                Return Nothing
            End If
        End Function

        Private Sub Refactor_ResolvePlugIn_TextChanged(ByVal ea As DevExpress.CodeRush.Core.TextChangedEventArgs) Handles MyBase.TextChanged
            If Not IsEnabled Then
                Return
            End If
            If ea.ChangeType = TextChangeType.Insertion OrElse ea.ChangeType = TextChangeType.Replacement Then
                _ChangedStartLine = ea.Insertion.Start.Line
                _ChangedEndLine = ea.Insertion.End.Line
            ElseIf ea.ChangeType = TextChangeType.Deletion Then
                _ChangedStartLine = ea.Deletion.Start.Line
                _ChangedEndLine = ea.Deletion.End.Line
            End If
        End Sub

        Private Sub Refactor_ResolvePlugIn_CaretMoved(ByVal ea As DevExpress.CodeRush.Core.CaretMovedEventArgs) Handles MyBase.CaretMoved
            If Not IsEnabled OrElse CodeRush.Documents.ActiveTextDocument Is Nothing Then
                Return
            End If
            If (_ChangedStartLine <> -1) AndAlso ((ea.OldPosition.Line >= _ChangedStartLine OrElse ea.OldPosition.Line <= _ChangedEndLine) AndAlso ea.OldPosition.Line <> ea.NewPosition.Line) Then
                If Not CodeRush.TextViews.Active Is Nothing Then
                    CodeRush.Source.ParseIfTextChanged(CodeRush.Documents.ActiveTextDocument)
                    CodeRush.TextViews.Active.Invalidate()
                    _ChangedStartLine = -1
                    _ChangedEndLine = -1
                End If
            End If
        End Sub

        

        Private Sub ResolveProvider_Apply(ByVal sender As Object, ByVal ea As DevExpress.Refactor.Core.ApplyRefactoringEventArgs) Handles ResolveProvider.Apply
            Try
                If IsAvailable() Then
                    ApplyFix()
                End If
            Catch ex As Exception

            End Try
        End Sub

        Private Sub QuickResolveAction_Execute(ByVal ea As DevExpress.CodeRush.Core.ExecuteEventArgs) Handles QuickResolveAction.Execute
            Dim theElement As LanguageElement = GetTheElement() 'CodeRush.Source.Active
            Dim oldEnhanced As Boolean = Enhanced
            Try
                Enhanced = False
                If IsAvailable() Then
                    Dim Doc As TextDocument
                    Doc = CodeRush.Documents.ActiveTextDocument
                    GatherNamespaces(theElement)
                    Dim newCode As Object
                    If mnuCol.Count > 0 Then
                        If mnuCol.Count = 1 Then
                            newCode = DirectCast(mnuCol(0), NameSpaceItem).NewName
                            If Not newCode Is Nothing Then
                                Doc.QueueReplace(theElement.Range, newCode)
                                Doc.ApplyQueuedEdits(String.Format("Resolve {0}", newCode))
                            End If
                            newCode = DirectCast(mnuCol(0), NameSpaceItem).NameSpaceName
                            CodeRush.Source.DeclareNamespaceReference(newCode)
                            Dim deps As ArrayList = DirectCast(mnuCol(0), NameSpaceItem).Dependencies
                            If Not deps Is Nothing Then
                                For Each depString As String In deps
                                    Try
                                        CodeRush.Project.Active.AddReference(depString)
                                    Catch ex As Exception
                                        'ignore
                                    End Try
                                Next
                            End If

                            If Not CodeRush.TextViews.Active Is Nothing Then
                                CodeRush.TextViews.Active.Invalidate()
                                _ChangedStartLine = -1
                                _ChangedEndLine = -1
                            End If
                        Else
                            ResolveForElement(theElement, False)
                        End If

                    Else
                        MessageBox.Show("Could not resolve")
                    End If

                End If
            Finally
                Enhanced = oldEnhanced
            End Try
        End Sub



    Private Sub ResolveProvider_CheckAvailability( ByVal sender As System.Object,  ByVal ea As DevExpress.CodeRush.Core.CheckContentAvailabilityEventArgs) 
            If IsAvailable() Then
                ea.Available = RefactoringAvailability.Available
            End If
End Sub
    End Class
End Namespace
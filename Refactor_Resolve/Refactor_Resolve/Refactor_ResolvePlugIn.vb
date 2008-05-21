Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser
Imports System.Collections

Namespace Refactor_Resolve
    ''' <summary>
    ''' Summary description for Refactor_ResolvePlugIn.
    ''' </summary>
    Public Class Refactor_ResolvePlugIn
        Inherits StandardPlugIn
        Friend WithEvents ReloverProvider As DevExpress.Refactor.Core.RefactoringProvider
        Friend WithEvents ResolveAction As DevExpress.CodeRush.Core.Action
        Private _usingMenu As New System.Windows.Forms.ContextMenu

#Region " private fields... "
        Private components As System.ComponentModel.IContainer
        Private _ChangedStartLine As Integer = -1
        Private _ChangedEndLine As Integer = -1
        Private Shared _isEnabled As Boolean
        Private Shared _enhanced As Boolean
        Private Shared _ignoreCase As Boolean


#End Region

#Region "Private types"
        Private Structure NameSpaceItem
            Public NameSpaceName As String
            Public NewName As String
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
            ''' <summary>
            ''' Required for Windows.Forms Class Composition Designer support
            ''' </summary>
            InitializeComponent()
            Dim opt As String
            'TODO: Add your initialization code here.
            If OptRefactor_Resolve.Storage.ValueExists("Resolve", "Options") Then
                opt = OptRefactor_Resolve.Storage.ReadString("Resolve", "Options")
                If opt.Substring(0, 1) = "1" Then
                    IsEnabled = True
                Else
                    IsEnabled = False
                End If
                If opt.Substring(1, 1) = "1" Then
                    Enhanced = True
                Else
                    Enhanced = False
                End If
                If opt.Substring(2, 1) = "1" Then
                    IgnoreCase = True
                Else
                    IgnoreCase = False
                End If
            Else
                IsEnabled = True
                Enhanced = True
                IgnoreCase = True
            End If
        End Sub
#End Region

        ' CodeRush-generated code
#Region " InitializePlugIn "
        Public Overrides Sub InitializePlugIn()
            MyBase.InitializePlugIn()

            '
            ' TODO: Add your initialization code here.
            '
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
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Dim resources As System.Resources.ResourceManager = New System.Resources.ResourceManager(GetType(Refactor_ResolvePlugIn))
            Me.ReloverProvider = New DevExpress.Refactor.Core.RefactoringProvider(Me.components)
            Me.ResolveAction = New DevExpress.CodeRush.Core.Action(Me.components)
            CType(Me.ReloverProvider, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.ResolveAction, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            '
            'ReloverProvider
            '
            Me.ReloverProvider.ActionHintText = ""
            Me.ReloverProvider.AutoActivate = True
            Me.ReloverProvider.AutoUndo = False
            Me.ReloverProvider.Description = "Try to resove the unknown type and add the namspace"
            Me.ReloverProvider.DisplayName = "Resolve"
            Me.ReloverProvider.ProviderName = ""
            Me.ReloverProvider.Register = True
            '
            'ResolveAction
            '
            Me.ResolveAction.ActionName = "RefactorResolve"
            Me.ResolveAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None
            Me.ResolveAction.Description = "Try to resove the unknown type and add the namspace"
            Me.ResolveAction.Image = CType(resources.GetObject("ResolveAction.Image"), System.Drawing.Bitmap)
            Me.ResolveAction.ImageBackColor = System.Drawing.Color.FromArgb(CType(0, Byte), CType(254, Byte), CType(0, Byte))
            CType(Me.ReloverProvider, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.ResolveAction, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

        End Sub
#End Region


#Region "Private functions"

        Private Function IsAvailable() As Boolean
            CodeRush.Source.ParseIfTextChanged(CodeRush.Documents.ActiveTextDocument)
            Dim le As LanguageElement = CodeRush.Source.Active
            If le Is Nothing OrElse (Not TypeOf le Is TypeReferenceExpression AndAlso Not TypeOf le Is ElementReferenceExpression) Then
                Return False
            End If
            Dim theElement As LanguageElement = le
            Dim declarationElement As IElement
            Dim elementnameSpace As String = GetFullnameSpace(theElement)
            declarationElement = theElement.GetDeclaration
            If declarationElement Is Nothing Then
                Return True
            Else
                If TypeOf theElement Is TypeReferenceExpression Then
                    Dim fullSignature As String = DirectCast(theElement, TypeReferenceExpression).FullSignature
                    Dim fullName As String = declarationElement.FullName
                    Dim declarationNameSpace As String
                    If fullName <> fullSignature Then
                        declarationNameSpace = fullName.Substring(0, fullName.LastIndexOf("."))
                    Else
                        declarationNameSpace = elementnameSpace
                    End If
                    Dim namespaces As SortedList = CodeRush.Source.NamespaceReferences
                    If declarationNameSpace <> "System" AndAlso Not fullName = fullSignature AndAlso declarationNameSpace <> elementnameSpace AndAlso Not namespaces.ContainsValue(fullName) AndAlso Not namespaces.ContainsValue(declarationNameSpace) Then
                        Return True
                    End If
                End If
            End If
        End Function

        Private Sub ApplyFix()
            Dim le As LanguageElement = CodeRush.Source.Active
            ResolveForElement(le)
        End Sub

        Private Sub ResolveForElement(ByVal le As LanguageElement)
            Dim mnuCol As New ArrayList
            Dim mnuColCaseInsensitive As New ArrayList
            Dim doc As TextDocument = CodeRush.Documents.ActiveTextDocument
            Dim nameSpaceItem As NameSpaceItem
            CodeRush.Source.ParseIfNeeded(doc)
            If Not doc Is Nothing Then
                RemoveMenuEventhandlers()
                _usingMenu.MenuItems.Clear()
                If le Is Nothing Then
                    Return
                End If
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
                                asm = System.Reflection.Assembly.LoadFrom(leRef.FilePath)
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
                        Catch ex As Exception
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
                Dim le As LanguageElement = theMenu.theElement
                If Not doc Is Nothing Then
                    newCode = theMenu.TheNameSpace + "."
                    doc.QueueInsert(New SourcePoint(startline, startOffset), newCode)
                    doc.ApplyQueuedEdits()
                End If
            Else
                newCode = theMenu.TheNameSpace
                CodeRush.Source.DeclareNamespaceReference(newCode)
            End If
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

        Private Sub GerReferenceNameSpaces(ByVal theLanguageElement As LanguageElement, ByVal theList As ArrayList)
            Dim typesCol As DevExpress.CodeRush.StructuralParser.LanguageElementCollection
            typesCol = DevExpress.CodeRush.StructuralParser.SourceModelUtils.GetTypes(theLanguageElement)
            If Not typesCol Is Nothing Then
                For Each le As LanguageElement In typesCol
                    If (TypeOf le Is [Class] AndAlso DirectCast(le, [Class]).Visibility = MemberVisibility.Public) OrElse (TypeOf le Is Struct AndAlso DirectCast(le, Struct).Visibility = MemberVisibility.Public) Then
                        Dim tempList As ArrayList
                        Dim strFullNameSpace As String = GetFullnameSpace(le)
                        Dim strprojNS As String = ""
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
            Dim typesCol As DevExpress.CodeRush.StructuralParser.LanguageElementCollection
            namespacesCol = DevExpress.CodeRush.StructuralParser.SourceModelUtils.GetNamespaces(theLanguageElement)
            If TypeOf theLanguageElement Is ProjectElement Then
                GetProjectNameSpaces(theLanguageElement, theList)
            End If
            If TypeOf theLanguageElement Is [Namespace] OrElse TypeOf theLanguageElement Is [Class] OrElse TypeOf theLanguageElement Is SourceFile Then
                GerReferenceNameSpaces(theLanguageElement, theList)
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
            End If
            Return strNameSpace
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


        Private Sub Refactor_ResolvePlugIn_EditorPaintLanguageElement(ByVal ea As DevExpress.CodeRush.Core.EditorPaintLanguageElementEventArgs) Handles MyBase.EditorPaintLanguageElement
            If Not IsEnabled Then
                Return
            End If
            Dim theElement As LanguageElement = ea.LanguageElement
            Dim declarationElement As IElement
            If theElement Is Nothing OrElse (Not TypeOf theElement Is TypeReferenceExpression AndAlso Not TypeOf theElement Is ElementReferenceExpression) Then
                Return
            End If
            Dim elementnameSpace As String = GetFullnameSpace(ea.LanguageElement)
            If Not theElement.Project Is Nothing AndAlso theElement.Project.RootNamespace <> "" Then
                Dim tempnameSpace As String = theElement.Project.RootNamespace & "." & elementnameSpace
                elementnameSpace = tempnameSpace.Substring(0, tempnameSpace.Length - elementnameSpace.Length - 1)
            End If
            declarationElement = theElement.GetDeclaration
            If declarationElement Is Nothing Then
                ea.PaintArgs.DrawLine(theElement.NameRange.Start.Line, theElement.NameRange.Start.Offset, theElement.NameRange.End.Offset - theElement.NameRange.Start.Offset, Color.BlueViolet, LineStyle.SolidUnderline)
            Else
                If TypeOf theElement Is TypeReferenceExpression Then
                    Dim fullSignature As String = DirectCast(theElement, TypeReferenceExpression).FullSignature
                    Dim fullName As String = declarationElement.FullName
                    Dim declarationNameSpace As String
                    If fullName <> fullSignature Then
                        declarationNameSpace = fullName.Substring(0, fullName.LastIndexOf("."))
                    Else
                        declarationNameSpace = elementnameSpace
                    End If
                    Dim namespaces As SortedList = CodeRush.Source.NamespaceReferences
                    If declarationNameSpace <> "System" AndAlso Not fullName = fullSignature AndAlso declarationNameSpace <> elementnameSpace AndAlso Not namespaces.ContainsValue(fullName) AndAlso Not namespaces.ContainsValue(declarationNameSpace) Then
                        ea.PaintArgs.DrawLine(theElement.NameRange.Start.Line, theElement.NameRange.Start.Offset, theElement.NameRange.End.Offset - theElement.NameRange.Start.Offset, Color.BlueViolet, LineStyle.SolidUnderline)
                    End If
                End If
            End If
        End Sub

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


        Private Sub ReloverProvider_CheckAvailability(ByVal sender As Object, ByVal ea As DevExpress.Refactor.Core.CheckAvailabilityEventArgs) Handles ReloverProvider.CheckAvailability
            If IsAvailable() Then
                ea.Availability = RefactoringAvailability.Available
            End If
        End Sub


        Private Sub ReloverProvider_Apply(ByVal sender As Object, ByVal ea As DevExpress.Refactor.Core.ApplyRefactoringEventArgs) Handles ReloverProvider.Apply
            Try
                ApplyFix()
            Catch ex As Exception

            End Try
        End Sub
    End Class
End Namespace

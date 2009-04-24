Imports System
Imports System.ComponentModel
Imports System.Drawing
Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports DevExpress.CodeRush.Core
Imports DevExpress.CodeRush.PlugInCore
Imports DevExpress.CodeRush.StructuralParser

Namespace CR_EventHandlerCheckTC
	''' <summary>
	''' Summary description for EventHandlerCheckTCWindow.
	''' </summary>
	<Guid("f6f5e1e5-ce02-4846-a894-11e352448732"), _
	 Title("Check EventHandlers Try-Catch statements")> _
	Public Class EventHandlerCheckTCWindow
		Inherits ToolWindowPlugIn
        Private bh As New BlockHighlighter

#Region " private fields... "
		Friend WithEvents events_ As DevExpress.DXCore.PlugInCore.DXCoreEvents
		''' <summary>
		''' Required designer variable.
		''' </summary>
        Private components As System.ComponentModel.IContainer
        Private _nbrDocs As Integer
        Private _activeDoc As Integer
#End Region

		' constructor...
#Region " EventHandlerCheckTCWindow "
		Public Sub New()
			' This call is required by the Windows.Forms Form Designer.
			InitializeComponent()
		End Sub
#End Region

		' DXCore-generated code
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

#Region " ShowWindow "
		''' <summary>
		''' Opens and displays this tool window. If the tool window is already open,
		''' it will be focused.
		''' </summary>
		Public Shared Function ShowWindow() As EnvDTE.Window
			Return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Show(GetType(EventHandlerCheckTCWindow).GUID)
		End Function
#End Region
#Region " HideWindow "
		''' <summary>
		''' Hides this tool window if it is open.
		''' </summary>
		Public Shared Function HideWindow() As EnvDTE.Window
			Return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Hide(GetType(EventHandlerCheckTCWindow).GUID)
		End Function
#End Region

#Region " Instance "
		''' <summary>
		''' Returns the created instance of this tool window if it is open. If the tool window
		''' is not open in Visual Studio, this property returns null.
		''' </summary>
		Public Shared ReadOnly Property Instance() As EventHandlerCheckTCWindow
			Get
				Return CType(DevExpress.CodeRush.Core.CodeRush.ToolWindows.GetPlugInControl(GetType(EventHandlerCheckTCWindow)), EventHandlerCheckTCWindow)
			End Get
		End Property
#End Region

#Region " Component Designer generated code "
		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
        Friend WithEvents btnCheck As System.Windows.Forms.Button
        Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
        Friend WithEvents rbFile As System.Windows.Forms.RadioButton
        Friend WithEvents rbProject As System.Windows.Forms.RadioButton
        Friend WithEvents btnFix As System.Windows.Forms.Button
        Friend WithEvents Label1 As System.Windows.Forms.Label
        Friend WithEvents txtMethodName As System.Windows.Forms.TextBox
        Friend WithEvents lvHandlers As System.Windows.Forms.ListView
        Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
        Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
        Friend WithEvents btnSelectAll As System.Windows.Forms.Button
        Friend WithEvents btnUncheck As System.Windows.Forms.Button
        Private Sub InitializeComponent()
            Me.components = New System.ComponentModel.Container
            Me.events_ = New DevExpress.DXCore.PlugInCore.DXCoreEvents(Me.components)
            Me.btnCheck = New System.Windows.Forms.Button
            Me.GroupBox1 = New System.Windows.Forms.GroupBox
            Me.rbProject = New System.Windows.Forms.RadioButton
            Me.rbFile = New System.Windows.Forms.RadioButton
            Me.btnFix = New System.Windows.Forms.Button
            Me.Label1 = New System.Windows.Forms.Label
            Me.txtMethodName = New System.Windows.Forms.TextBox
            Me.lvHandlers = New System.Windows.Forms.ListView
            Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
            Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
            Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
            Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
            Me.btnSelectAll = New System.Windows.Forms.Button
            Me.btnUncheck = New System.Windows.Forms.Button
            CType(Me.events_, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.GroupBox1.SuspendLayout()
            CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'btnCheck
            '
            Me.btnCheck.Location = New System.Drawing.Point(16, 8)
            Me.btnCheck.Name = "btnCheck"
            Me.btnCheck.Size = New System.Drawing.Size(104, 23)
            Me.btnCheck.TabIndex = 1
            Me.btnCheck.Text = "Start Check"
            '
            'GroupBox1
            '
            Me.GroupBox1.Controls.Add(Me.rbProject)
            Me.GroupBox1.Controls.Add(Me.rbFile)
            Me.GroupBox1.Location = New System.Drawing.Point(248, 8)
            Me.GroupBox1.Name = "GroupBox1"
            Me.GroupBox1.Size = New System.Drawing.Size(128, 56)
            Me.GroupBox1.TabIndex = 2
            Me.GroupBox1.TabStop = False
            Me.GroupBox1.Text = "Scope"
            '
            'rbProject
            '
            Me.rbProject.Location = New System.Drawing.Point(56, 24)
            Me.rbProject.Name = "rbProject"
            Me.rbProject.Size = New System.Drawing.Size(64, 24)
            Me.rbProject.TabIndex = 1
            Me.rbProject.Text = "Project"
            '
            'rbFile
            '
            Me.rbFile.Checked = True
            Me.rbFile.Location = New System.Drawing.Point(8, 24)
            Me.rbFile.Name = "rbFile"
            Me.rbFile.Size = New System.Drawing.Size(48, 24)
            Me.rbFile.TabIndex = 0
            Me.rbFile.TabStop = True
            Me.rbFile.Text = "File"
            '
            'btnFix
            '
            Me.btnFix.Location = New System.Drawing.Point(16, 40)
            Me.btnFix.Name = "btnFix"
            Me.btnFix.Size = New System.Drawing.Size(104, 23)
            Me.btnFix.TabIndex = 3
            Me.btnFix.Text = "Fix checked items"
            '
            'Label1
            '
            Me.Label1.Location = New System.Drawing.Point(384, 8)
            Me.Label1.Name = "Label1"
            Me.Label1.Size = New System.Drawing.Size(184, 31)
            Me.Label1.TabIndex = 4
            Me.Label1.Text = "Method to use in catch statement.  Example: LogException(ex)"
            '
            'txtMethodName
            '
            Me.txtMethodName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.txtMethodName.Location = New System.Drawing.Point(384, 40)
            Me.txtMethodName.Name = "txtMethodName"
            Me.txtMethodName.Size = New System.Drawing.Size(280, 20)
            Me.txtMethodName.TabIndex = 5
            '
            'lvHandlers
            '
            Me.lvHandlers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                        Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.lvHandlers.CheckBoxes = True
            Me.lvHandlers.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4})
            Me.lvHandlers.FullRowSelect = True
            Me.lvHandlers.GridLines = True
            Me.lvHandlers.Location = New System.Drawing.Point(8, 72)
            Me.lvHandlers.Name = "lvHandlers"
            Me.lvHandlers.Size = New System.Drawing.Size(704, 152)
            Me.lvHandlers.TabIndex = 6
            Me.lvHandlers.UseCompatibleStateImageBehavior = False
            Me.lvHandlers.View = System.Windows.Forms.View.Details
            '
            'ColumnHeader1
            '
            Me.ColumnHeader1.Text = "Method"
            Me.ColumnHeader1.Width = 200
            '
            'ColumnHeader2
            '
            Me.ColumnHeader2.Text = "Called directly"
            '
            'ColumnHeader3
            '
            Me.ColumnHeader3.Text = "AddHandler"
            '
            'ColumnHeader4
            '
            Me.ColumnHeader4.Text = "File"
            Me.ColumnHeader4.Width = 150
            '
            'btnSelectAll
            '
            Me.btnSelectAll.Location = New System.Drawing.Point(136, 8)
            Me.btnSelectAll.Name = "btnSelectAll"
            Me.btnSelectAll.Size = New System.Drawing.Size(104, 23)
            Me.btnSelectAll.TabIndex = 7
            Me.btnSelectAll.Text = "Check all"
            '
            'btnUncheck
            '
            Me.btnUncheck.Location = New System.Drawing.Point(136, 40)
            Me.btnUncheck.Name = "btnUncheck"
            Me.btnUncheck.Size = New System.Drawing.Size(104, 23)
            Me.btnUncheck.TabIndex = 8
            Me.btnUncheck.Text = "UnCheck all"
            '
            'EventHandlerCheckTCWindow
            '
            Me.Controls.Add(Me.btnUncheck)
            Me.Controls.Add(Me.btnSelectAll)
            Me.Controls.Add(Me.lvHandlers)
            Me.Controls.Add(Me.txtMethodName)
            Me.Controls.Add(Me.Label1)
            Me.Controls.Add(Me.btnFix)
            Me.Controls.Add(Me.GroupBox1)
            Me.Controls.Add(Me.btnCheck)
            Me.Name = "EventHandlerCheckTCWindow"
            Me.Size = New System.Drawing.Size(720, 232)
            CType(Me.events_, System.ComponentModel.ISupportInitialize).EndInit()
            Me.GroupBox1.ResumeLayout(False)
            CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
#End Region

		' protected methods...
#Region " Dispose "
		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
			If (disposing) Then
				If Not components Is Nothing Then
					components.Dispose()
				End If
			End If

			MyBase.Dispose(disposing)
		End Sub
#End Region

        Private Sub btnCheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheck.Click
            CodeRush.Documents.ActiveTextDocument.ParseIfNeeded()
            Dim saveCursor As Cursor = Cursor.Current
            Try
                Cursor.Current = Cursors.WaitCursor
                lvHandlers.Items.Clear()
                Select Case True
                    Case rbFile.Checked
                        checkfile()
                    Case rbProject.Checked
                        checkProject()
                        'Case rbSolution.Checked
                        '    checkSolution()

                End Select
                If lvHandlers.Items.Count = 0 Then
                    MessageBox.Show("Found no eventhandlers without Try-Catch statements.")
                Else
                    MessageBox.Show(String.Format("Found {0} eventhandlers without Try-Catch statements.", lvHandlers.Items.Count))
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            Finally
                Cursor.Current = saveCursor
            End Try
        End Sub

        Private Sub checkProject()
            _nbrDocs = CodeRush.Source.ActiveProject.AllFilesCount()
            For i As Integer = 0 To CodeRush.Source.ActiveProject.AllFilesCount - 1
                _activeDoc = i + 1
                checkfile(CodeRush.Source.ActiveProject.AllFiles(i))
            Next i
        End Sub

        Private Sub checkfile()
            Dim doc As TextDocument = CodeRush.Documents.Active
            _nbrDocs = 1
            _activeDoc = 1
            checkfile(doc.FileNode)
        End Sub
        Private Sub checkfile(ByVal doc As LanguageElement)
            Dim lEnum As SourceTreeEnumerator = New SourceTreeEnumerator(doc, ElementFilters.Method)
            Dim nbrElements As Integer = 0
            Dim elementCounter As Integer = 0
            While lEnum.MoveNext()
                nbrElements += 1
            End While
            lEnum.Reset()
            While lEnum.MoveNext()
                elementCounter += 1
                Dim statBarText As String
                statBarText = String.Format("Processing doc {0}/{1}, element {2}/{3}", _activeDoc, _nbrDocs, elementCounter, nbrElements)
                DirectCast(CodeRush.ApplicationObject, EnvDTE.DTE).StatusBar.Progress(True, statBarText, elementCounter, nbrElements)
                If DirectCast(lEnum.Current, IElement).ElementType = LanguageElementType.Method Then
                    Dim theMethod As Method = DirectCast(lEnum.Current, Method)
                    Dim MethodCalledDirectly As Boolean = False
                    Dim IsMethodUsedInAddHandler As Boolean
                    If theMethod.IsEventHandler Then
                        If Not MethodHasTryCatch(theMethod) Then
                            IsMethodUsedInAddHandler = MethodUsedInAddHandler(MethodCalledDirectly, theMethod)
                            AddMethodToList(MethodCalledDirectly, IsMethodUsedInAddHandler, theMethod)
                        End If
                    ElseIf Not MethodHasTryCatch(theMethod) Then
                        IsMethodUsedInAddHandler = MethodUsedInAddHandler(MethodCalledDirectly, theMethod)
                        If IsMethodUsedInAddHandler Then
                            AddMethodToList(MethodCalledDirectly, IsMethodUsedInAddHandler, theMethod)
                        End If
                    End If
                End If
            End While
        End Sub

        Private Function MethodUsedInAddHandler(ByRef MethodCalledDirectly As Boolean, ByVal theMethod As Method) As Boolean
            Dim refs As IElementCollection = theMethod.FindAllReferences
            Dim UsedInAddHanlder As Boolean = False
            For Each IElement As IElement In refs
                If Not IElement.Parent Is Nothing AndAlso IElement.Parent.ElementType = LanguageElementType.AddressOfExpression Then
                    Dim dec As IElement
                    dec = IElement.Parent.PreviousSibling.GetDeclaration
                    If dec Is Nothing OrElse dec.ElementType = LanguageElementType.Event Then
                        UsedInAddHanlder = True
                    End If
                ElseIf IElement.ElementType = LanguageElementType.MethodCall OrElse IElement.ElementType = LanguageElementType.MethodCallExpression OrElse IElement.ElementType = LanguageElementType.MethodReferenceExpression Then
                    MethodCalledDirectly = True
                End If
            Next
            Return UsedInAddHanlder
        End Function

        Private Sub AddMethodToList(ByVal IsMethodCalledDirectly As Boolean, ByVal MethodInAddHandler As Boolean, ByVal theMethod As Method)
            Dim item As New ListItem(theMethod, IsMethodCalledDirectly, MethodInAddHandler)
            Dim litem As ListViewItem = New ListViewItem(theMethod.Name)
            litem.Tag = item
            litem.SubItems.Add(IsMethodCalledDirectly.ToString)
            litem.SubItems.Add(MethodInAddHandler.ToString)
            litem.SubItems.Add(New IO.FileInfo(theMethod.FileNode.Name).Name)
            lvHandlers.Items.Add(litem)
        End Sub

        Private Function MethodHasTryCatch(ByVal theMethod As Method) As Boolean
            Dim lEnumMethod As SourceTreeEnumerator = New SourceTreeEnumerator(theMethod)
            While lEnumMethod.MoveNext
                If TypeOf lEnumMethod.Current Is [Catch] Then
                    Return True
                End If
            End While
            Return False
        End Function


        Private Sub btnFix_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFix.Click
            Try
                Dim doc As TextDocument
                If txtMethodName.Text.Trim = "" Then
                    MessageBox.Show("Please enter the name of the method to use in the catch statemen,t first.")
                    Return
                End If
                For i As Integer = 0 To lvHandlers.Items.Count - 1
                    If lvHandlers.Items(i).Checked Then
                        Dim lstItem As ListItem = OpenFileForItem(i)
                        If lstItem Is Nothing Then
                            MessageBox.Show("The code has changed, please run the analysis again")
                            Return
                        End If
                        Dim theMethod As Method = lstItem.theElement
                        If doc Is Nothing Then
                            doc = theMethod.Document
                        End If
                        If Not doc Is theMethod.Document Then
                            doc.ApplyQueuedEdits(String.Format("AutoFix Try-Catch ({0})", doc.Name))
                            doc = theMethod.Document
                        End If
                        Dim theRange As SourceRange
                        theRange = theMethod.GetBlockCodeRange(False)
                        Dim nextNode As LanguageElement
                        nextNode = doc.GetNodeAt(theRange.Start.Line, theRange.Start.Offset)
                        If Not nextNode Is Nothing AndAlso nextNode.ElementType = LanguageElementType.Comment Then
                            theRange = New SourceRange(nextNode.EndLine + 1, 1, theRange.End.Line, theRange.End.Offset)
                        End If
                        Dim tryCatchCode As String = GenerateTryCatchBlock(doc.GetText(theRange))
                        doc.QueueReplace(theRange, tryCatchCode)
                    End If
                Next i
                If Not doc Is Nothing Then
                    doc.ApplyQueuedEdits(String.Format("AutoFix Try-Catch ({0})", doc.Name))
                    doc.Format
                    lvHandlers.Items.Clear()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try
        End Sub
        Private Function GenerateTryCatchBlock(ByVal theCode As String) As String
            Dim myBuilder As ElementBuilder
            myBuilder = New ElementBuilder
            Dim tryBlock As [Try]
            Dim CatchBlock As [Catch]
            tryBlock = myBuilder.AddTry(Nothing)
            myBuilder.AddSnippetCodeElement(tryBlock, theCode & Environment.NewLine)
            CatchBlock = myBuilder.AddCatch(Nothing, "Exception", "ex")
            myBuilder.AddMethodCall(CatchBlock, txtMethodName.Text, Nothing)
            Return myBuilder.GenerateCode
        End Function



        Private Sub lvHandlers_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lvHandlers.SelectedIndexChanged
            Dim item As ListItem
            Try
                If lvHandlers.SelectedItems.Count = 0 Then
                    Return
                End If
                item = OpenFileForItem(lvHandlers.SelectedIndices(0))
                If item Is Nothing Then
                    MessageBox.Show("The code has changed, please run the analysis again")
                    Return
                End If
                bh.Start(CodeRush.TextViews.Active, item.theElement, True)
                lvHandlers.Select()
                lvHandlers.Items(lvHandlers.SelectedIndices(0)).Focused = True
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try

        End Sub


        Private Function OpenFileForItem(ByVal index As Integer) As ListItem
            Dim item As ListItem
            item = DirectCast(lvHandlers.Items(index).Tag, ListItem)
            CodeRush.File.Activate(item.FullFileName)
            CodeRush.Source.ParseIfNeeded()
            Dim theElement As LanguageElement
            theElement = CodeRush.Source.GetNodeAt(item.theElement.StartLine, item.theElement.StartOffset)
            If theElement.ElementType = item.theElement.ElementType AndAlso theElement.Name = item.theElement.Name Then
                item.theElement = theElement
            Else
                Return Nothing
            End If
            Return item
        End Function
        Private Sub lvHandlers_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles lvHandlers.ItemCheck
            Try
                If lvHandlers.SelectedIndices.Count = 0 Then
                    Return
                End If
                If e.NewValue = CheckState.Checked AndAlso DirectCast(lvHandlers.SelectedItems(0).Tag, ListItem).CalledDirectly Then
                    e.NewValue = CheckState.Unchecked
                    MessageBox.Show("This method is called directly, please refactor the method")
                End If
            Catch ex As Exception
                MessageBox.Show(ex.ToString)
            End Try

        End Sub

        Private Sub btnSelectAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectAll.Click
            For i As Integer = 0 To lvHandlers.Items.Count - 1
                If Not DirectCast(lvHandlers.Items(i).Tag, ListItem).CalledDirectly Then
                    lvHandlers.Items(i).Checked = True
                End If
            Next i
        End Sub

        Private Sub btnUncheck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUncheck.Click
            For i As Integer = 0 To lvHandlers.Items.Count - 1
                lvHandlers.Items(i).Checked = False
            Next i
        End Sub
    End Class
End Namespace
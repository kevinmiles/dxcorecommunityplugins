Imports System
Imports System.Windows.Forms
Public Class OrganizerTreeview
    Inherits System.Windows.Forms.UserControl

#Region " Windows Form Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'UserControl overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents trvOrganizer As System.Windows.Forms.TreeView
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnAddRoot As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents sfDialog As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ofDialog As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnOpen As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnUp As System.Windows.Forms.Button
    Friend WithEvents btnDown As System.Windows.Forms.Button
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OrganizerTreeview))
        Me.trvOrganizer = New System.Windows.Forms.TreeView
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnAddRoot = New System.Windows.Forms.Button
        Me.btnEdit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.sfDialog = New System.Windows.Forms.SaveFileDialog
        Me.ofDialog = New System.Windows.Forms.OpenFileDialog
        Me.btnOpen = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnDown = New System.Windows.Forms.Button
        Me.btnUp = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'trvOrganizer
        '
        Me.trvOrganizer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.trvOrganizer.HideSelection = False
        Me.trvOrganizer.Location = New System.Drawing.Point(8, 8)
        Me.trvOrganizer.Name = "trvOrganizer"
        Me.trvOrganizer.Size = New System.Drawing.Size(187, 216)
        Me.trvOrganizer.TabIndex = 0
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.Location = New System.Drawing.Point(201, 46)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(76, 32)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "Add"
        '
        'btnAddRoot
        '
        Me.btnAddRoot.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddRoot.Location = New System.Drawing.Point(201, 8)
        Me.btnAddRoot.Name = "btnAddRoot"
        Me.btnAddRoot.Size = New System.Drawing.Size(76, 32)
        Me.btnAddRoot.TabIndex = 5
        Me.btnAddRoot.Text = "Add Root"
        '
        'btnEdit
        '
        Me.btnEdit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEdit.Location = New System.Drawing.Point(201, 84)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(76, 32)
        Me.btnEdit.TabIndex = 9
        Me.btnEdit.Text = "Edit"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(136, 230)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(58, 32)
        Me.btnSave.TabIndex = 10
        Me.btnSave.Text = "Save"
        '
        'sfDialog
        '
        Me.sfDialog.Filter = "XML files|*.xml"
        '
        'ofDialog
        '
        Me.ofDialog.Filter = "XML files|*.xml|All files|*.*"
        '
        'btnOpen
        '
        Me.btnOpen.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOpen.Location = New System.Drawing.Point(72, 230)
        Me.btnOpen.Name = "btnOpen"
        Me.btnOpen.Size = New System.Drawing.Size(58, 32)
        Me.btnOpen.TabIndex = 11
        Me.btnOpen.Text = "Open"
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNew.Location = New System.Drawing.Point(8, 230)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(58, 32)
        Me.btnNew.TabIndex = 12
        Me.btnNew.Text = "New"
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.ImageIndex = 1
        Me.btnDelete.ImageList = Me.ImageList1
        Me.btnDelete.Location = New System.Drawing.Point(201, 122)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(76, 32)
        Me.btnDelete.TabIndex = 13
        Me.btnDelete.Text = "Delete"
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Fuchsia
        Me.ImageList1.Images.SetKeyName(0, "BuilderDialog_moveup.bmp")
        Me.ImageList1.Images.SetKeyName(1, "BuilderDialog_delete.bmp")
        Me.ImageList1.Images.SetKeyName(2, "BuilderDialog_movedown.bmp")
        Me.ImageList1.Images.SetKeyName(3, "BuilderDialog_add.bmp")
        Me.ImageList1.Images.SetKeyName(4, "BuilderDialog_remove.bmp")
        '
        'btnDown
        '
        Me.btnDown.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnDown.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnDown.ImageIndex = 2
        Me.btnDown.ImageList = Me.ImageList1
        Me.btnDown.Location = New System.Drawing.Point(204, 200)
        Me.btnDown.Name = "btnDown"
        Me.btnDown.Size = New System.Drawing.Size(22, 24)
        Me.btnDown.TabIndex = 15
        '
        'btnUp
        '
        Me.btnUp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUp.ImageIndex = 0
        Me.btnUp.ImageList = Me.ImageList1
        Me.btnUp.Location = New System.Drawing.Point(204, 170)
        Me.btnUp.Name = "btnUp"
        Me.btnUp.Size = New System.Drawing.Size(22, 24)
        Me.btnUp.TabIndex = 14
        '
        'OrganizerTreeview
        '
        Me.Controls.Add(Me.btnDown)
        Me.Controls.Add(Me.btnUp)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.btnOpen)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnAddRoot)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.trvOrganizer)
        Me.Name = "OrganizerTreeview"
        Me.Size = New System.Drawing.Size(280, 265)
        Me.ResumeLayout(False)

    End Sub

#End Region

    Private _ds As DataSet = GetDs()


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim node As TreeNode = Nothing
        Dim frmNewNode As New DefineNodeForm
        If frmNewNode.ShowDialog <> DialogResult.OK Then
            Return
        End If
        node = MakeNode(frmNewNode.cboType.Text, frmNewNode.txtName.Text, , frmNewNode.cboVisibility.Text, , frmNewNode.chkAbstract.Checked, frmNewNode.chkExtern.Checked, frmNewNode.chkOverloads.Checked, frmNewNode.chkOverrides.Checked, frmNewNode.chkVitual.Checked)
        If Not node Is Nothing Then
            If Not trvOrganizer.SelectedNode Is Nothing Then
                trvOrganizer.SelectedNode.Nodes.Add(node)
                CType(node.Tag, INode).ParentID = CType(trvOrganizer.SelectedNode.Tag, INode).ID
                trvOrganizer.SelectedNode.Expand()
            Else
                trvOrganizer.Nodes.Add(node)
            End If
            'trvOrganizer.SelectedNode = node
            trvOrganizer.Focus()
        End If
    End Sub


    Private Sub btnAddRoot_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRoot.Click
        Dim node As TreeNode = Nothing
        Dim frmNewNode As New DefineNodeForm
        If frmNewNode.ShowDialog <> DialogResult.OK Then
            Return
        End If
        node = MakeNode(frmNewNode.cboType.Text, frmNewNode.txtName.Text, , frmNewNode.cboVisibility.Text, , frmNewNode.chkAbstract.Checked, frmNewNode.chkExtern.Checked, frmNewNode.chkOverloads.Checked, frmNewNode.chkOverrides.Checked, frmNewNode.chkVitual.Checked)
        If Not node Is Nothing Then
            trvOrganizer.Nodes.Add(node)
            trvOrganizer.SelectedNode = node
            trvOrganizer.Focus()
        End If
    End Sub

    Private Function MakeNode(ByVal type As String, ByVal name As String, Optional ByVal ID As String = "", Optional ByVal visibility As String = "", Optional ByVal ParentID As String = "", Optional ByVal blnAbstract As Boolean = False, Optional ByVal blnExtern As Boolean = False, Optional ByVal blnOverloads As Boolean = False, Optional ByVal blnOverrides As Boolean = False, Optional ByVal blnVirtual As Boolean = False) As TreeNode
        Dim item As Object = Nothing
        If type = "Region" Or type = "Comment" Then
            item = New ElementNode(name, type, ID, ParentID)
        Else
            If Not visibility = "" Then
                item = New ExtendedNode(name, type, [Enum].Parse(GetType(Visibility), visibility), ID, ParentID, blnAbstract, blnExtern, blnOverloads, blnOverrides, blnVirtual)
            Else
                MsgBox("Visibility must be given")
            End If
        End If
        If Not item Is Nothing Then
            Dim test As New TreeNode(item.ToString)
            test.Tag = item
            Return test
        Else
            Return Nothing
        End If
    End Function

    'Private Sub trvOrganizer_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvOrganizer.AfterSelect
    '    txtName.Text = CType(trvOrganizer.SelectedNode.Tag, INode).Name
    '    cboType.Text = CType(trvOrganizer.SelectedNode.Tag, INode).Type

    '    If TypeOf trvOrganizer.SelectedNode.Tag Is ExtendedNode Then
    '        cboVisibility.Enabled = True
    '        cboVisibility.Text = CType(trvOrganizer.SelectedNode.Tag, ExtendedNode).Visible.ToString
    '    Else
    '        cboVisibility.Enabled = False
    '        cboVisibility.Text = ""
    '    End If
    'End Sub

    'Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
    '    If Not trvOrganizer.SelectedNode Is Nothing Then
    '        CType(trvOrganizer.SelectedNode.Tag, INode).Name = txtName.Text
    '        CType(trvOrganizer.SelectedNode.Tag, INode).Type = cboType.Text
    '        If TypeOf trvOrganizer.SelectedNode.Tag Is ExtendedNode Then
    '            cboVisibility.Enabled = True
    '            CType(trvOrganizer.SelectedNode.Tag, ExtendedNode).Visible = [Enum].Parse(GetType(Visibility), Me.cboVisibility.Text)
    '        Else
    '            cboVisibility.Enabled = False
    '            cboVisibility.Text = ""
    '        End If
    '        trvOrganizer.SelectedNode.Text = CType(trvOrganizer.SelectedNode.Tag, INode).ToString
    '    End If
    'End Sub

    Private Sub SaveTree(ByVal nodes As TreeNodeCollection)
        For Each node As TreeNode In nodes
            AddNodeToDataSet(_ds, node)
            SaveTree(node.Nodes)
        Next
    End Sub

    Private Shared Function GetDs() As DataSet
        Static ds As DataSet = Nothing
        If ds Is Nothing Then
            ds = New DataSet("Tree")

            Dim xmlResouce As String = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".NodesSchema.xsd"
            Using s As IO.Stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream(xmlResouce)
                ds.ReadXmlSchema(s)
            End Using

            'ds.ReadXmlSchema("NodesSchema.xsd")

            'Dim dt As DataTable = ds.Tables.Add("Nodes")
            'Dim nodeID As DataColumn = dt.Columns.Add("ID", GetType(String))
            'Dim nodeDesc As DataColumn = dt.Columns.Add("Name", GetType(String))
            'Dim nodeType As DataColumn = dt.Columns.Add("Type", GetType(String))
            'Dim nodeVis As DataColumn = dt.Columns.Add("Visibility", GetType(String))
            'Dim nodeParentID As DataColumn = dt.Columns.Add("ParentID", GetType(String))

            'dt.PrimaryKey = New DataColumn() {nodeID}
        End If
        Return ds
    End Function

    Private Sub AddNodeToDataSet(ByRef ds As DataSet, ByVal node As TreeNode)
        'Save to Dataset
        Dim drow As DataRow = ds.Tables("Nodes").NewRow()

        drow.Item("ID") = CType(node.Tag, INode).ID
        drow.Item("Name") = CType(node.Tag, INode).Name
        drow.Item("Type") = CType(node.Tag, INode).Type
        If TypeOf node.Tag Is ExtendedNode Then
            drow.Item("Visibility") = CType(node.Tag, ExtendedNode).Visible
            drow.Item("Abstract") = CType(node.Tag, ExtendedNode).Abstract
            drow.Item("Extern") = CType(node.Tag, ExtendedNode).Extern
            drow.Item("Overload") = CType(node.Tag, ExtendedNode).Overload
            drow.Item("Override") = CType(node.Tag, ExtendedNode).Override
            drow.Item("Virtual") = CType(node.Tag, ExtendedNode).Virtual
        End If
        drow.Item("ParentID") = CType(node.Tag, INode).ParentID
        ds.Tables("Nodes").Rows.Add(drow)
    End Sub

    Private Sub LoadTree(ByVal table As DataTable)
        For Each row As DataRow In table.Rows
            Dim node As TreeNode
            If IsDBNull(row("Visibility")) Then
                node = MakeNode(row("Type"), row("Name"), row("ID"), "", row("ParentID"))
                node.Name = row("ID")
            Else
                node = MakeNode(row("Type"), row("Name"), row("ID"), row("Visibility"), row("ParentID"), row("Abstract"), row("Extern"), row("Overload"), row("Override"), row("Virtual"))
                node.Name = row("ID")
            End If
            Dim totalFound As Integer = trvOrganizer.Nodes.Find(row("ParentID"), True).Length
            If totalFound > 0 Then
                Dim parent As TreeNode = trvOrganizer.Nodes.Find(row("ParentID"), True)(0)
                parent.Nodes.Add(node)
            Else
                trvOrganizer.Nodes.Add(node)
            End If
        Next
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        _ds.Tables(0).Clear()
        SaveTree(trvOrganizer.Nodes)
        If sfDialog.ShowDialog() = DialogResult.OK Then
            _ds.WriteXml(sfDialog.FileName, XmlWriteMode.WriteSchema)
        End If
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If trvOrganizer.SelectedNode.Nodes.Count > 0 Then
            If MsgBox("Are you sure you want to delete the selected node? The subNodes will be removed too if you click OK.", MsgBoxStyle.YesNo, "Remove nodes") = MsgBoxResult.Yes Then
                trvOrganizer.SelectedNode.Remove()
            End If
        Else
            trvOrganizer.SelectedNode.Remove()
        End If
    End Sub

    Private Sub btnOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpen.Click
        If ofDialog.ShowDialog() = DialogResult.OK Then
            _ds.Clear()
            trvOrganizer.Nodes.Clear()
            _ds.ReadXml(ofDialog.FileName, XmlReadMode.ReadSchema)
            LoadTree(_ds.Tables("Nodes"))
        End If
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        trvOrganizer.Nodes.Clear()
        _ds.Clear()
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Dim nod As TreeNode = trvOrganizer.SelectedNode
        Try
            If Not nod.PrevNode Is Nothing Then
                If nod.Parent Is Nothing Then
                    trvOrganizer.Nodes.Insert(nod.Index - 1, CType(nod.Clone, TreeNode))
                    trvOrganizer.SelectedNode = trvOrganizer.Nodes(nod.Index - 2)
                Else
                    nod.Parent.Nodes.Insert(nod.Index - 1, CType(nod.Clone, TreeNode))
                    trvOrganizer.SelectedNode = nod.Parent.Nodes(nod.Index - 2)
                End If
                nod.Remove()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Dim nod As TreeNode = trvOrganizer.SelectedNode
        Try
            If Not nod.NextNode Is Nothing Then
                If nod.Parent Is Nothing Then
                    trvOrganizer.Nodes.Insert(nod.Index + 2, CType(nod.Clone, TreeNode))
                    trvOrganizer.SelectedNode = trvOrganizer.Nodes(nod.Index + 2)
                Else
                    nod.Parent.Nodes.Insert(nod.Index + 2, CType(nod.Clone, TreeNode))
                    trvOrganizer.SelectedNode = nod.Parent.Nodes(nod.Index + 2)
                End If
                nod.Remove()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btnHigher_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim nod As TreeNode = trvOrganizer.SelectedNode
        Try
            If Not nod.Parent Is Nothing Then
                If nod.Parent.Parent Is Nothing Then
                    trvOrganizer.Nodes.Insert(nod.Parent.Index + 2, CType(nod.Clone, TreeNode))
                Else
                    nod.Parent.Parent.Nodes.Insert(nod.Parent.Parent.Index + 2, CType(nod.Clone, TreeNode))
                End If
                nod.Remove()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString)
        End Try
    End Sub

    Private Sub btnLower_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'TODO
        'Dim nod As TreeNode = trvOrganizer.SelectedNode
        'Try
        '    If Not nod.PrevNode Is Nothing Then
        '        nod.PrevNode.Nodes.Add(CType(nod.Clone, TreeNode))
        '        nod.Remove()
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString)
        'End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim frmEditnode As DefineNodeForm
        If Not trvOrganizer.SelectedNode Is Nothing AndAlso Not trvOrganizer.SelectedNode.Tag Is Nothing Then
            frmEditnode = New DefineNodeForm(CType(trvOrganizer.SelectedNode.Tag, INode))
            If frmEditnode.ShowDialog = DialogResult.OK Then
                If Not trvOrganizer.SelectedNode Is Nothing Then
                    CType(trvOrganizer.SelectedNode.Tag, INode).Name = frmEditnode.txtName.Text
                    CType(trvOrganizer.SelectedNode.Tag, INode).Type = frmEditnode.cboType.Text
                    If TypeOf trvOrganizer.SelectedNode.Tag Is ExtendedNode Then
                        frmEditnode.cboVisibility.Enabled = True
                        CType(trvOrganizer.SelectedNode.Tag, ExtendedNode).Visible = [Enum].Parse(GetType(Visibility), frmEditnode.cboVisibility.Text)
                        CType(trvOrganizer.SelectedNode.Tag, ExtendedNode).Abstract = frmEditnode.chkAbstract.Checked
                        CType(trvOrganizer.SelectedNode.Tag, ExtendedNode).Extern = frmEditnode.chkExtern.Checked
                        CType(trvOrganizer.SelectedNode.Tag, ExtendedNode).Overload = frmEditnode.chkOverloads.Checked
                        CType(trvOrganizer.SelectedNode.Tag, ExtendedNode).Override = frmEditnode.chkOverrides.Checked
                        CType(trvOrganizer.SelectedNode.Tag, ExtendedNode).Virtual = frmEditnode.chkVitual.Checked
                    Else
                        frmEditnode.cboVisibility.Enabled = False
                        frmEditnode.cboVisibility.Text = ""
                    End If
                    trvOrganizer.SelectedNode.Text = CType(trvOrganizer.SelectedNode.Tag, INode).ToString
                End If

            End If
        End If
    End Sub

    'Private Sub trvOrganizer_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles trvOrganizer.AfterSelect
    '    txtName.Text = CType(trvOrganizer.SelectedNode.Tag, INode).Name
    '    cboType.Text = CType(trvOrganizer.SelectedNode.Tag, INode).Type

    '    If TypeOf trvOrganizer.SelectedNode.Tag Is ExtendedNode Then
    '        cboVisibility.Enabled = True
    '        cboVisibility.Text = CType(trvOrganizer.SelectedNode.Tag, ExtendedNode).Visible.ToString
    '    Else
    '        cboVisibility.Enabled = False
    '        cboVisibility.Text = ""
    '    End If
    'End Sub


End Class

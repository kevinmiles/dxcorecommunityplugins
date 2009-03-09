Public Class DefineNodeForm

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.DialogResult = Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub

    Public Sub New(ByVal node As INode)
        Me.new()
        txtName.Text = node.Name
        cboType.Text = node.Type
        If TypeOf node Is ExtendedNode Then
            cboVisibility.Enabled = True
            cboVisibility.Text = CType(node, ExtendedNode).Visible.ToString

            chkAbstract.Checked = CType(node, ExtendedNode).Abstract
            chkExtern.Checked = CType(node, ExtendedNode).Extern
            chkOverloads.Checked = CType(node, ExtendedNode).Overload
            chkOverrides.Checked = CType(node, ExtendedNode).Override
            chkVitual.Checked = CType(node, ExtendedNode).Virtual
        Else
            cboVisibility.Enabled = False
            cboVisibility.Text = ""

            chkAbstract.Enabled = False
            chkAbstract.Checked = False
            chkExtern.Enabled = False
            chkExtern.Checked = False
            chkOverloads.Enabled = False
            chkOverloads.Checked = False
            chkOverrides.Enabled = False
            chkOverrides.Checked = False
            chkVitual.Enabled = False
            chkVitual.Checked = False
        End If
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.cboType.Items.Add("Comment")
        Me.cboType.Items.Add("Constant")
        Me.cboType.Items.Add("Constructor")
        Me.cboType.Items.Add("Delegate")
        Me.cboType.Items.Add("Event")
        Me.cboType.Items.Add("Method")
        Me.cboType.Items.Add("Property")
        Me.cboType.Items.Add("Region")
        Me.cboType.Items.Add("Variable")
        Me.cboType.Items.Add("Other")

        Me.cboVisibility.Items.Add(Visibility.Private)
        Me.cboVisibility.Items.Add(Visibility.Public)
        Me.cboVisibility.Items.Add(Visibility.Protected)
        Me.cboVisibility.Items.Add(Visibility.Protected_internal)
        Me.cboVisibility.Items.Add(Visibility.Internal)
        Me.cboVisibility.Items.Add(Visibility.All)
        Me.cboVisibility.SelectedIndex = 5
    End Sub

    Private Sub cboType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboType.SelectedIndexChanged
        If cboType.Text = "Region" Or cboType.Text = "Comment" Then
            cboVisibility.Enabled = False
            cboVisibility.Text = ""
            txtName.Enabled = True

            chkAbstract.Enabled = False
            chkAbstract.Checked = False
            chkExtern.Enabled = False
            chkExtern.Checked = False
            chkOverloads.Enabled = False
            chkOverloads.Checked = False
            chkOverrides.Enabled = False
            chkOverrides.Checked = False
            chkVitual.Enabled = False
            chkVitual.Checked = False
        ElseIf cboType.Text = "Constructor" Then
            cboVisibility.Enabled = True
            cboVisibility.Text = ""
            txtName.Enabled = False

            chkAbstract.Enabled = True
            chkAbstract.Checked = False
            chkExtern.Enabled = True
            chkExtern.Checked = False
            chkOverloads.Enabled = True
            chkOverloads.Checked = False
            chkOverrides.Enabled = True
            chkOverrides.Checked = False
            chkVitual.Enabled = True
            chkVitual.Checked = False
        Else
            cboVisibility.Enabled = True
            txtName.Enabled = True

            chkAbstract.Enabled = True
            chkAbstract.Checked = False
            chkExtern.Enabled = True
            chkExtern.Checked = False
            chkOverloads.Enabled = True
            chkOverloads.Checked = False
            chkOverrides.Enabled = True
            chkOverrides.Checked = False
            chkVitual.Enabled = True
            chkVitual.Checked = False
        End If
    End Sub
End Class
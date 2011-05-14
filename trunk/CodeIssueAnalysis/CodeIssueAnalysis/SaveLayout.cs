using System;
using System.Windows.Forms;
using DevExpress.DXCore.Controls.XtraGrid.Views.Grid;
using System.IO;

namespace CodeIssueAnalysis
{
    public partial class SaveLayout : Form
    {
        readonly GridView view;
        internal string saveName;

        public SaveLayout()
        {
            InitializeComponent();
        }

        public SaveLayout(GridView view)
        {
            this.view = view;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text.Length > 0)
                {
                    view.SaveLayoutToXml(String.Format("{0}{1}{2}.xml", CodeIssueOptions.GetLayoutPath(), Path.DirectorySeparatorChar, txtName.Text));
                    saveName = txtName.Text + ".xml";
                    Close();
                }
                else
                {
                    MessageBox.Show("You must enter a layout name.", "Enter Layout Name");
                }
            }
            catch
            {
                MessageBox.Show("Failed to Save", "Saving Failed Possibly Access Privileges");
            }
        }
    }
}

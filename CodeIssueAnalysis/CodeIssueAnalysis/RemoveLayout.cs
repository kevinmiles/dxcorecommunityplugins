using System;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace CodeIssueAnalysis
{
    public partial class RemoveLayout : Form
    {
        public RemoveLayout()
        {
            InitializeComponent();
            CodeIssueOptions.UpdateLayoutsList(cmbLayouts, false);
            try
            {
                cmbLayouts.SelectedIndex = 0;
            }
            catch 
            {
                Debug.Assert(false, "No Layouts");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                File.Delete(String.Format("{0}{1}{2}",CodeIssueOptions.GetLayoutPath(), Path.DirectorySeparatorChar, cmbLayouts.SelectedItem));
                Close();
            }
            catch
            {
                MessageBox.Show("Failed to Remove Layout", "Removing Layout Failed");
                Close();
            }
        }
    }
}

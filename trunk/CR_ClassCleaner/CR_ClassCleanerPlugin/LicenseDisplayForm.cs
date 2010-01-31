using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CR_ClassCleaner
{
    public partial class LicenseDisplayForm : Form
    {
        public LicenseDisplayForm()
        {
            InitializeComponent();
        }

        private void LicenseDisplayForm_Load(object sender, EventArgs e)
        {
            txtLicense.Text = Properties.Settings.Default.LicenseAgreement;
            txtLicense.Parent.Focus();
        }

    }
}

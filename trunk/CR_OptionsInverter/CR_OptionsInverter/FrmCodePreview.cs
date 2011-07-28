using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CR_OptionsInverter
{
	public partial class FrmCodePreview : Form
	{
		public FrmCodePreview()
		{
			InitializeComponent();
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			Clipboard.SetText(codeView1.Text);
		}

		public FrmCodePreview(string generatedCode, string language)
			: this()
		{
			codeView1.ShowCode(generatedCode, language);
		}
	}
   
}

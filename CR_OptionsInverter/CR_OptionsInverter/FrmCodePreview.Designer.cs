namespace CR_OptionsInverter
{
	partial class FrmCodePreview
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCodePreview));
			this.codeView1 = new DevExpress.CodeRush.UserControls.CodeView();
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.codeView1)).BeginInit();
			this.SuspendLayout();
			// 
			// codeView1
			// 
			this.codeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.codeView1.BorderColor = System.Drawing.Color.Empty;
			this.codeView1.EmphasisRange = ((DevExpress.CodeRush.StructuralParser.SourceRange)(resources.GetObject("codeView1.EmphasisRange")));
			this.codeView1.LanguageID = "CSharp";
			this.codeView1.LeftPixel = 0;
			this.codeView1.Location = new System.Drawing.Point(0, -3);
			this.codeView1.Name = "codeView1";
			this.codeView1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.codeView1.Size = new System.Drawing.Size(287, 230);
			this.codeView1.TabIndex = 0;
			this.codeView1.TextChangeEffect = DevExpress.CodeRush.UserControls.TextChangeEffect.None;
			this.codeView1.TopLine = 0;
			this.codeView1.TransitionTime = 900;
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopy.Location = new System.Drawing.Point(131, 233);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCopy.TabIndex = 1;
			this.btnCopy.Text = "Copy";
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(212, 233);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 1;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// FrmCodePreview
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnCopy);
			this.Controls.Add(this.codeView1);
			this.Name = "FrmCodePreview";
			this.Text = "FrmCodePreview";
			((System.ComponentModel.ISupportInitialize)(this.codeView1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.CodeRush.UserControls.CodeView codeView1;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Button btnOK;
	}
}
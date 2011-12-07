namespace CR_ImportNamespace
{
	partial class FrmCollectingClasses
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
      this.panel1 = new System.Windows.Forms.Panel();
      this.lblPerformingScan = new System.Windows.Forms.Label();
      this.progress = new System.Windows.Forms.ProgressBar();
      this.lblScanStatus = new System.Windows.Forms.Label();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // panel1
      // 
      this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.panel1.Controls.Add(this.lblPerformingScan);
      this.panel1.Controls.Add(this.progress);
      this.panel1.Controls.Add(this.lblScanStatus);
      this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.panel1.Location = new System.Drawing.Point(0, 0);
      this.panel1.Name = "panel1";
      this.panel1.Size = new System.Drawing.Size(441, 79);
      this.panel1.TabIndex = 0;
      // 
      // lblPerformingScan
      // 
      this.lblPerformingScan.AutoSize = true;
      this.lblPerformingScan.Location = new System.Drawing.Point(9, 8);
      this.lblPerformingScan.Name = "lblPerformingScan";
      this.lblPerformingScan.Size = new System.Drawing.Size(256, 13);
      this.lblPerformingScan.TabIndex = 5;
      this.lblPerformingScan.Text = "Performing one-time scan of the .NET {0} framework.";
      // 
      // progress
      // 
      this.progress.Location = new System.Drawing.Point(8, 47);
      this.progress.Name = "progress";
      this.progress.Size = new System.Drawing.Size(424, 23);
      this.progress.TabIndex = 4;
      // 
      // lblScanStatus
      // 
      this.lblScanStatus.AutoSize = true;
      this.lblScanStatus.Location = new System.Drawing.Point(9, 29);
      this.lblScanStatus.Name = "lblScanStatus";
      this.lblScanStatus.Size = new System.Drawing.Size(78, 13);
      this.lblScanStatus.TabIndex = 3;
      this.lblScanStatus.Text = "Scanning {0}...";
      // 
      // FrmCollectingClasses
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(441, 79);
      this.Controls.Add(this.panel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "FrmCollectingClasses";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "Collecting Types...";
      this.panel1.ResumeLayout(false);
      this.panel1.PerformLayout();
      this.ResumeLayout(false);

		}

		#endregion

    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Label lblPerformingScan;
    private System.Windows.Forms.ProgressBar progress;
    private System.Windows.Forms.Label lblScanStatus;

  }
}
namespace CR_Initials
{
    partial class OptInitials
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
            this.lblDevName = new System.Windows.Forms.Label();
            this.lblDevInitials = new System.Windows.Forms.Label();
            this.chkFullname = new System.Windows.Forms.CheckBox();
            this.txtDevName = new System.Windows.Forms.TextBox();
            this.txtDevInitials = new System.Windows.Forms.TextBox();
            this.txtDateFormat = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDevName
            // 
            this.lblDevName.AutoSize = true;
            this.lblDevName.Location = new System.Drawing.Point(35, 29);
            this.lblDevName.Name = "lblDevName";
            this.lblDevName.Size = new System.Drawing.Size(90, 13);
            this.lblDevName.TabIndex = 0;
            this.lblDevName.Text = "Developer Name:";
            // 
            // lblDevInitials
            // 
            this.lblDevInitials.AutoSize = true;
            this.lblDevInitials.Location = new System.Drawing.Point(35, 64);
            this.lblDevInitials.Name = "lblDevInitials";
            this.lblDevInitials.Size = new System.Drawing.Size(91, 13);
            this.lblDevInitials.TabIndex = 1;
            this.lblDevInitials.Text = "Developer Initials:";
            // 
            // chkFullname
            // 
            this.chkFullname.AutoSize = true;
            this.chkFullname.Location = new System.Drawing.Point(205, 64);
            this.chkFullname.Name = "chkFullname";
            this.chkFullname.Size = new System.Drawing.Size(173, 17);
            this.chkFullname.TabIndex = 2;
            this.chkFullname.Text = "Use full name for line comment.";
            this.chkFullname.UseVisualStyleBackColor = true;
            // 
            // txtDevName
            // 
            this.txtDevName.Location = new System.Drawing.Point(132, 26);
            this.txtDevName.Name = "txtDevName";
            this.txtDevName.Size = new System.Drawing.Size(218, 20);
            this.txtDevName.TabIndex = 3;
            // 
            // txtDevInitials
            // 
            this.txtDevInitials.Location = new System.Drawing.Point(132, 61);
            this.txtDevInitials.Name = "txtDevInitials";
            this.txtDevInitials.Size = new System.Drawing.Size(67, 20);
            this.txtDevInitials.TabIndex = 4;
            // 
            // txtDateFormat
            // 
            this.txtDateFormat.Location = new System.Drawing.Point(132, 97);
            this.txtDateFormat.Name = "txtDateFormat";
            this.txtDateFormat.Size = new System.Drawing.Size(100, 20);
            this.txtDateFormat.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(57, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Date Format:";
            // 
            // OptInitials
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDateFormat);
            this.Controls.Add(this.txtDevInitials);
            this.Controls.Add(this.txtDevName);
            this.Controls.Add(this.chkFullname);
            this.Controls.Add(this.lblDevInitials);
            this.Controls.Add(this.lblDevName);
            this.Name = "OptInitials";
            this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.OptInitials_PreparePage);
            this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.OptInitials_CommitChanges);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDevName;
        private System.Windows.Forms.Label lblDevInitials;
        private System.Windows.Forms.CheckBox chkFullname;
        private System.Windows.Forms.TextBox txtDevName;
        private System.Windows.Forms.TextBox txtDevInitials;
        private System.Windows.Forms.TextBox txtDateFormat;
        private System.Windows.Forms.Label label1;
    }
}

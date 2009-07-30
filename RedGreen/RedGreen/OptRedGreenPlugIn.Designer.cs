using System;
using DevExpress.CodeRush.Core;

namespace RedGreen
{
	partial class OptRedGreenPlugIn
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public OptRedGreenPlugIn()
		{
			/// <summary>
			/// Required for Windows.Forms Class Composition Designer support
			/// </summary>
			InitializeComponent();
		}

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
			this.drawAdHocIcon = new System.Windows.Forms.CheckBox();
			this.colorDialog1 = new System.Windows.Forms.ColorDialog();
			this.passColor = new System.Windows.Forms.Button();
			this.passAlpha = new System.Windows.Forms.TrackBar();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.failAlpha = new System.Windows.Forms.TrackBar();
			this.failColor = new System.Windows.Forms.Button();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.skipAlpha = new System.Windows.Forms.TrackBar();
			this.skipColor = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.passAlpha)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.failAlpha)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.skipAlpha)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// drawAdHocIcon
			// 
			this.drawAdHocIcon.AutoSize = true;
			this.drawAdHocIcon.Location = new System.Drawing.Point(24, 387);
			this.drawAdHocIcon.Name = "drawAdHocIcon";
			this.drawAdHocIcon.Size = new System.Drawing.Size(257, 21);
			this.drawAdHocIcon.TabIndex = 1;
			this.drawAdHocIcon.Text = "Show Test Runner for Ad Hoc Tests";
			this.drawAdHocIcon.UseVisualStyleBackColor = true;
			// 
			// passColor
			// 
			this.passColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.passColor.Location = new System.Drawing.Point(141, 34);
			this.passColor.Name = "passColor";
			this.passColor.Size = new System.Drawing.Size(152, 30);
			this.passColor.TabIndex = 4;
			this.passColor.UseVisualStyleBackColor = true;
			this.passColor.Click += new System.EventHandler(this.passColor_Click);
			// 
			// passAlpha
			// 
			this.passAlpha.Location = new System.Drawing.Point(141, 71);
			this.passAlpha.Name = "passAlpha";
			this.passAlpha.Size = new System.Drawing.Size(166, 56);
			this.passAlpha.TabIndex = 5;
			this.passAlpha.ValueChanged += new System.EventHandler(this.passAlpha_ValueChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.label1.Location = new System.Drawing.Point(150, 109);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(81, 17);
			this.label1.TabIndex = 6;
			this.label1.Text = "transparent";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(237, 109);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 17);
			this.label2.TabIndex = 7;
			this.label2.Text = "opaque";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(21, 4);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(126, 17);
			this.label3.TabIndex = 8;
			this.label3.Text = "Attribute Colors:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(21, 34);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(39, 17);
			this.label4.TabIndex = 9;
			this.label4.Text = "Pass";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(21, 156);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(30, 17);
			this.label5.TabIndex = 14;
			this.label5.Text = "Fail";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(237, 231);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 17);
			this.label6.TabIndex = 13;
			this.label6.Text = "opaque";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.label7.Location = new System.Drawing.Point(150, 231);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(81, 17);
			this.label7.TabIndex = 12;
			this.label7.Text = "transparent";
			// 
			// failAlpha
			// 
			this.failAlpha.Location = new System.Drawing.Point(141, 193);
			this.failAlpha.Name = "failAlpha";
			this.failAlpha.Size = new System.Drawing.Size(166, 56);
			this.failAlpha.TabIndex = 11;
			this.failAlpha.ValueChanged += new System.EventHandler(this.failAlpha_ValueChanged);
			// 
			// failColor
			// 
			this.failColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.failColor.Location = new System.Drawing.Point(141, 156);
			this.failColor.Name = "failColor";
			this.failColor.Size = new System.Drawing.Size(152, 30);
			this.failColor.TabIndex = 10;
			this.failColor.UseVisualStyleBackColor = true;
			this.failColor.Click += new System.EventHandler(this.failColor_Click);
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(21, 276);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(35, 17);
			this.label8.TabIndex = 19;
			this.label8.Text = "Skip";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(237, 351);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(56, 17);
			this.label9.TabIndex = 18;
			this.label9.Text = "opaque";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.ForeColor = System.Drawing.SystemColors.ControlDark;
			this.label10.Location = new System.Drawing.Point(150, 351);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(81, 17);
			this.label10.TabIndex = 17;
			this.label10.Text = "transparent";
			// 
			// skipAlpha
			// 
			this.skipAlpha.Location = new System.Drawing.Point(141, 313);
			this.skipAlpha.Name = "skipAlpha";
			this.skipAlpha.Size = new System.Drawing.Size(166, 56);
			this.skipAlpha.TabIndex = 16;
			this.skipAlpha.ValueChanged += new System.EventHandler(this.skipAlpha_ValueChanged);
			// 
			// skipColor
			// 
			this.skipColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.skipColor.Location = new System.Drawing.Point(141, 276);
			this.skipColor.Name = "skipColor";
			this.skipColor.Size = new System.Drawing.Size(152, 30);
			this.skipColor.TabIndex = 15;
			this.skipColor.UseVisualStyleBackColor = true;
			this.skipColor.Click += new System.EventHandler(this.skipColor_Click);
			// 
			// OptRedGreenPlugIn
			// 
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.skipAlpha);
			this.Controls.Add(this.skipColor);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.failAlpha);
			this.Controls.Add(this.failColor);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.passAlpha);
			this.Controls.Add(this.passColor);
			this.Controls.Add(this.drawAdHocIcon);
			this.Name = "OptRedGreenPlugIn";
			this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.OptRedGreenPlugIn_PreparePage);
			this.RestoreDefaults += new DevExpress.CodeRush.Core.OptionsPage.RestoreDefaultsEventHandler(this.OptRedGreenPlugIn_RestoreDefaults);
			this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.OptRedGreenPlugIn_CommitChanges);
			((System.ComponentModel.ISupportInitialize)(this.passAlpha)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.failAlpha)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.skipAlpha)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		///
		/// Gets a DecoupledStorage instance for this options page.
		///
		public static DecoupledStorage Storage
		{
			get
			{
				return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName());
			}
		}
		///
		/// Returns the category of this options page.
		///
		public override string Category
		{
			get
			{
				return OptRedGreenPlugIn.GetCategory();
			}
		}
		///
		/// Returns the page name of this options page.
		///
		public override string PageName
		{
			get
			{
				return OptRedGreenPlugIn.GetPageName();
			}
		}
		///
		/// Returns the full path (Category + PageName) of this options page.
		///
		public static string FullPath
		{
			get
			{
				return GetCategory() + "\\" + GetPageName();
			}
		}

		///
		/// Displays the DXCore options dialog and selects this page.
		///
		public new static void Show()
		{
			DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath);
		}
		private System.Windows.Forms.CheckBox drawAdHocIcon;
		private System.Windows.Forms.ColorDialog colorDialog1;
		private System.Windows.Forms.Button passColor;
		private System.Windows.Forms.TrackBar passAlpha;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TrackBar failAlpha;
		private System.Windows.Forms.Button failColor;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TrackBar skipAlpha;
		private System.Windows.Forms.Button skipColor;
	}
}
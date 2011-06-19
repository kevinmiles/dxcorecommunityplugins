using System;
using DevExpress.CodeRush.Core;

namespace CR_EasierIdentiers
{
	partial class OptConvertSpacesToCamelCase
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public OptConvertSpacesToCamelCase()
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
			this.chkEnabled = new System.Windows.Forms.CheckBox();
			this.chkEnableInParameters = new System.Windows.Forms.CheckBox();
			this.chkEnableLocalVariables = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// chkEnabled
			// 
			this.chkEnabled.AutoSize = true;
			this.chkEnabled.Location = new System.Drawing.Point(3, 3);
			this.chkEnabled.Name = "chkEnabled";
			this.chkEnabled.Size = new System.Drawing.Size(64, 17);
			this.chkEnabled.TabIndex = 0;
			this.chkEnabled.Text = "Enabled";
			this.chkEnabled.UseVisualStyleBackColor = true;
			// 
			// chkEnableInParameters
			// 
			this.chkEnableInParameters.AutoSize = true;
			this.chkEnableInParameters.Location = new System.Drawing.Point(22, 26);
			this.chkEnableInParameters.Name = "chkEnableInParameters";
			this.chkEnableInParameters.Size = new System.Drawing.Size(127, 17);
			this.chkEnableInParameters.TabIndex = 0;
			this.chkEnableInParameters.Text = "Enable in Parameters";
			this.chkEnableInParameters.UseVisualStyleBackColor = true;
			// 
			// chkEnableLocalVariables
			// 
			this.chkEnableLocalVariables.AutoSize = true;
			this.chkEnableLocalVariables.Location = new System.Drawing.Point(22, 49);
			this.chkEnableLocalVariables.Name = "chkEnableLocalVariables";
			this.chkEnableLocalVariables.Size = new System.Drawing.Size(142, 17);
			this.chkEnableLocalVariables.TabIndex = 0;
			this.chkEnableLocalVariables.Text = "Enable in Local Variables";
			this.chkEnableLocalVariables.UseVisualStyleBackColor = true;
			// 
			// OptConvertSpacesToCamelCase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.chkEnableLocalVariables);
			this.Controls.Add(this.chkEnableInParameters);
			this.Controls.Add(this.chkEnabled);
			this.Name = "OptConvertSpacesToCamelCase";
			this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.OptConvertSpacesToCamelCase_CommitChanges);
			this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.OptConvertSpacesToCamelCase_PreparePage);
			this.RestoreDefaults += new DevExpress.CodeRush.Core.OptionsPage.RestoreDefaultsEventHandler(this.OptConvertSpacesToCamelCase_RestoreDefaults);
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
				return OptConvertSpacesToCamelCase.GetCategory();
			}
		}
		///
		/// Returns the page name of this options page.
		///
		public override string PageName
		{
			get
			{
				return OptConvertSpacesToCamelCase.GetPageName();
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

		private System.Windows.Forms.CheckBox chkEnabled;
		private System.Windows.Forms.CheckBox chkEnableInParameters;
		private System.Windows.Forms.CheckBox chkEnableLocalVariables;
	}
}
using System;
using DevExpress.CodeRush.Core;

namespace UnitTestErrorVisualizer
{
	partial class OptUnitTestVisualizer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public OptUnitTestVisualizer()
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
			this.shadeAttribute = new System.Windows.Forms.CheckBox();
			this.arrowToFailed = new System.Windows.Forms.CheckBox();
			this.overlayMessage = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// shadeAttribute
			// 
			this.shadeAttribute.AutoSize = true;
			this.shadeAttribute.Checked = true;
			this.shadeAttribute.CheckState = System.Windows.Forms.CheckState.Checked;
			this.shadeAttribute.Location = new System.Drawing.Point(16, 16);
			this.shadeAttribute.Name = "shadeAttribute";
			this.shadeAttribute.Size = new System.Drawing.Size(280, 21);
			this.shadeAttribute.TabIndex = 0;
			this.shadeAttribute.Text = "Shade test attribute with pass/fail status";
			this.shadeAttribute.UseVisualStyleBackColor = true;
			// 
			// arrowToFailed
			// 
			this.arrowToFailed.AutoSize = true;
			this.arrowToFailed.Checked = true;
			this.arrowToFailed.CheckState = System.Windows.Forms.CheckState.Checked;
			this.arrowToFailed.Location = new System.Drawing.Point(16, 44);
			this.arrowToFailed.Name = "arrowToFailed";
			this.arrowToFailed.Size = new System.Drawing.Size(205, 21);
			this.arrowToFailed.TabIndex = 1;
			this.arrowToFailed.Text = "Draw arrow to failed asserts";
			this.arrowToFailed.UseVisualStyleBackColor = true;
			// 
			// overlayMessage
			// 
			this.overlayMessage.AutoSize = true;
			this.overlayMessage.Checked = true;
			this.overlayMessage.CheckState = System.Windows.Forms.CheckState.Checked;
			this.overlayMessage.Location = new System.Drawing.Point(16, 72);
			this.overlayMessage.Name = "overlayMessage";
			this.overlayMessage.Size = new System.Drawing.Size(302, 21);
			this.overlayMessage.TabIndex = 2;
			this.overlayMessage.Text = "Parse failed asserts and overlay the details";
			this.overlayMessage.UseVisualStyleBackColor = true;
			// 
			// OptUnitTestVisualizer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.Controls.Add(this.overlayMessage);
			this.Controls.Add(this.arrowToFailed);
			this.Controls.Add(this.shadeAttribute);
			this.Name = "OptUnitTestVisualizer";
			this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.UnitTestVisualizer_PreparePage);
			this.RestoreDefaults += new DevExpress.CodeRush.Core.OptionsPage.RestoreDefaultsEventHandler(this.UnitTestVisualizer_RestoreDefaults);
			this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.UnitTestVisualizer_CommitChanges);
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
				return OptUnitTestVisualizer.GetCategory();
			}
		}
		///
		/// Returns the page name of this options page.
		///
		public override string PageName
		{
			get
			{
				return OptUnitTestVisualizer.GetPageName();
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

		private System.Windows.Forms.CheckBox shadeAttribute;
		private System.Windows.Forms.CheckBox arrowToFailed;
		private System.Windows.Forms.CheckBox overlayMessage;
	}
}
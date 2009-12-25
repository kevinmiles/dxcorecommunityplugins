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
			this.shortenLongStrings = new System.Windows.Forms.CheckBox();
			this.maxContextLength = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.convertEscape = new System.Windows.Forms.CheckBox();
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
			this.arrowToFailed.CheckedChanged += new System.EventHandler(this.arrowToFailed_CheckedChanged);
			// 
			// overlayMessage
			// 
			this.overlayMessage.AutoSize = true;
			this.overlayMessage.Checked = true;
			this.overlayMessage.CheckState = System.Windows.Forms.CheckState.Checked;
			this.overlayMessage.Location = new System.Drawing.Point(16, 150);
			this.overlayMessage.Name = "overlayMessage";
			this.overlayMessage.Size = new System.Drawing.Size(302, 21);
			this.overlayMessage.TabIndex = 2;
			this.overlayMessage.Text = "Parse failed asserts and overlay the details";
			this.overlayMessage.UseVisualStyleBackColor = true;
			// 
			// shortenLongStrings
			// 
			this.shortenLongStrings.AutoSize = true;
			this.shortenLongStrings.Checked = true;
			this.shortenLongStrings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.shortenLongStrings.Location = new System.Drawing.Point(36, 71);
			this.shortenLongStrings.Name = "shortenLongStrings";
			this.shortenLongStrings.Size = new System.Drawing.Size(396, 21);
			this.shortenLongStrings.TabIndex = 3;
			this.shortenLongStrings.Text = "Display only characters arround difference for long strings";
			this.shortenLongStrings.UseVisualStyleBackColor = true;
			// 
			// maxContextLength
			// 
			this.maxContextLength.Location = new System.Drawing.Point(217, 99);
			this.maxContextLength.Name = "maxContextLength";
			this.maxContextLength.Size = new System.Drawing.Size(100, 22);
			this.maxContextLength.TabIndex = 4;
			this.maxContextLength.Validating += new System.ComponentModel.CancelEventHandler(this.contextSize_Validating);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(59, 99);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(152, 17);
			this.label1.TabIndex = 5;
			this.label1.Text = "Shortened context size";
			// 
			// convertEscape
			// 
			this.convertEscape.AutoSize = true;
			this.convertEscape.Checked = true;
			this.convertEscape.CheckState = System.Windows.Forms.CheckState.Checked;
			this.convertEscape.Location = new System.Drawing.Point(36, 123);
			this.convertEscape.Name = "convertEscape";
			this.convertEscape.Size = new System.Drawing.Size(266, 21);
			this.convertEscape.TabIndex = 6;
			this.convertEscape.Text = "Make non-printable characters visible";
			this.convertEscape.UseVisualStyleBackColor = true;
			// 
			// OptUnitTestVisualizer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.Controls.Add(this.convertEscape);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.maxContextLength);
			this.Controls.Add(this.shortenLongStrings);
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
		private System.Windows.Forms.CheckBox shortenLongStrings;
		private System.Windows.Forms.TextBox maxContextLength;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox convertEscape;
	}
}
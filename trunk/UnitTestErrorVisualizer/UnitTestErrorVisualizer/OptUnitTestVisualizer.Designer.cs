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
			this.shadeAttribute.Location = new System.Drawing.Point(12, 13);
			this.shadeAttribute.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.shadeAttribute.Name = "shadeAttribute";
			this.shadeAttribute.Size = new System.Drawing.Size(214, 17);
			this.shadeAttribute.TabIndex = 0;
			this.shadeAttribute.Text = "Shade test attribute with pass/fail status";
			this.shadeAttribute.UseVisualStyleBackColor = true;
			// 
			// arrowToFailed
			// 
			this.arrowToFailed.AutoSize = true;
			this.arrowToFailed.Checked = true;
			this.arrowToFailed.CheckState = System.Windows.Forms.CheckState.Checked;
			this.arrowToFailed.Location = new System.Drawing.Point(12, 36);
			this.arrowToFailed.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.arrowToFailed.Name = "arrowToFailed";
			this.arrowToFailed.Size = new System.Drawing.Size(156, 17);
			this.arrowToFailed.TabIndex = 1;
			this.arrowToFailed.Text = "Draw arrow to failed asserts";
			this.arrowToFailed.UseVisualStyleBackColor = true;
			this.arrowToFailed.CheckedChanged += new System.EventHandler(this.arrowToFailed_CheckedChanged);
			// 
			// shortenLongStrings
			// 
			this.shortenLongStrings.AutoSize = true;
			this.shortenLongStrings.Checked = true;
			this.shortenLongStrings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.shortenLongStrings.Location = new System.Drawing.Point(27, 58);
			this.shortenLongStrings.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.shortenLongStrings.Name = "shortenLongStrings";
			this.shortenLongStrings.Size = new System.Drawing.Size(295, 17);
			this.shortenLongStrings.TabIndex = 3;
			this.shortenLongStrings.Text = "Display only characters arround difference for long strings";
			this.shortenLongStrings.UseVisualStyleBackColor = true;
			// 
			// maxContextLength
			// 
			this.maxContextLength.Location = new System.Drawing.Point(163, 80);
			this.maxContextLength.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.maxContextLength.Name = "maxContextLength";
			this.maxContextLength.Size = new System.Drawing.Size(76, 20);
			this.maxContextLength.TabIndex = 4;
			this.maxContextLength.Validating += new System.ComponentModel.CancelEventHandler(this.contextSize_Validating);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(44, 80);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(115, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Shortened context size";
			// 
			// convertEscape
			// 
			this.convertEscape.AutoSize = true;
			this.convertEscape.Checked = true;
			this.convertEscape.CheckState = System.Windows.Forms.CheckState.Checked;
			this.convertEscape.Location = new System.Drawing.Point(27, 100);
			this.convertEscape.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
			this.convertEscape.Name = "convertEscape";
			this.convertEscape.Size = new System.Drawing.Size(202, 17);
			this.convertEscape.TabIndex = 6;
			this.convertEscape.Text = "Make non-printable characters visible";
			this.convertEscape.UseVisualStyleBackColor = true;
			// 
			// OptUnitTestVisualizer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.convertEscape);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.maxContextLength);
			this.Controls.Add(this.shortenLongStrings);
			this.Controls.Add(this.arrowToFailed);
			this.Controls.Add(this.shadeAttribute);
			this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
		private System.Windows.Forms.CheckBox shortenLongStrings;
		private System.Windows.Forms.TextBox maxContextLength;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox convertEscape;
	}
}
using System;
using DevExpress.CodeRush.Core;

namespace Impromptu
{
	partial class ImpromptuOptions
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public ImpromptuOptions()
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
			this.dispalyTile = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// dispalyTile
			// 
			this.dispalyTile.AutoSize = true;
			this.dispalyTile.Location = new System.Drawing.Point(16, 14);
			this.dispalyTile.Name = "dispalyTile";
			this.dispalyTile.Size = new System.Drawing.Size(228, 21);
			this.dispalyTile.TabIndex = 0;
			this.dispalyTile.Text = "Display run icon next to method";
			this.dispalyTile.UseVisualStyleBackColor = true;
			// 
			// ImpromptuOptions
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.Controls.Add(this.dispalyTile);
			this.Name = "ImpromptuOptions";
			this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.ImpromptuOptions_PreparePage);
			this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.ImpromptuOptions_CommitChanges);
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
				return ImpromptuOptions.GetCategory();
			}
		}
		///
		/// Returns the page name of this options page.
		///
		public override string PageName
		{
			get
			{
				return ImpromptuOptions.GetPageName();
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

		private System.Windows.Forms.CheckBox dispalyTile;
	}
}
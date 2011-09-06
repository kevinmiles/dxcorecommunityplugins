using System;
using DevExpress.CodeRush.Core;

namespace DevExpress.CodeRush.Core
{
	partial class OptMultiSelect
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public OptMultiSelect()
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
			this.clrSelection = new DevExpress.CodeRush.UserControls.ColorSwatch();
			this.label1 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.clrSelection.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// clrSelection
			// 
			this.clrSelection.Color = System.Drawing.SystemColors.Control;
			this.clrSelection.Cursor = System.Windows.Forms.Cursors.Hand;
			this.clrSelection.DropDownOnMouseHover = false;
			this.clrSelection.InternalCloseOnLostFocus = true;
			this.clrSelection.InternalCloseOnOuterMouseClick = true;
			this.clrSelection.Location = new System.Drawing.Point(103, 12);
			this.clrSelection.Name = "clrSelection";
			this.clrSelection.Properties.Buttons.AddRange(new DevExpress.DXCore.Controls.XtraEditors.Controls.EditorButton[] {
            new DevExpress.DXCore.Controls.XtraEditors.Controls.EditorButton(DevExpress.DXCore.Controls.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.clrSelection.Properties.PopupSizeable = false;
			this.clrSelection.Properties.ShowPopupCloseButton = false;
			this.clrSelection.ShowColorName = false;
			this.clrSelection.Size = new System.Drawing.Size(152, 20);
			this.clrSelection.TabIndex = 0;
			this.clrSelection.ToolTip = null;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(15, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Selection Color:";
			// 
			// OptMultiSelect
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.clrSelection);
			this.Name = "OptMultiSelect";
			this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.OptMultiSelect_CommitChanges);
			this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.OptMultiSelect_PreparePage);
			this.RestoreDefaults += new DevExpress.CodeRush.Core.OptionsPage.RestoreDefaultsEventHandler(this.OptMultiSelect_RestoreDefaults);
			((System.ComponentModel.ISupportInitialize)(this.clrSelection.Properties)).EndInit();
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
				return OptMultiSelect.GetCategory();
			}
		}
		///
		/// Returns the page name of this options page.
		///
		public override string PageName
		{
			get
			{
				return OptMultiSelect.GetPageName();
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

		private DevExpress.CodeRush.UserControls.ColorSwatch clrSelection;
		private System.Windows.Forms.Label label1;
	}
}
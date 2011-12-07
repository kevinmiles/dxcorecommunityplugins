namespace CR_ImportNamespace
{
	partial class PlugIn1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public PlugIn1()
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
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlugIn1));
      this.actImportNamespace = new DevExpress.CodeRush.Core.Action(this.components);
      this.cpImportNamespace = new DevExpress.CodeRush.Core.CodeProvider(this.components);
      this.stImportNamespace = new DevExpress.CodeRush.Core.SmartTagProvider(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.actImportNamespace)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.cpImportNamespace)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.stImportNamespace)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // actImportNamespace
      // 
      this.actImportNamespace.ActionName = "ImportNamespace";
      this.actImportNamespace.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.actImportNamespace.Image = ((System.Drawing.Bitmap)(resources.GetObject("actImportNamespace.Image")));
      this.actImportNamespace.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.actImportNamespace.ToolbarItem.ButtonIsPressed = false;
      this.actImportNamespace.ToolbarItem.Caption = null;
      this.actImportNamespace.ToolbarItem.Image = null;
      this.actImportNamespace.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actImportNamespace_Execute);
      // 
      // cpImportNamespace
      // 
      this.cpImportNamespace.ActionHintText = "";
      this.cpImportNamespace.AutoActivate = true;
      this.cpImportNamespace.AutoUndo = false;
      this.cpImportNamespace.CodeIssueMessage = null;
      this.cpImportNamespace.Description = "Imports the appropriate namespace and assembly reference for the identifier at th" +
    "e caret.";
      this.cpImportNamespace.Image = ((System.Drawing.Bitmap)(resources.GetObject("cpImportNamespace.Image")));
      this.cpImportNamespace.NeedsSelection = false;
      this.cpImportNamespace.ProviderName = "Import Namespace";
      this.cpImportNamespace.Register = true;
      this.cpImportNamespace.RequiresSubMenuChoice = false;
      this.cpImportNamespace.SupportsAsyncMode = false;
      this.cpImportNamespace.CheckAvailability += new DevExpress.Refactor.Core.CheckAvailabilityEventHandler(this.cpImportNamespace_CheckAvailability);
      this.cpImportNamespace.Apply += new DevExpress.Refactor.Core.ApplyRefactoringEventHandler(this.cpImportNamespace_Apply);
      // 
      // stImportNamespace
      // 
      this.stImportNamespace.Description = "Import Namespace";
      this.stImportNamespace.Image = ((System.Drawing.Bitmap)(resources.GetObject("stImportNamespace.Image")));
      this.stImportNamespace.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.stImportNamespace.MenuOrder = 0;
      this.stImportNamespace.ProviderName = "Import Namespace";
      this.stImportNamespace.Register = true;
      this.stImportNamespace.ShowInContextMenu = false;
      this.stImportNamespace.ShowInPopupMenu = false;
      this.stImportNamespace.GetSmartTagItems += new DevExpress.CodeRush.Core.GetSmartTagItemsEventHandler(this.stImportNamespace_GetSmartTagItems);
      this.stImportNamespace.GetSmartTagItemColors += new DevExpress.CodeRush.Core.GetSmartTagItemColorsEventHandler(this.stImportNamespace_GetSmartTagItemColors);
      ((System.ComponentModel.ISupportInitialize)(this.actImportNamespace)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.cpImportNamespace)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.stImportNamespace)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.Action actImportNamespace;
		private DevExpress.CodeRush.Core.CodeProvider cpImportNamespace;
    private DevExpress.CodeRush.Core.SmartTagProvider stImportNamespace;
	}
}
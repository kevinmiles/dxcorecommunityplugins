namespace CR_FavoriteViews
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
			DevExpress.CodeRush.Core.Parameter parameter1 = new DevExpress.CodeRush.Core.Parameter(new DevExpress.CodeRush.Core.StringParameterType());
			this.actRestoreView = new DevExpress.CodeRush.Core.Action(this.components);
			((System.ComponentModel.ISupportInitialize)(this.actRestoreView)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// actRestoreView
			// 
			this.actRestoreView.ActionName = "RestoreView";
			this.actRestoreView.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.actRestoreView.Description = "Restores the specified view.";
			this.actRestoreView.Image = ((System.Drawing.Bitmap)(resources.GetObject("actRestoreView.Image")));
			this.actRestoreView.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
			parameter1.DefaultValue = "";
			parameter1.Description = "The name of the view to restore.";
			parameter1.Name = "ViewName";
			parameter1.Optional = false;
			this.actRestoreView.Parameters.Add(parameter1);
			this.actRestoreView.ToolbarItem.ButtonIsPressed = false;
			this.actRestoreView.ToolbarItem.Caption = null;
			this.actRestoreView.ToolbarItem.Image = null;
			this.actRestoreView.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actRestoreView_Execute);
			((System.ComponentModel.ISupportInitialize)(this.actRestoreView)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.Action actRestoreView;
	}
}
namespace MarkersToolWindow
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
			this.actShowMarkersToolWindow = new DevExpress.CodeRush.Core.Action(this.components);
			((System.ComponentModel.ISupportInitialize)(this.actShowMarkersToolWindow)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// actShowMarkersToolWindow
			// 
			this.actShowMarkersToolWindow.ActionName = "ShowMarkersToolWindow";
			this.actShowMarkersToolWindow.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.actShowMarkersToolWindow.Description = "Show the Markers Tool Window.";
			this.actShowMarkersToolWindow.Image = ((System.Drawing.Bitmap)(resources.GetObject("actShowMarkersToolWindow.Image")));
			this.actShowMarkersToolWindow.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.actShowMarkersToolWindow.ToolbarItem.ButtonIsPressed = false;
			this.actShowMarkersToolWindow.ToolbarItem.Caption = null;
			this.actShowMarkersToolWindow.ToolbarItem.Image = null;
			this.actShowMarkersToolWindow.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actShowMarkersToolWindow_Execute);
			((System.ComponentModel.ISupportInitialize)(this.actShowMarkersToolWindow)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.Action actShowMarkersToolWindow;
	}
}
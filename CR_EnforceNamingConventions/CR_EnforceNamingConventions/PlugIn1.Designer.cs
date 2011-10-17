namespace CR_EnforceNamingConventions
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
			this.actEnforceNamingConvention = new DevExpress.CodeRush.Core.Action(this.components);
			((System.ComponentModel.ISupportInitialize)(this.actEnforceNamingConvention)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// actEnforceNamingConvention
			// 
			this.actEnforceNamingConvention.ActionName = "Enforce Naming Convention";
			this.actEnforceNamingConvention.ButtonText = "Enforce Naming Convention";
			this.actEnforceNamingConvention.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.actEnforceNamingConvention.Description = "Enforces Naming Conventions...";
			this.actEnforceNamingConvention.Image = ((System.Drawing.Bitmap)(resources.GetObject("actEnforceNamingConvention.Image")));
			this.actEnforceNamingConvention.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.actEnforceNamingConvention.ParentMenu = "DevExpress";
			this.actEnforceNamingConvention.ToolbarItem.ButtonIsPressed = false;
			this.actEnforceNamingConvention.ToolbarItem.Caption = null;
			this.actEnforceNamingConvention.ToolbarItem.Image = null;
			this.actEnforceNamingConvention.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actEnforceNamingConvention_Execute);
			((System.ComponentModel.ISupportInitialize)(this.actEnforceNamingConvention)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.Action actEnforceNamingConvention;
	}
}
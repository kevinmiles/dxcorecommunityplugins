namespace CR_QuickPair
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
			DevExpress.CodeRush.Core.Parameter parameter4 = new DevExpress.CodeRush.Core.Parameter(new DevExpress.CodeRush.Core.StringParameterType());
			DevExpress.CodeRush.Core.Parameter parameter5 = new DevExpress.CodeRush.Core.Parameter(new DevExpress.CodeRush.Core.StringParameterType());
			DevExpress.CodeRush.Core.Parameter parameter6 = new DevExpress.CodeRush.Core.Parameter(new DevExpress.CodeRush.Core.BooleanParameterType());
			this.actQuickPair = new DevExpress.CodeRush.Core.Action(this.components);
			((System.ComponentModel.ISupportInitialize)(this.actQuickPair)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// actQuickPair
			// 
			this.actQuickPair.ActionName = "QuickPair";
			this.actQuickPair.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.actQuickPair.Description = "Adds a pair of specified delimiters at the caret (or surrounds a selection), opti" +
    "onally placing the caret (or selection) inside the delimiters.";
			this.actQuickPair.Image = ((System.Drawing.Bitmap)(resources.GetObject("actQuickPair.Image")));
			this.actQuickPair.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			parameter4.DefaultValue = "";
			parameter4.Description = "The left delimiter";
			parameter4.Name = "Left";
			parameter4.Optional = false;
			parameter5.DefaultValue = "";
			parameter5.Description = "The right delimiter.";
			parameter5.Name = "Right";
			parameter5.Optional = false;
			parameter6.DefaultValue = false;
			parameter6.Description = "If true, the caret (or selection) will be positioned between the delimiters after" +
    " this command executes.";
			parameter6.Name = "CaretBetween";
			parameter6.Optional = true;
			this.actQuickPair.Parameters.Add(parameter4);
			this.actQuickPair.Parameters.Add(parameter5);
			this.actQuickPair.Parameters.Add(parameter6);
			this.actQuickPair.ToolbarItem.ButtonIsPressed = false;
			this.actQuickPair.ToolbarItem.Caption = null;
			this.actQuickPair.ToolbarItem.Image = null;
			this.actQuickPair.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actQuickPair_Execute);
			
			((System.ComponentModel.ISupportInitialize)(this.actQuickPair)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.Action actQuickPair;
	}
}
namespace CR_OptionsInverter
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
			this.actReadWriteInverter = new DevExpress.CodeRush.Core.Action(this.components);
			this.actGenCodeTest = new DevExpress.CodeRush.Core.Action(this.components);
			((System.ComponentModel.ISupportInitialize)(this.actReadWriteInverter)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.actGenCodeTest)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// actReadWriteInverter
			// 
			this.actReadWriteInverter.ActionName = "ReadWriteInverter";
			this.actReadWriteInverter.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.actReadWriteInverter.Description = "Converts Read calls into Write calls.";
			this.actReadWriteInverter.Image = ((System.Drawing.Bitmap)(resources.GetObject("actReadWriteInverter.Image")));
			this.actReadWriteInverter.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.actReadWriteInverter.ToolbarItem.ButtonIsPressed = false;
			this.actReadWriteInverter.ToolbarItem.Caption = null;
			this.actReadWriteInverter.ToolbarItem.Image = null;
			this.actReadWriteInverter.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actReadWriteInverter_Execute);
			// 
			// actGenCodeTest
			// 
			this.actGenCodeTest.ActionName = "GenCodeTest";
			this.actGenCodeTest.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.actGenCodeTest.Description = "Generates some test code at the caret.";
			this.actGenCodeTest.Image = ((System.Drawing.Bitmap)(resources.GetObject("actGenCodeTest.Image")));
			this.actGenCodeTest.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.actGenCodeTest.ToolbarItem.ButtonIsPressed = false;
			this.actGenCodeTest.ToolbarItem.Caption = null;
			this.actGenCodeTest.ToolbarItem.Image = null;
			this.actGenCodeTest.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actGenCodeTest_Execute);
			((System.ComponentModel.ISupportInitialize)(this.actReadWriteInverter)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.actGenCodeTest)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.Action actReadWriteInverter;
		private DevExpress.CodeRush.Core.Action actGenCodeTest;
	}
}
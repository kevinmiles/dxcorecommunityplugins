namespace UnitTestErrorVisualizer
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
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// PlugIn1
			// 
			this.TileMouseEnter += new DevExpress.CodeRush.Core.TileEventHandler(this.PlugIn1_TileMouseEnter);
			this.EditorPaintForeground += new DevExpress.CodeRush.Core.EditorPaintEventHandler(this.PlugIn1_EditorPaintForeground);
			this.EditorPaintLanguageElement += new DevExpress.CodeRush.Core.EditorPaintLanguageElementEventHandler(this.PlugIn1_EditorPaintLanguageElement);
			this.TileMouseLeave += new DevExpress.CodeRush.Core.TileEventHandler(this.PlugIn1_TileMouseLeave);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion
	}
}
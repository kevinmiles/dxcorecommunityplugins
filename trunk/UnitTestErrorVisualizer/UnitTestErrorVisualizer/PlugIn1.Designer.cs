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
			this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.PlugIn1_OptionsChanged);
			this.DecorateLanguageElement += new DevExpress.CodeRush.Core.DecorateLanguageElementEventHandler(this.PlugIn1_DecorateLanguageElement);
			this.EditorPaintLanguageElement += new DevExpress.CodeRush.Core.EditorPaintLanguageElementEventHandler(this.PlugIn1_EditorPaintLanguageElement);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion
	}
}
namespace CR_NCover
{
	partial class PlugIn
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public PlugIn()
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
			// PlugIn
			// 
			this.EditorPaintBackground += new DevExpress.CodeRush.Core.EditorPaintEventHandler(this.WhenPaintingBackground);
			this.SolutionOpened += new DevExpress.CodeRush.Core.DefaultHandler(this.AfterOpeningSolution);
			this.TextChanged += new DevExpress.CodeRush.Core.TextChangedEventHandler(this.WhenTextChanges);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

	}
}
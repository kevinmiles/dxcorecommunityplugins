namespace CR_FavoriteViews
{
	[System.Runtime.InteropServices.Guid("7c1b5987-1bbd-428a-8de0-b1a856aadf9f")]
	partial class FavoriteViews
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private DevExpress.DXCore.PlugInCore.DXCoreEvents events;

		public FavoriteViews()
		{
			// Required for Windows.Forms Class Composition Designer support
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FavoriteViews));
			this.events = new DevExpress.DXCore.PlugInCore.DXCoreEvents(this.components);
			this.btnSaveCurrentView = new System.Windows.Forms.Button();
			this.lstViews = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.btnDeleteSelectedView = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.events)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// events
			// 
			this.events.AfterClosingSolution += new DevExpress.CodeRush.Core.DefaultHandler(this.events_AfterClosingSolution);
			this.events.SolutionOpened += new DevExpress.CodeRush.Core.DefaultHandler(this.events_SolutionOpened);
			// 
			// btnSaveCurrentView
			// 
			this.btnSaveCurrentView.Location = new System.Drawing.Point(0, 0);
			this.btnSaveCurrentView.Name = "btnSaveCurrentView";
			this.btnSaveCurrentView.Size = new System.Drawing.Size(125, 23);
			this.btnSaveCurrentView.TabIndex = 0;
			this.btnSaveCurrentView.Text = "Save Current View";
			this.btnSaveCurrentView.UseVisualStyleBackColor = true;
			this.btnSaveCurrentView.Click += new System.EventHandler(this.btnSaveCurrentView_Click);
			// 
			// lstViews
			// 
			this.lstViews.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.lstViews.FormattingEnabled = true;
			this.lstViews.Location = new System.Drawing.Point(3, 29);
			this.lstViews.Name = "lstViews";
			this.lstViews.Size = new System.Drawing.Size(283, 264);
			this.lstViews.TabIndex = 1;
			this.lstViews.SelectedIndexChanged += new System.EventHandler(this.lstViews_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(131, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(38, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Name:";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(172, 2);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(114, 20);
			this.textBox1.TabIndex = 3;
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(0, 313);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(142, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Ctrl+click a view to restore it.";
			// 
			// btnDeleteSelectedView
			// 
			this.btnDeleteSelectedView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDeleteSelectedView.Location = new System.Drawing.Point(0, 326);
			this.btnDeleteSelectedView.Name = "btnDeleteSelectedView";
			this.btnDeleteSelectedView.Size = new System.Drawing.Size(123, 23);
			this.btnDeleteSelectedView.TabIndex = 0;
			this.btnDeleteSelectedView.Text = "Delete Selected View";
			this.btnDeleteSelectedView.UseVisualStyleBackColor = true;
			this.btnDeleteSelectedView.Click += new System.EventHandler(this.btnDeleteSelectedView_Click);
			// 
			// FavoriteViews
			// 
			this.Controls.Add(this.label2);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.lstViews);
			this.Controls.Add(this.btnDeleteSelectedView);
			this.Controls.Add(this.btnSaveCurrentView);
			this.Image = ((System.Drawing.Bitmap)(resources.GetObject("$this.Image")));
			this.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.Name = "FavoriteViews";
			this.Size = new System.Drawing.Size(289, 349);
			((System.ComponentModel.ISupportInitialize)(this.events)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		#region ShowWindow
		///
		/// Displays this tool window.
		///
		public static EnvDTE.Window ShowWindow()
		{
			return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Show(typeof(FavoriteViews).GUID);
		}
		#endregion
		#region HideWindow
		///
		/// Hides this tool window.
		///
		public static EnvDTE.Window HideWindow()
		{
			return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Hide(typeof(FavoriteViews).GUID);
		}
		#endregion

		private System.Windows.Forms.Button btnSaveCurrentView;
		private System.Windows.Forms.ListBox lstViews;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnDeleteSelectedView;
	}
}
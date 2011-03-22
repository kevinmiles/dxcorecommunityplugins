using System.Runtime.InteropServices;

namespace MarkersToolWindow
{
	[Guid("e3e2d9ed-4e5b-4eac-8c71-83d14cc87cdb")]
	partial class MarkersToolWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private DevExpress.DXCore.PlugInCore.DXCoreEvents events;

		public MarkersToolWindow()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarkersToolWindow));
			this.events = new DevExpress.DXCore.PlugInCore.DXCoreEvents(this.components);
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.lstMarkers = new System.Windows.Forms.ListBox();
			this.codePreview = new DevExpress.CodeRush.UserControls.CodeView();
			((System.ComponentModel.ISupportInitialize)(this.events)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.codePreview)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// events
			// 
			this.events.DocumentClosing += new DevExpress.CodeRush.Core.DocumentEventHandler(this.events_DocumentClosing);
			this.events.MarkerDropped += new DevExpress.CodeRush.Core.MarkerEventHandler(this.events_MarkerDropped);
			this.events.MarkerCollected += new DevExpress.CodeRush.Core.MarkerEventHandler(this.events_MarkerCollected);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lstMarkers);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.codePreview);
			this.splitContainer1.Size = new System.Drawing.Size(318, 417);
			this.splitContainer1.SplitterDistance = 106;
			this.splitContainer1.TabIndex = 0;
			// 
			// lstMarkers
			// 
			this.lstMarkers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstMarkers.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.lstMarkers.FormattingEnabled = true;
			this.lstMarkers.ItemHeight = 16;
			this.lstMarkers.Location = new System.Drawing.Point(0, 0);
			this.lstMarkers.Name = "lstMarkers";
			this.lstMarkers.Size = new System.Drawing.Size(318, 106);
			this.lstMarkers.TabIndex = 0;
			this.lstMarkers.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.lstMarkers_DrawItem);
			this.lstMarkers.SelectedIndexChanged += new System.EventHandler(this.lstMarkers_SelectedIndexChanged);
			this.lstMarkers.DoubleClick += new System.EventHandler(this.lstMarkers_DoubleClick);
			// 
			// codePreview
			// 
			this.codePreview.BorderColor = System.Drawing.Color.Empty;
			this.codePreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.codePreview.EmphasisRange = ((DevExpress.CodeRush.StructuralParser.SourceRange)(resources.GetObject("codePreview.EmphasisRange")));
			this.codePreview.LanguageID = "CSharp";
			this.codePreview.LeftPixel = 0;
			this.codePreview.Location = new System.Drawing.Point(0, 0);
			this.codePreview.Name = "codePreview";
			this.codePreview.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.codePreview.Size = new System.Drawing.Size(318, 307);
			this.codePreview.TabIndex = 0;
			this.codePreview.TextChangeEffect = DevExpress.CodeRush.UserControls.TextChangeEffect.None;
			this.codePreview.TopLine = 0;
			this.codePreview.TransitionTime = 900;
			this.codePreview.Painted += new System.Windows.Forms.PaintEventHandler(this.codePreview_Painted);
			// 
			// MarkersToolWindow
			// 
			this.Controls.Add(this.splitContainer1);
			this.Image = ((System.Drawing.Bitmap)(resources.GetObject("$this.Image")));
			this.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.Name = "MarkersToolWindow";
			this.Size = new System.Drawing.Size(318, 417);
			((System.ComponentModel.ISupportInitialize)(this.events)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.codePreview)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		#region ShowWindow
		///
		/// Displays this tool window.
		///
		public static EnvDTE.Window ShowWindow()
		{
			return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Show(typeof(MarkersToolWindow).GUID);
		}
		#endregion
		#region HideWindow
		///
		/// Hides this tool window.
		///
		public static EnvDTE.Window HideWindow()
		{
			return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Hide(typeof(MarkersToolWindow).GUID);
		}
		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox lstMarkers;
		private DevExpress.CodeRush.UserControls.CodeView codePreview;

	}
}
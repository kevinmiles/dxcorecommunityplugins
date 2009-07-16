using System.Runtime.InteropServices;

namespace CR_Xtras
{
	[Guid("63d414ef-236a-456b-9860-e675a2bce7d8")]
	partial class ToolWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private DevExpress.DXCore.PlugInCore.DXCoreEvents events;

		public ToolWindow()
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
			this.events = new DevExpress.DXCore.PlugInCore.DXCoreEvents(this.components);
			((System.ComponentModel.ISupportInitialize)(this.events)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.events)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
		}

		#endregion

		#region ShowWindow
		///
		/// Displays this tool window.
		///
		public static EnvDTE.Window ShowWindow()
		{
			return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Show(typeof(ToolWindow).GUID);
		}
		#endregion
		#region HideWindow
		///
		/// Hides this tool window.
		///
		public static EnvDTE.Window HideWindow()
		{
			return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Hide(typeof(ToolWindow).GUID);
		}
		#endregion
	}
}
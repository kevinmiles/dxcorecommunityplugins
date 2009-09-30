using System.Runtime.InteropServices;

namespace DX_ContextLab
{
	[Guid("1a616b6a-50e1-43a4-b349-f3014b38d7b6")]
	partial class ContextWindow
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private DevExpress.DXCore.PlugInCore.DXCoreEvents events;

		public ContextWindow()
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContextWindow));
			this.events = new DevExpress.DXCore.PlugInCore.DXCoreEvents(this.components);
			this.contextList = new System.Windows.Forms.ListBox();
			this.contextPolling = new System.Windows.Forms.CheckBox();
			this.evaluateContextAction = new DevExpress.CodeRush.Core.Action(this.components);
			this.pollingTimer = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.events)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.evaluateContextAction)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// contextList
			// 
			this.contextList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.contextList.FormattingEnabled = true;
			this.contextList.Location = new System.Drawing.Point(3, 29);
			this.contextList.Name = "contextList";
			this.contextList.Size = new System.Drawing.Size(144, 368);
			this.contextList.Sorted = true;
			this.contextList.TabIndex = 0;
			// 
			// contextPolling
			// 
			this.contextPolling.AutoSize = true;
			this.contextPolling.Location = new System.Drawing.Point(4, 4);
			this.contextPolling.Name = "contextPolling";
			this.contextPolling.Size = new System.Drawing.Size(130, 17);
			this.contextPolling.TabIndex = 1;
			this.contextPolling.Text = "Enable context polling";
			this.contextPolling.UseVisualStyleBackColor = true;
			this.contextPolling.CheckedChanged += new System.EventHandler(this.ContextPolling_CheckedChanged);
			// 
			// evaluateContextAction
			// 
			this.evaluateContextAction.ActionName = "Update Context Lab";
			this.evaluateContextAction.ButtonText = "Update Context Lab";
			this.evaluateContextAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.evaluateContextAction.Description = "Manually updates the Context Lab plugin with the caret\'s current context.";
			this.evaluateContextAction.Image = ((System.Drawing.Bitmap)(resources.GetObject("evaluateContextAction.Image")));
			this.evaluateContextAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.evaluateContextAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.EvaluateContextAction_Execute);
			this.evaluateContextAction.QueryStatus += new DevExpress.CodeRush.Core.QueryStatusEventHandler(this.EvaluateContextAction_QueryStatus);
			// 
			// pollingTimer
			// 
			this.pollingTimer.Interval = 1000;
			this.pollingTimer.Tick += new System.EventHandler(this.PollingTimer_Tick);
			// 
			// ContextWindow
			// 
			this.Controls.Add(this.contextPolling);
			this.Controls.Add(this.contextList);
			this.Image = ((System.Drawing.Bitmap)(resources.GetObject("$this.Image")));
			this.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.Name = "ContextWindow";
			this.Size = new System.Drawing.Size(150, 400);
			((System.ComponentModel.ISupportInitialize)(this.events)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.evaluateContextAction)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		/// <summary>
		/// Displays the tool window.
		/// </summary>
		/// <returns>A reference to the displayed window.</returns>
		public static EnvDTE.Window ShowWindow()
		{
			return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Show(typeof(ContextWindow).GUID);
		}

		/// <summary>
		/// Hides the tool window.
		/// </summary>
		/// <returns>A reference to the hidden window.</returns>
		public static EnvDTE.Window HideWindow()
		{
			return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Hide(typeof(ContextWindow).GUID);
		}

		private System.Windows.Forms.ListBox contextList;
		private System.Windows.Forms.CheckBox contextPolling;
		private DevExpress.CodeRush.Core.Action evaluateContextAction;
		private System.Windows.Forms.Timer pollingTimer;
	}
}
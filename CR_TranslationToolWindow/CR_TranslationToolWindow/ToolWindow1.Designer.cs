using System.Runtime.InteropServices;

namespace CR_TranslatorToolWindow
{
	[Guid("3cc5d46c-4ab0-450f-b4cc-757289d4d7bf")]
	partial class ToolWindow1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private DevExpress.DXCore.PlugInCore.DXCoreEvents events;

		public ToolWindow1()
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton3 = new System.Windows.Forms.RadioButton();
			this.radioButton4 = new System.Windows.Forms.RadioButton();
			this.codeView1 = new DevExpress.CodeRush.UserControls.CodeView();
			((System.ComponentModel.ISupportInitialize)(this.events)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.codeView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// events
			// 
			this.events.LanguageElementActivated += new DevExpress.CodeRush.Core.LanguageElementActivatedEventHandler(this.events_LanguageElementActivated);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.radioButton4);
			this.panel1.Controls.Add(this.radioButton3);
			this.panel1.Controls.Add(this.radioButton2);
			this.panel1.Controls.Add(this.radioButton1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(347, 40);
			this.panel1.TabIndex = 0;
			// 
			// radioButton1
			// 
			this.radioButton1.AutoSize = true;
			this.radioButton1.Checked = true;
			this.radioButton1.Location = new System.Drawing.Point(4, 4);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(49, 17);
			this.radioButton1.TabIndex = 0;
			this.radioButton1.TabStop = true;
			this.radioButton1.Text = "Basic";
			this.radioButton1.UseVisualStyleBackColor = true;
			this.radioButton1.Click += new System.EventHandler(this.LanguageChanged);
			// 
			// radioButton2
			// 
			this.radioButton2.AutoSize = true;
			this.radioButton2.Location = new System.Drawing.Point(59, 4);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(60, 17);
			this.radioButton2.TabIndex = 0;
			this.radioButton2.Text = "CSharp";
			this.radioButton2.UseVisualStyleBackColor = true;
			this.radioButton2.Click += new System.EventHandler(this.LanguageChanged);
			// 
			// radioButton3
			// 
			this.radioButton3.AutoSize = true;
			this.radioButton3.Location = new System.Drawing.Point(125, 4);
			this.radioButton3.Name = "radioButton3";
			this.radioButton3.Size = new System.Drawing.Size(75, 17);
			this.radioButton3.TabIndex = 0;
			this.radioButton3.Text = "JavaScript";
			this.radioButton3.UseVisualStyleBackColor = true;
			this.radioButton3.Click += new System.EventHandler(this.LanguageChanged);
			// 
			// radioButton4
			// 
			this.radioButton4.AutoSize = true;
			this.radioButton4.Location = new System.Drawing.Point(206, 4);
			this.radioButton4.Name = "radioButton4";
			this.radioButton4.Size = new System.Drawing.Size(59, 17);
			this.radioButton4.TabIndex = 0;
			this.radioButton4.Text = "C/C++";
			this.radioButton4.UseVisualStyleBackColor = true;
			this.radioButton4.Click += new System.EventHandler(this.LanguageChanged);
			// 
			// codeView1
			// 
			this.codeView1.BorderColor = System.Drawing.Color.Empty;
			this.codeView1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.codeView1.LanguageID = "CSharp";
			this.codeView1.LeftPixel = 0;
			this.codeView1.Location = new System.Drawing.Point(0, 40);
			this.codeView1.Name = "codeView1";
			this.codeView1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.codeView1.Size = new System.Drawing.Size(347, 110);
			this.codeView1.TabIndex = 1;
			this.codeView1.TextChangeEffect = DevExpress.CodeRush.UserControls.TextChangeEffect.None;
			this.codeView1.TopLine = 0;
			this.codeView1.TransitionTime = 900;
			// 
			// ToolWindow1
			// 
			this.Controls.Add(this.codeView1);
			this.Controls.Add(this.panel1);
			this.Name = "ToolWindow1";
			this.Size = new System.Drawing.Size(347, 150);
			((System.ComponentModel.ISupportInitialize)(this.events)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.codeView1)).EndInit();
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
			return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Show(typeof(ToolWindow1).GUID);
		}
		#endregion
		#region HideWindow
		///
		/// Hides this tool window.
		///
		public static EnvDTE.Window HideWindow()
		{
			return DevExpress.CodeRush.Core.CodeRush.ToolWindows.Hide(typeof(ToolWindow1).GUID);
		}
		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.RadioButton radioButton4;
		private System.Windows.Forms.RadioButton radioButton3;
		private System.Windows.Forms.RadioButton radioButton2;
		private System.Windows.Forms.RadioButton radioButton1;
		private DevExpress.CodeRush.UserControls.CodeView codeView1;
	}
}
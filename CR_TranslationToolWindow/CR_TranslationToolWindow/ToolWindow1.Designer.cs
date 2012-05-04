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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.optSourceFile = new System.Windows.Forms.RadioButton();
			this.optSourceMember = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.optCpp = new System.Windows.Forms.RadioButton();
			this.optBasic = new System.Windows.Forms.RadioButton();
			this.optJavaScript = new System.Windows.Forms.RadioButton();
			this.optCSharp = new System.Windows.Forms.RadioButton();
			this.codeView1 = new DevExpress.CodeRush.UserControls.CodeView();
			((System.ComponentModel.ISupportInitialize)(this.events)).BeginInit();
			this.panel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
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
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(642, 40);
			this.panel1.TabIndex = 0;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.optSourceFile);
			this.groupBox2.Controls.Add(this.optSourceMember);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(280, 0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(362, 40);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Source";
			// 
			// optSourceFile
			// 
			this.optSourceFile.AutoSize = true;
			this.optSourceFile.Location = new System.Drawing.Point(134, 16);
			this.optSourceFile.Name = "optSourceFile";
			this.optSourceFile.Size = new System.Drawing.Size(72, 17);
			this.optSourceFile.TabIndex = 0;
			this.optSourceFile.Text = "Whole file";
			this.optSourceFile.UseVisualStyleBackColor = true;
			this.optSourceFile.CheckedChanged += new System.EventHandler(this.SourceChanged);
			// 
			// optSourceMember
			// 
			this.optSourceMember.AutoSize = true;
			this.optSourceMember.Checked = true;
			this.optSourceMember.Location = new System.Drawing.Point(25, 16);
			this.optSourceMember.Name = "optSourceMember";
			this.optSourceMember.Size = new System.Drawing.Size(103, 17);
			this.optSourceMember.TabIndex = 0;
			this.optSourceMember.TabStop = true;
			this.optSourceMember.Text = "Current Member";
			this.optSourceMember.UseVisualStyleBackColor = true;
			this.optSourceMember.CheckedChanged += new System.EventHandler(this.SourceChanged);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.optCpp);
			this.groupBox1.Controls.Add(this.optBasic);
			this.groupBox1.Controls.Add(this.optJavaScript);
			this.groupBox1.Controls.Add(this.optCSharp);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Left;
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(280, 40);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Language";
			// 
			// optCpp
			// 
			this.optCpp.AutoSize = true;
			this.optCpp.Location = new System.Drawing.Point(208, 16);
			this.optCpp.Name = "optCpp";
			this.optCpp.Size = new System.Drawing.Size(59, 17);
			this.optCpp.TabIndex = 0;
			this.optCpp.Text = "C/C++";
			this.optCpp.UseVisualStyleBackColor = true;
			this.optCpp.Click += new System.EventHandler(this.TranslationLanguageChanged);
			// 
			// optBasic
			// 
			this.optBasic.AutoSize = true;
			this.optBasic.Checked = true;
			this.optBasic.Location = new System.Drawing.Point(6, 16);
			this.optBasic.Name = "optBasic";
			this.optBasic.Size = new System.Drawing.Size(49, 17);
			this.optBasic.TabIndex = 0;
			this.optBasic.TabStop = true;
			this.optBasic.Text = "Basic";
			this.optBasic.UseVisualStyleBackColor = true;
			this.optBasic.Click += new System.EventHandler(this.TranslationLanguageChanged);
			// 
			// optJavaScript
			// 
			this.optJavaScript.AutoSize = true;
			this.optJavaScript.Location = new System.Drawing.Point(127, 16);
			this.optJavaScript.Name = "optJavaScript";
			this.optJavaScript.Size = new System.Drawing.Size(75, 17);
			this.optJavaScript.TabIndex = 0;
			this.optJavaScript.Text = "JavaScript";
			this.optJavaScript.UseVisualStyleBackColor = true;
			this.optJavaScript.Click += new System.EventHandler(this.TranslationLanguageChanged);
			// 
			// optCSharp
			// 
			this.optCSharp.AutoSize = true;
			this.optCSharp.Location = new System.Drawing.Point(61, 16);
			this.optCSharp.Name = "optCSharp";
			this.optCSharp.Size = new System.Drawing.Size(60, 17);
			this.optCSharp.TabIndex = 0;
			this.optCSharp.Text = "CSharp";
			this.optCSharp.UseVisualStyleBackColor = true;
			this.optCSharp.Click += new System.EventHandler(this.TranslationLanguageChanged);
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
			this.codeView1.Size = new System.Drawing.Size(642, 110);
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
			this.Size = new System.Drawing.Size(642, 150);
			((System.ComponentModel.ISupportInitialize)(this.events)).EndInit();
			this.panel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
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
		private System.Windows.Forms.RadioButton optCpp;
		private System.Windows.Forms.RadioButton optJavaScript;
		private System.Windows.Forms.RadioButton optCSharp;
		private System.Windows.Forms.RadioButton optBasic;
		private DevExpress.CodeRush.UserControls.CodeView codeView1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.RadioButton optSourceFile;
		private System.Windows.Forms.RadioButton optSourceMember;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}
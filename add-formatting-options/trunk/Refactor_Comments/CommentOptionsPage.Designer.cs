using System;
using DevExpress.CodeRush.Core;

namespace Refactor_Comments
{
	partial class CommentOptionsPage
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		public CommentOptionsPage()
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
			Refactor_Comments.MultiLineCommentOptions multiLineCommentOptions1 = new Refactor_Comments.MultiLineCommentOptions();
			Refactor_Comments.MultiLineCommentOptions multiLineCommentOptions2 = new Refactor_Comments.MultiLineCommentOptions();
			Refactor_Comments.MultiLineCommentOptions multiLineCommentOptions3 = new Refactor_Comments.MultiLineCommentOptions();
			this.jScriptMultiLineCommentOptionsControl = new Refactor_Comments.MultiLineCommentOptionsControl();
			this.cPlusPlusMultiLineCommentOptionsControl = new Refactor_Comments.MultiLineCommentOptionsControl();
			this.cSharpMultiLineCommentOptionsControl = new Refactor_Comments.MultiLineCommentOptionsControl();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// jScriptMultiLineCommentOptionsControl
			// 
			this.jScriptMultiLineCommentOptionsControl.LanguageId = "JavaScript";
			this.jScriptMultiLineCommentOptionsControl.LineLeaderText = "*";
			this.jScriptMultiLineCommentOptionsControl.Location = new System.Drawing.Point(3, 184);
			this.jScriptMultiLineCommentOptionsControl.Name = "jScriptMultiLineCommentOptionsControl";
			multiLineCommentOptions1.AddLineLeaderString = false;
			multiLineCommentOptions1.TerminatorOnLastCommentLine = false;
			this.jScriptMultiLineCommentOptionsControl.Options = multiLineCommentOptions1;
			this.jScriptMultiLineCommentOptionsControl.PreviewGenerator = null;
			this.jScriptMultiLineCommentOptionsControl.Size = new System.Drawing.Size(260, 175);
			this.jScriptMultiLineCommentOptionsControl.TabIndex = 2;
			// 
			// cPlusPlusMultiLineCommentOptionsControl
			// 
			this.cPlusPlusMultiLineCommentOptionsControl.LanguageId = "C/C++";
			this.cPlusPlusMultiLineCommentOptionsControl.LineLeaderText = "*";
			this.cPlusPlusMultiLineCommentOptionsControl.Location = new System.Drawing.Point(265, 3);
			this.cPlusPlusMultiLineCommentOptionsControl.Name = "cPlusPlusMultiLineCommentOptionsControl";
			multiLineCommentOptions2.AddLineLeaderString = false;
			multiLineCommentOptions2.TerminatorOnLastCommentLine = false;
			this.cPlusPlusMultiLineCommentOptionsControl.Options = multiLineCommentOptions2;
			this.cPlusPlusMultiLineCommentOptionsControl.PreviewGenerator = null;
			this.cPlusPlusMultiLineCommentOptionsControl.Size = new System.Drawing.Size(265, 175);
			this.cPlusPlusMultiLineCommentOptionsControl.TabIndex = 1;
			// 
			// cSharpMultiLineCommentOptionsControl
			// 
			this.cSharpMultiLineCommentOptionsControl.LanguageId = "C#";
			this.cSharpMultiLineCommentOptionsControl.LineLeaderText = "*";
			this.cSharpMultiLineCommentOptionsControl.Location = new System.Drawing.Point(3, 3);
			this.cSharpMultiLineCommentOptionsControl.Name = "cSharpMultiLineCommentOptionsControl";
			multiLineCommentOptions3.AddLineLeaderString = false;
			multiLineCommentOptions3.TerminatorOnLastCommentLine = false;
			this.cSharpMultiLineCommentOptionsControl.Options = multiLineCommentOptions3;
			this.cSharpMultiLineCommentOptionsControl.PreviewGenerator = null;
			this.cSharpMultiLineCommentOptionsControl.Size = new System.Drawing.Size(265, 175);
			this.cSharpMultiLineCommentOptionsControl.TabIndex = 0;
			// 
			// CommentOptionsPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.Controls.Add(this.jScriptMultiLineCommentOptionsControl);
			this.Controls.Add(this.cPlusPlusMultiLineCommentOptionsControl);
			this.Controls.Add(this.cSharpMultiLineCommentOptionsControl);
			this.Name = "CommentOptionsPage";
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		///
		/// Gets a DecoupledStorage instance for this options page.
		///
		public static DecoupledStorage Storage
		{
			get
			{
				return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName());
			}
		}
		///
		/// Returns the category of this options page.
		///
		public override string Category
		{
			get
			{
				return CommentOptionsPage.GetCategory();
			}
		}
		///
		/// Returns the page name of this options page.
		///
		public override string PageName
		{
			get
			{
				return CommentOptionsPage.GetPageName();
			}
		}
		///
		/// Returns the full path (Category + PageName) of this options page.
		///
		public static string FullPath
		{
			get
			{
				return GetCategory() + "\\" + GetPageName();
			}
		}

		///
		/// Displays the DXCore options dialog and selects this page.
		///
		public new static void Show()
		{
			DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath);
		}

		private MultiLineCommentOptionsControl cSharpMultiLineCommentOptionsControl;
		private MultiLineCommentOptionsControl cPlusPlusMultiLineCommentOptionsControl;
		private MultiLineCommentOptionsControl jScriptMultiLineCommentOptionsControl;
	}
}
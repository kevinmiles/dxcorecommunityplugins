namespace Refactor_Comments
{
	partial class MultiLineCommentOptionsControl
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MultiLineCommentOptionsControl));
			this.optionGroup = new System.Windows.Forms.GroupBox();
			this.exampleBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.useLineLeaders = new System.Windows.Forms.CheckBox();
			this.sameLineTerminator = new System.Windows.Forms.CheckBox();
			this.optionGroup.SuspendLayout();
			this.SuspendLayout();
			// 
			// optionGroup
			// 
			this.optionGroup.Controls.Add(this.exampleBox);
			this.optionGroup.Controls.Add(this.label1);
			this.optionGroup.Controls.Add(this.useLineLeaders);
			this.optionGroup.Controls.Add(this.sameLineTerminator);
			resources.ApplyResources(this.optionGroup, "optionGroup");
			this.optionGroup.Name = "optionGroup";
			this.optionGroup.TabStop = false;
			// 
			// exampleBox
			// 
			this.exampleBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.exampleBox.Cursor = System.Windows.Forms.Cursors.Arrow;
			resources.ApplyResources(this.exampleBox, "exampleBox");
			this.exampleBox.Name = "exampleBox";
			this.exampleBox.ReadOnly = true;
			// 
			// label1
			// 
			resources.ApplyResources(this.label1, "label1");
			this.label1.Name = "label1";
			// 
			// useLineLeaders
			// 
			resources.ApplyResources(this.useLineLeaders, "useLineLeaders");
			this.useLineLeaders.Name = "useLineLeaders";
			this.useLineLeaders.UseVisualStyleBackColor = true;
			this.useLineLeaders.CheckedChanged += new System.EventHandler(this.useLineLeaders_CheckedChanged);
			// 
			// sameLineTerminator
			// 
			resources.ApplyResources(this.sameLineTerminator, "sameLineTerminator");
			this.sameLineTerminator.Name = "sameLineTerminator";
			this.sameLineTerminator.UseVisualStyleBackColor = true;
			this.sameLineTerminator.CheckedChanged += new System.EventHandler(this.sameLineTerminator_CheckedChanged);
			// 
			// MultiLineCommentOptionsControl
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.optionGroup);
			this.Name = "MultiLineCommentOptionsControl";
			this.optionGroup.ResumeLayout(false);
			this.optionGroup.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox optionGroup;
		private System.Windows.Forms.TextBox exampleBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox useLineLeaders;
		private System.Windows.Forms.CheckBox sameLineTerminator;
	}
}

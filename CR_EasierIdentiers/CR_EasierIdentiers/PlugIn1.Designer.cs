namespace CR_EasierIdentiers
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
			this.components = new System.ComponentModel.Container();
			this.ctxConvertingSpacesToCamelCase = new DevExpress.CodeRush.Extensions.ContextProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.ctxConvertingSpacesToCamelCase)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// ctxConvertingSpacesToCamelCase
			// 
			this.ctxConvertingSpacesToCamelCase.Description = "Satisfied if we\'re converting spaces to CamelCase.";
			this.ctxConvertingSpacesToCamelCase.ProviderName = "System\\ConvertingSpacesToCamelCase";
			this.ctxConvertingSpacesToCamelCase.Register = true;
			this.ctxConvertingSpacesToCamelCase.ContextSatisfied += new DevExpress.CodeRush.Core.ContextSatisfiedEventHandler(this.ctxConvertingSpacesToCamelCase_ContextSatisfied);
			// 
			// PlugIn1
			// 
			this.CommandExecuting += new DevExpress.CodeRush.Core.CommandExecutingEventHandler(this.PlugIn1_CommandExecuting);
			this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.PlugIn1_OptionsChanged);
			this.EditorCharacterTyping += new DevExpress.CodeRush.Core.EditorCharacterTypingEventHandler(this.PlugIn1_EditorCharacterTyping);
			this.KeyPressed += new DevExpress.CodeRush.Core.KeyPressedEventHandler(this.PlugIn1_KeyPressed);
			((System.ComponentModel.ISupportInitialize)(this.ctxConvertingSpacesToCamelCase)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Extensions.ContextProvider ctxConvertingSpacesToCamelCase;
	}
}
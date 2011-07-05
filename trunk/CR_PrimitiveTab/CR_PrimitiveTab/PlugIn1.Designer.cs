namespace CR_PrimitiveTab
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
			this.spFindPrimitives = new DevExpress.CodeRush.Core.SearcherProvider();
			this.ctxInPrimitive = new DevExpress.CodeRush.Extensions.ContextProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.spFindPrimitives)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ctxInPrimitive)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// spFindPrimitives
			// 
			this.spFindPrimitives.ActiveFileOnly = false;
			this.spFindPrimitives.Description = "Finds matching primitives.";
			this.spFindPrimitives.ProviderName = "Find Primitives";
			this.spFindPrimitives.Register = true;
			this.spFindPrimitives.UseForNavigation = true;
			this.spFindPrimitives.UseForRenaming = false;
			this.spFindPrimitives.LanguageSupported += new DevExpress.CodeRush.Core.LanguageSupportedEventHandler(this.spFindPrimitives_LanguageSupported);
			this.spFindPrimitives.CheckAvailability += new DevExpress.CodeRush.Core.CheckSearchAvailabilityEventHandler(this.spFindPrimitives_CheckAvailability);
			this.spFindPrimitives.SearchReferences += new DevExpress.CodeRush.Core.SearchReferencesEventHandler(this.spFindPrimitives_SearchReferences);
			// 
			// ctxInPrimitive
			// 
			this.ctxInPrimitive.Description = "Satisfied if the caret is inside a primitive expression.";
			this.ctxInPrimitive.ProviderName = "Editor\\Code\\InPrimitive";
			this.ctxInPrimitive.Register = true;
			this.ctxInPrimitive.ContextSatisfied += new DevExpress.CodeRush.Core.ContextSatisfiedEventHandler(this.ctxInPrimitive_ContextSatisfied);
			// 
			// PlugIn1
			// 
			((System.ComponentModel.ISupportInitialize)(this.spFindPrimitives)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ctxInPrimitive)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.SearcherProvider spFindPrimitives;
		private DevExpress.CodeRush.Extensions.ContextProvider ctxInPrimitive;
	}
}
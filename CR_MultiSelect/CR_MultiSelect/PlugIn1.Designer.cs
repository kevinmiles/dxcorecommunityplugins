namespace CR_MultiSelect
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlugIn1));
			this.actMultiSelectAdd = new DevExpress.CodeRush.Core.Action(this.components);
			this.actMultiSelectClear = new DevExpress.CodeRush.Core.Action(this.components);
			this.actMultiSelectUndo = new DevExpress.CodeRush.Core.Action(this.components);
			this.actMultiSelectRedo = new DevExpress.CodeRush.Core.Action(this.components);
			this.ctxMultiSelectExists = new DevExpress.CodeRush.Extensions.ContextProvider(this.components);
			this.ctxMultiSelectRedoAvailable = new DevExpress.CodeRush.Extensions.ContextProvider(this.components);
			this.actMultiSelectIntegratedPaste = new DevExpress.CodeRush.Core.Action(this.components);
			((System.ComponentModel.ISupportInitialize)(this.actMultiSelectAdd)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.actMultiSelectClear)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.actMultiSelectUndo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.actMultiSelectRedo)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ctxMultiSelectExists)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ctxMultiSelectRedoAvailable)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.actMultiSelectIntegratedPaste)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// actMultiSelectAdd
			// 
			this.actMultiSelectAdd.ActionName = "MultiSelectAdd";
			this.actMultiSelectAdd.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.actMultiSelectAdd.Description = "Adds the current selection to the multi-select list.";
			this.actMultiSelectAdd.Image = ((System.Drawing.Bitmap)(resources.GetObject("actMultiSelectAdd.Image")));
			this.actMultiSelectAdd.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.actMultiSelectAdd.ToolbarItem.ButtonIsPressed = false;
			this.actMultiSelectAdd.ToolbarItem.Caption = null;
			this.actMultiSelectAdd.ToolbarItem.Image = null;
			this.actMultiSelectAdd.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actMultiSelectAdd_Execute);
			// 
			// actMultiSelectClear
			// 
			this.actMultiSelectClear.ActionName = "MultiSelectClear";
			this.actMultiSelectClear.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.actMultiSelectClear.Description = "Clears the multi-select list for the active view.";
			this.actMultiSelectClear.Image = ((System.Drawing.Bitmap)(resources.GetObject("actMultiSelectClear.Image")));
			this.actMultiSelectClear.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.actMultiSelectClear.ToolbarItem.ButtonIsPressed = false;
			this.actMultiSelectClear.ToolbarItem.Caption = null;
			this.actMultiSelectClear.ToolbarItem.Image = null;
			this.actMultiSelectClear.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actMultiSelectClear_Execute);
			// 
			// actMultiSelectUndo
			// 
			this.actMultiSelectUndo.ActionName = "MultiSelectUndo";
			this.actMultiSelectUndo.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.actMultiSelectUndo.Description = "Removes the most recently-added selection from the multi-select list.";
			this.actMultiSelectUndo.Image = ((System.Drawing.Bitmap)(resources.GetObject("actMultiSelectUndo.Image")));
			this.actMultiSelectUndo.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.actMultiSelectUndo.ToolbarItem.ButtonIsPressed = false;
			this.actMultiSelectUndo.ToolbarItem.Caption = null;
			this.actMultiSelectUndo.ToolbarItem.Image = null;
			this.actMultiSelectUndo.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actMultiSelectUndo_Execute);
			// 
			// actMultiSelectRedo
			// 
			this.actMultiSelectRedo.ActionName = "MultiSelectRedo";
			this.actMultiSelectRedo.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
			this.actMultiSelectRedo.Description = "Adds the most recently-undone (removed) selection back to the multi-select list.";
			this.actMultiSelectRedo.Image = ((System.Drawing.Bitmap)(resources.GetObject("actMultiSelectRedo.Image")));
			this.actMultiSelectRedo.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.actMultiSelectRedo.ToolbarItem.ButtonIsPressed = false;
			this.actMultiSelectRedo.ToolbarItem.Caption = null;
			this.actMultiSelectRedo.ToolbarItem.Image = null;
			this.actMultiSelectRedo.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actMultiSelectRedo_Execute);
			// 
			// ctxMultiSelectExists
			// 
			this.ctxMultiSelectExists.Description = "Satisfied if a multi-select list exists for the active view.";
			this.ctxMultiSelectExists.ProviderName = "Editor\\Selection\\MultiSelect Exists";
			this.ctxMultiSelectExists.Register = true;
			this.ctxMultiSelectExists.ContextSatisfied += new DevExpress.CodeRush.Core.ContextSatisfiedEventHandler(this.ctxMultiSelectExists_ContextSatisfied);
			// 
			// ctxMultiSelectRedoAvailable
			// 
			this.ctxMultiSelectRedoAvailable.Description = "Satisfied if a multi-select redo operation is available for the active view.";
			this.ctxMultiSelectRedoAvailable.ProviderName = "Editor\\Selection\\MultiSelect Redo Available";
			this.ctxMultiSelectRedoAvailable.Register = true;
			this.ctxMultiSelectRedoAvailable.ContextSatisfied += new DevExpress.CodeRush.Core.ContextSatisfiedEventHandler(this.ctxMultiSelectRedoAvailable_ContextSatisfied);
			// 
			// actMultiSelectIntegratedPaste
			// 
			this.actMultiSelectIntegratedPaste.ActionName = "MultiSelectIntegratedPaste";
			this.actMultiSelectIntegratedPaste.ButtonText = "Integrated Paste";
			this.actMultiSelectIntegratedPaste.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.EditorContext;
			this.actMultiSelectIntegratedPaste.Description = "Pastes the members of the MultiSelect list that\'s on the clipboard into their app" +
    "ropriate locations in the active type.";
			this.actMultiSelectIntegratedPaste.Image = ((System.Drawing.Bitmap)(resources.GetObject("actMultiSelectIntegratedPaste.Image")));
			this.actMultiSelectIntegratedPaste.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
			this.actMultiSelectIntegratedPaste.ToolbarItem.ButtonIsPressed = false;
			this.actMultiSelectIntegratedPaste.ToolbarItem.Caption = null;
			this.actMultiSelectIntegratedPaste.ToolbarItem.Image = null;
			this.actMultiSelectIntegratedPaste.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.actMultiSelectIntegratedPaste_Execute);
			// 
			// PlugIn1
			// 
			this.CommandExecuting += new DevExpress.CodeRush.Core.CommandExecutingEventHandler(this.PlugIn1_CommandExecuting);
			this.EditorContextMenuShowing += new DevExpress.CodeRush.Core.EditorContextMenuShowingEventHandler(this.PlugIn1_EditorContextMenuShowing);
			((System.ComponentModel.ISupportInitialize)(this.actMultiSelectAdd)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.actMultiSelectClear)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.actMultiSelectUndo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.actMultiSelectRedo)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ctxMultiSelectExists)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ctxMultiSelectRedoAvailable)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.actMultiSelectIntegratedPaste)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.CodeRush.Core.Action actMultiSelectAdd;
		private DevExpress.CodeRush.Core.Action actMultiSelectClear;
		private DevExpress.CodeRush.Core.Action actMultiSelectUndo;
		private DevExpress.CodeRush.Core.Action actMultiSelectRedo;
		private DevExpress.CodeRush.Extensions.ContextProvider ctxMultiSelectExists;
		private DevExpress.CodeRush.Extensions.ContextProvider ctxMultiSelectRedoAvailable;
		private DevExpress.CodeRush.Core.Action actMultiSelectIntegratedPaste;
	}
}
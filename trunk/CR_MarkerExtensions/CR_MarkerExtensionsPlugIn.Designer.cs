namespace CR_MarkerExtensions
{
  partial class CR_MarkerExtensionsPlugIn
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public CR_MarkerExtensionsPlugIn()
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
    protected override void Dispose( bool disposing )
    {
      if ( disposing && (components != null) )
      {
        components.Dispose();
      }
      base.Dispose( disposing );
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.MarkerFirstAction = new DevExpress.CodeRush.Core.Action(this.components);
      this.MarkerPrevAction = new DevExpress.CodeRush.Core.Action(this.components);
      this.MarkerNextAction = new DevExpress.CodeRush.Core.Action(this.components);
      this.MarkerLastAction = new DevExpress.CodeRush.Core.Action(this.components);
      this.locatorBeacon = new DevExpress.CodeRush.PlugInCore.LocatorBeacon(this.components);
      this.MarkerCollectFirstAction = new DevExpress.CodeRush.Core.Action(this.components);
      this.MarkerCollectLastAction = new DevExpress.CodeRush.Core.Action(this.components);
      this.MarkerCollectPrevAction = new DevExpress.CodeRush.Core.Action(this.components);
      this.MarkerCollectNextAction = new DevExpress.CodeRush.Core.Action(this.components);
      this.MarkerStackTopAction = new DevExpress.CodeRush.Core.Action(this.components);
      this.MarkerCollectAtCaretAction = new DevExpress.CodeRush.Core.Action(this.components);
      this.MarkerStackBottomAction = new DevExpress.CodeRush.Core.Action(this.components);
      ((System.ComponentModel.ISupportInitialize)(this.MarkerFirstAction)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerPrevAction)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerNextAction)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerLastAction)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.locatorBeacon)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerCollectFirstAction)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerCollectLastAction)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerCollectPrevAction)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerCollectNextAction)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerStackTopAction)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerCollectAtCaretAction)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerStackBottomAction)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      // 
      // MarkerFirstAction
      // 
      this.MarkerFirstAction.ActionName = "MarkerFirst";
      this.MarkerFirstAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.MarkerFirstAction.Description = "Navigates to the \"first\" marker in the current document but doesn\'t collect it.";
      this.MarkerFirstAction.Image = null;
      this.MarkerFirstAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.MarkerFirstAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.MarkerAction_Execute);
      // 
      // MarkerPrevAction
      // 
      this.MarkerPrevAction.ActionName = "MarkerPrev";
      this.MarkerPrevAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.MarkerPrevAction.Description = "Navigates \"up\" to the previous marker in the current document but doesn\'t collect" +
    "ing it.";
      this.MarkerPrevAction.Image = null;
      this.MarkerPrevAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.MarkerPrevAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.MarkerAction_Execute);
      // 
      // MarkerNextAction
      // 
      this.MarkerNextAction.ActionName = "MarkerNext";
      this.MarkerNextAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.MarkerNextAction.Description = "Navigates \"down\" to the next marker in the current document but doesn\'t collect i" +
    "t.";
      this.MarkerNextAction.Image = null;
      this.MarkerNextAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.MarkerNextAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.MarkerAction_Execute);
      // 
      // MarkerLastAction
      // 
      this.MarkerLastAction.ActionName = "MarkerLast";
      this.MarkerLastAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.MarkerLastAction.Description = "Navigates to the \"last\" marker in the current document but doesn\'t collect it.";
      this.MarkerLastAction.Image = null;
      this.MarkerLastAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.MarkerLastAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.MarkerAction_Execute);
      // 
      // locatorBeacon
      // 
      this.locatorBeacon.Color = System.Drawing.Color.SlateBlue;
      this.locatorBeacon.Duration = 200;
      // 
      // MarkerCollectFirstAction
      // 
      this.MarkerCollectFirstAction.ActionName = "MarkerCollectFirst";
      this.MarkerCollectFirstAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.MarkerCollectFirstAction.Description = "Navigates to the \"first\" marker in the current document and collects it.";
      this.MarkerCollectFirstAction.Image = null;
      this.MarkerCollectFirstAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.MarkerCollectFirstAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.MarkerAction_Execute);
      // 
      // MarkerCollectLastAction
      // 
      this.MarkerCollectLastAction.ActionName = "MarkerCollectLast";
      this.MarkerCollectLastAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.MarkerCollectLastAction.Description = "Navigates to the \"last\" marker in the current document and collects it.";
      this.MarkerCollectLastAction.Image = null;
      this.MarkerCollectLastAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.MarkerCollectLastAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.MarkerAction_Execute);
      // 
      // MarkerCollectPrevAction
      // 
      this.MarkerCollectPrevAction.ActionName = "MarkerCollectPrev";
      this.MarkerCollectPrevAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.MarkerCollectPrevAction.Description = "Navigates \"up\" to the previous marker in the current document and collects it.";
      this.MarkerCollectPrevAction.Image = null;
      this.MarkerCollectPrevAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.MarkerCollectPrevAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.MarkerAction_Execute);
      // 
      // MarkerCollectNextAction
      // 
      this.MarkerCollectNextAction.ActionName = "MarkerCollectNext";
      this.MarkerCollectNextAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.MarkerCollectNextAction.Description = "Navigates \"down\" to the next marker in the current document and collects it.";
      this.MarkerCollectNextAction.Image = null;
      this.MarkerCollectNextAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.MarkerCollectNextAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.MarkerAction_Execute);
      // 
      // MarkerStackTopAction
      // 
      this.MarkerStackTopAction.ActionName = "MarkerStackTop";
      this.MarkerStackTopAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.MarkerStackTopAction.Description = "Navigates to the top-most marker in the marker stack but doesn\'t collect it.";
      this.MarkerStackTopAction.Image = null;
      this.MarkerStackTopAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.MarkerStackTopAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.MarkerAction_Execute);
      // 
      // MarkerCollectAtCaretAction
      // 
      this.MarkerCollectAtCaretAction.ActionName = "MarkerCollectAtCaret";
      this.MarkerCollectAtCaretAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.MarkerCollectAtCaretAction.Description = "Collects the marker at the current caret location.";
      this.MarkerCollectAtCaretAction.Image = null;
      this.MarkerCollectAtCaretAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.MarkerCollectAtCaretAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.MarkerAction_Execute);
      // 
      // MarkerStackBottomAction
      // 
      this.MarkerStackBottomAction.ActionName = "MarkerStackBottom";
      this.MarkerStackBottomAction.CommonMenu = DevExpress.CodeRush.Menus.VsCommonBar.None;
      this.MarkerStackBottomAction.Description = "Navigates to the bottom-most marker in the marker stack but doesn\'t collect it.";
      this.MarkerStackBottomAction.Image = null;
      this.MarkerStackBottomAction.ImageBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(254)))), ((int)(((byte)(0)))));
      this.MarkerStackBottomAction.Execute += new DevExpress.CodeRush.Core.CommandExecuteEventHandler(this.MarkerAction_Execute);
      // 
      // CR_MarkerExtensionsPlugIn
      // 
      this.OptionsChanged += new DevExpress.CodeRush.Core.OptionsChangedEventHandler(this.CR_MarkerExtensionsPlugIn_OptionsChanged);
      ((System.ComponentModel.ISupportInitialize)(this.MarkerFirstAction)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerPrevAction)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerNextAction)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerLastAction)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.locatorBeacon)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerCollectFirstAction)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerCollectLastAction)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerCollectPrevAction)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerCollectNextAction)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerStackTopAction)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerCollectAtCaretAction)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.MarkerStackBottomAction)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.CodeRush.Core.Action MarkerFirstAction;
    private DevExpress.CodeRush.Core.Action MarkerPrevAction;
    private DevExpress.CodeRush.Core.Action MarkerNextAction;
    private DevExpress.CodeRush.Core.Action MarkerLastAction;
    private DevExpress.CodeRush.PlugInCore.LocatorBeacon locatorBeacon;
    private DevExpress.CodeRush.Core.Action MarkerCollectFirstAction;
    private DevExpress.CodeRush.Core.Action MarkerCollectLastAction;
    private DevExpress.CodeRush.Core.Action MarkerCollectPrevAction;
    private DevExpress.CodeRush.Core.Action MarkerCollectNextAction;
    private DevExpress.CodeRush.Core.Action MarkerStackTopAction;
    private DevExpress.CodeRush.Core.Action MarkerCollectAtCaretAction;
    private DevExpress.CodeRush.Core.Action MarkerStackBottomAction;
  }
}
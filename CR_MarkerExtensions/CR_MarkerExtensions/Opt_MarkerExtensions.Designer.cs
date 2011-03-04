using System;
using DevExpress.CodeRush.Core;

namespace CR_MarkerExtensions
{
  partial class Opt_MarkerExtensions
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public Opt_MarkerExtensions()
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
      this.showLocatorBeaconLabel = new System.Windows.Forms.Label();
      this.showLocatorBeaconCheckBox = new System.Windows.Forms.CheckBox();
      this.beaconColorLabel = new System.Windows.Forms.Label();
      this.beaconDurationLabel = new System.Windows.Forms.Label();
      this.beaconDurationTrackBar = new System.Windows.Forms.TrackBar();
      this.rollOverOnNextPrevCheckBox = new System.Windows.Forms.CheckBox();
      this.dynamicBeaconDurationLabel = new System.Windows.Forms.Label();
      this.beaconColorDialog = new System.Windows.Forms.ColorDialog();
      this.testBeaconButton = new System.Windows.Forms.Button();
      this.skipSelectionMarkersCheckBox = new System.Windows.Forms.CheckBox();
      this.beaconColorSelectButton = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.beaconDurationTrackBar)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
      this.SuspendLayout();
      // 
      // showLocatorBeaconLabel
      // 
      this.showLocatorBeaconLabel.AutoSize = true;
      this.showLocatorBeaconLabel.Location = new System.Drawing.Point(12, 60);
      this.showLocatorBeaconLabel.Name = "showLocatorBeaconLabel";
      this.showLocatorBeaconLabel.Size = new System.Drawing.Size(86, 13);
      this.showLocatorBeaconLabel.TabIndex = 2;
      this.showLocatorBeaconLabel.Text = "Locator Beacons";
      // 
      // showLocatorBeaconCheckBox
      // 
      this.showLocatorBeaconCheckBox.AutoSize = true;
      this.showLocatorBeaconCheckBox.Location = new System.Drawing.Point(24, 84);
      this.showLocatorBeaconCheckBox.Name = "showLocatorBeaconCheckBox";
      this.showLocatorBeaconCheckBox.Size = new System.Drawing.Size(64, 17);
      this.showLocatorBeaconCheckBox.TabIndex = 3;
      this.showLocatorBeaconCheckBox.Text = "Enabled";
      this.showLocatorBeaconCheckBox.UseVisualStyleBackColor = true;
      // 
      // beaconColorLabel
      // 
      this.beaconColorLabel.AutoSize = true;
      this.beaconColorLabel.Location = new System.Drawing.Point(20, 108);
      this.beaconColorLabel.Name = "beaconColorLabel";
      this.beaconColorLabel.Size = new System.Drawing.Size(74, 13);
      this.beaconColorLabel.TabIndex = 4;
      this.beaconColorLabel.Text = "Beacon Color:";
      // 
      // beaconDurationLabel
      // 
      this.beaconDurationLabel.AutoSize = true;
      this.beaconDurationLabel.Location = new System.Drawing.Point(20, 136);
      this.beaconDurationLabel.Name = "beaconDurationLabel";
      this.beaconDurationLabel.Size = new System.Drawing.Size(52, 13);
      this.beaconDurationLabel.TabIndex = 6;
      this.beaconDurationLabel.Text = "Duration:";
      // 
      // beaconDurationTrackBar
      // 
      this.beaconDurationTrackBar.LargeChange = 25;
      this.beaconDurationTrackBar.Location = new System.Drawing.Point(100, 132);
      this.beaconDurationTrackBar.Maximum = 3000;
      this.beaconDurationTrackBar.Minimum = 100;
      this.beaconDurationTrackBar.Name = "beaconDurationTrackBar";
      this.beaconDurationTrackBar.Size = new System.Drawing.Size(392, 45);
      this.beaconDurationTrackBar.SmallChange = 5;
      this.beaconDurationTrackBar.TabIndex = 7;
      this.beaconDurationTrackBar.TickFrequency = 100;
      this.beaconDurationTrackBar.Value = 100;
      this.beaconDurationTrackBar.ValueChanged += new System.EventHandler(this.beaconDurationTrackBar_ValueChanged);
      // 
      // rollOverOnNextPrevCheckBox
      // 
      this.rollOverOnNextPrevCheckBox.AutoSize = true;
      this.rollOverOnNextPrevCheckBox.Location = new System.Drawing.Point(16, 16);
      this.rollOverOnNextPrevCheckBox.Name = "rollOverOnNextPrevCheckBox";
      this.rollOverOnNextPrevCheckBox.Size = new System.Drawing.Size(337, 17);
      this.rollOverOnNextPrevCheckBox.TabIndex = 0;
      this.rollOverOnNextPrevCheckBox.Text = "\"Roll over\" first/last marker when navigating to prev/next marker";
      this.rollOverOnNextPrevCheckBox.UseVisualStyleBackColor = true;
      // 
      // dynamicBeaconDurationLabel
      // 
      this.dynamicBeaconDurationLabel.Location = new System.Drawing.Point(108, 172);
      this.dynamicBeaconDurationLabel.Name = "dynamicBeaconDurationLabel";
      this.dynamicBeaconDurationLabel.Size = new System.Drawing.Size(100, 23);
      this.dynamicBeaconDurationLabel.TabIndex = 8;
      this.dynamicBeaconDurationLabel.Text = "(0 ms)";
      // 
      // beaconColorDialog
      // 
      this.beaconColorDialog.AnyColor = true;
      this.beaconColorDialog.FullOpen = true;
      // 
      // testBeaconButton
      // 
      this.testBeaconButton.BackColor = System.Drawing.Color.White;
      this.testBeaconButton.FlatAppearance.BorderColor = System.Drawing.SystemColors.ControlDark;
      this.testBeaconButton.FlatAppearance.BorderSize = 2;
      this.testBeaconButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
      this.testBeaconButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
      this.testBeaconButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.testBeaconButton.ForeColor = System.Drawing.SystemColors.ControlText;
      this.testBeaconButton.Location = new System.Drawing.Point(100, 200);
      this.testBeaconButton.Name = "testBeaconButton";
      this.testBeaconButton.Size = new System.Drawing.Size(392, 100);
      this.testBeaconButton.TabIndex = 9;
      this.testBeaconButton.Text = "(click to test)";
      this.testBeaconButton.UseVisualStyleBackColor = false;
      this.testBeaconButton.Click += new System.EventHandler(this.testBeaconButton_Click);
      // 
      // skipSelectionMarkersCheckBox
      // 
      this.skipSelectionMarkersCheckBox.AutoSize = true;
      this.skipSelectionMarkersCheckBox.Location = new System.Drawing.Point(16, 36);
      this.skipSelectionMarkersCheckBox.Name = "skipSelectionMarkersCheckBox";
      this.skipSelectionMarkersCheckBox.Size = new System.Drawing.Size(139, 17);
      this.skipSelectionMarkersCheckBox.TabIndex = 1;
      this.skipSelectionMarkersCheckBox.Text = "Skip \"selection\" markers";
      this.skipSelectionMarkersCheckBox.UseVisualStyleBackColor = true;
      // 
      // beaconColorSelectButton
      // 
      this.beaconColorSelectButton.BackColor = System.Drawing.Color.SlateBlue;
      this.beaconColorSelectButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
      this.beaconColorSelectButton.Location = new System.Drawing.Point(100, 106);
      this.beaconColorSelectButton.Name = "beaconColorSelectButton";
      this.beaconColorSelectButton.Size = new System.Drawing.Size(172, 18);
      this.beaconColorSelectButton.TabIndex = 5;
      this.beaconColorSelectButton.UseVisualStyleBackColor = false;
      this.beaconColorSelectButton.Click += new System.EventHandler(this.beaconColorSelectLabel_Click);
      // 
      // Opt_MarkerExtensions
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.Controls.Add(this.beaconColorSelectButton);
      this.Controls.Add(this.skipSelectionMarkersCheckBox);
      this.Controls.Add(this.testBeaconButton);
      this.Controls.Add(this.dynamicBeaconDurationLabel);
      this.Controls.Add(this.rollOverOnNextPrevCheckBox);
      this.Controls.Add(this.beaconDurationTrackBar);
      this.Controls.Add(this.beaconDurationLabel);
      this.Controls.Add(this.beaconColorLabel);
      this.Controls.Add(this.showLocatorBeaconCheckBox);
      this.Controls.Add(this.showLocatorBeaconLabel);
      this.Name = "Opt_MarkerExtensions";
      this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.Opt_MarkerExtensions_CommitChanges);
      this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.Opt_MarkerExtensions_PreparePage);
      this.RestoreDefaults += new DevExpress.CodeRush.Core.OptionsPage.RestoreDefaultsEventHandler(this.Opt_MarkerExtensions_RestoreDefaults);
      ((System.ComponentModel.ISupportInitialize)(this.beaconDurationTrackBar)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    ///
    /// Gets a DecoupledStorage instance for this options page.
    ///
    public static DecoupledStorage Storage
    {
      get
      {
        return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage( GetCategory(), GetPageName() );
      }
    }
    ///
    /// Returns the category of this options page.
    ///
    public override string Category
    {
      get
      {
        return Opt_MarkerExtensions.GetCategory();
      }
    }
    ///
    /// Returns the page name of this options page.
    ///
    public override string PageName
    {
      get
      {
        return Opt_MarkerExtensions.GetPageName();
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
      DevExpress.CodeRush.Core.CodeRush.Command.Execute( "Options", FullPath );
    }

    private System.Windows.Forms.Label showLocatorBeaconLabel;
    private System.Windows.Forms.CheckBox showLocatorBeaconCheckBox;
    private System.Windows.Forms.Label beaconColorLabel;
    private System.Windows.Forms.Label beaconDurationLabel;
    private System.Windows.Forms.TrackBar beaconDurationTrackBar;
    private System.Windows.Forms.CheckBox rollOverOnNextPrevCheckBox;
    private System.Windows.Forms.Label dynamicBeaconDurationLabel;
    private System.Windows.Forms.ColorDialog beaconColorDialog;
    private System.Windows.Forms.Button testBeaconButton;
    private System.Windows.Forms.CheckBox skipSelectionMarkersCheckBox;
    private System.Windows.Forms.Button beaconColorSelectButton;
  }
}
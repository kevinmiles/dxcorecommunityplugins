using System;
using DevExpress.CodeRush.Core;

namespace CR_BlockPainterPlus
{
    partial class BlockPainterOptions
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public BlockPainterOptions()
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
            this.arrowColorPicture = new System.Windows.Forms.PictureBox();
            this.fontColorPicture = new System.Windows.Forms.PictureBox();
            this.fontColorLabel = new System.Windows.Forms.Label();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.arrowTrackBar = new System.Windows.Forms.TrackBar();
            this.arrowGroup = new System.Windows.Forms.GroupBox();
            this.arrowOpacityLabel = new System.Windows.Forms.Label();
            this.arrowColorLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fontTrackBar = new System.Windows.Forms.TrackBar();
            this.fontOpacityLabel = new System.Windows.Forms.Label();
            this.minimumLinesCheckBox = new System.Windows.Forms.CheckBox();
            this.lineCountSpinner = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.arrowColorPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontColorPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arrowTrackBar)).BeginInit();
            this.arrowGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fontTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineCountSpinner)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // arrowColorPicture
            // 
            this.arrowColorPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.arrowColorPicture.Location = new System.Drawing.Point(89, 19);
            this.arrowColorPicture.Name = "arrowColorPicture";
            this.arrowColorPicture.Size = new System.Drawing.Size(153, 16);
            this.arrowColorPicture.TabIndex = 1;
            this.arrowColorPicture.TabStop = false;
            this.arrowColorPicture.DoubleClick += new System.EventHandler(this.arrowColorPicture_DoubleClick);
            // 
            // fontColorPicture
            // 
            this.fontColorPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fontColorPicture.Location = new System.Drawing.Point(95, 19);
            this.fontColorPicture.Name = "fontColorPicture";
            this.fontColorPicture.Size = new System.Drawing.Size(153, 16);
            this.fontColorPicture.TabIndex = 3;
            this.fontColorPicture.TabStop = false;
            this.fontColorPicture.DoubleClick += new System.EventHandler(this.fontColorPicture_DoubleClick);
            // 
            // fontColorLabel
            // 
            this.fontColorLabel.AutoSize = true;
            this.fontColorLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontColorLabel.Location = new System.Drawing.Point(36, 19);
            this.fontColorLabel.Name = "fontColorLabel";
            this.fontColorLabel.Size = new System.Drawing.Size(41, 16);
            this.fontColorLabel.TabIndex = 2;
            this.fontColorLabel.Text = "Color";
            // 
            // arrowTrackBar
            // 
            this.arrowTrackBar.Location = new System.Drawing.Point(89, 44);
            this.arrowTrackBar.Maximum = 255;
            this.arrowTrackBar.Minimum = 50;
            this.arrowTrackBar.Name = "arrowTrackBar";
            this.arrowTrackBar.Size = new System.Drawing.Size(153, 56);
            this.arrowTrackBar.SmallChange = 15;
            this.arrowTrackBar.TabIndex = 4;
            this.arrowTrackBar.TickFrequency = 15;
            this.arrowTrackBar.Value = 125;
            // 
            // arrowGroup
            // 
            this.arrowGroup.Controls.Add(this.arrowOpacityLabel);
            this.arrowGroup.Controls.Add(this.arrowColorLabel);
            this.arrowGroup.Controls.Add(this.arrowTrackBar);
            this.arrowGroup.Controls.Add(this.arrowColorPicture);
            this.arrowGroup.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowGroup.Location = new System.Drawing.Point(8, 13);
            this.arrowGroup.Name = "arrowGroup";
            this.arrowGroup.Size = new System.Drawing.Size(254, 111);
            this.arrowGroup.TabIndex = 5;
            this.arrowGroup.TabStop = false;
            this.arrowGroup.Text = "Arrow Stuff";
            // 
            // arrowOpacityLabel
            // 
            this.arrowOpacityLabel.AutoSize = true;
            this.arrowOpacityLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowOpacityLabel.Location = new System.Drawing.Point(23, 59);
            this.arrowOpacityLabel.Name = "arrowOpacityLabel";
            this.arrowOpacityLabel.Size = new System.Drawing.Size(59, 16);
            this.arrowOpacityLabel.TabIndex = 6;
            this.arrowOpacityLabel.Text = "Opacity";
            // 
            // arrowColorLabel
            // 
            this.arrowColorLabel.AutoSize = true;
            this.arrowColorLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.arrowColorLabel.Location = new System.Drawing.Point(41, 19);
            this.arrowColorLabel.Name = "arrowColorLabel";
            this.arrowColorLabel.Size = new System.Drawing.Size(41, 16);
            this.arrowColorLabel.TabIndex = 5;
            this.arrowColorLabel.Text = "Color";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fontTrackBar);
            this.groupBox1.Controls.Add(this.fontOpacityLabel);
            this.groupBox1.Controls.Add(this.fontColorPicture);
            this.groupBox1.Controls.Add(this.fontColorLabel);
            this.groupBox1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(269, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(254, 111);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Font Stuff";
            // 
            // fontTrackBar
            // 
            this.fontTrackBar.Location = new System.Drawing.Point(95, 44);
            this.fontTrackBar.Maximum = 255;
            this.fontTrackBar.Minimum = 50;
            this.fontTrackBar.Name = "fontTrackBar";
            this.fontTrackBar.Size = new System.Drawing.Size(153, 56);
            this.fontTrackBar.SmallChange = 15;
            this.fontTrackBar.TabIndex = 8;
            this.fontTrackBar.TickFrequency = 15;
            this.fontTrackBar.Value = 125;
            // 
            // fontOpacityLabel
            // 
            this.fontOpacityLabel.AutoSize = true;
            this.fontOpacityLabel.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fontOpacityLabel.Location = new System.Drawing.Point(18, 59);
            this.fontOpacityLabel.Name = "fontOpacityLabel";
            this.fontOpacityLabel.Size = new System.Drawing.Size(59, 16);
            this.fontOpacityLabel.TabIndex = 7;
            this.fontOpacityLabel.Text = "Opacity";
            // 
            // minimumLinesCheckBox
            // 
            this.minimumLinesCheckBox.AutoSize = true;
            this.minimumLinesCheckBox.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimumLinesCheckBox.Location = new System.Drawing.Point(8, 159);
            this.minimumLinesCheckBox.Name = "minimumLinesCheckBox";
            this.minimumLinesCheckBox.Size = new System.Drawing.Size(218, 20);
            this.minimumLinesCheckBox.TabIndex = 7;
            this.minimumLinesCheckBox.Text = "only paint blocks longer than";
            this.minimumLinesCheckBox.UseVisualStyleBackColor = true;
            // 
            // lineCountSpinner
            // 
            this.lineCountSpinner.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lineCountSpinner.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.lineCountSpinner.Location = new System.Drawing.Point(232, 159);
            this.lineCountSpinner.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lineCountSpinner.Name = "lineCountSpinner";
            this.lineCountSpinner.Size = new System.Drawing.Size(42, 23);
            this.lineCountSpinner.TabIndex = 8;
            this.lineCountSpinner.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Verdana", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(280, 161);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 16);
            this.label1.TabIndex = 9;
            this.label1.Text = "lines.";
            // 
            // BlockPainterOptions
            // 
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lineCountSpinner);
            this.Controls.Add(this.minimumLinesCheckBox);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.arrowGroup);
            this.DoubleBuffered = true;
            this.Name = "BlockPainterOptions";
            this.Title = "Block Painter";
            this.Load += new System.EventHandler(this.BlockPainterOptions_Load);
            this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.BlockPainterOptions_CommitChanges);
            ((System.ComponentModel.ISupportInitialize)(this.arrowColorPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fontColorPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arrowTrackBar)).EndInit();
            this.arrowGroup.ResumeLayout(false);
            this.arrowGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fontTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lineCountSpinner)).EndInit();
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
                return BlockPainterOptions.GetCategory();
            }
        }
        ///
        /// Returns the page name of this options page.
        ///
        public override string PageName
        {
            get
            {
                return BlockPainterOptions.GetPageName();
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
        private System.Windows.Forms.PictureBox arrowColorPicture;
        private System.Windows.Forms.PictureBox fontColorPicture;
        private System.Windows.Forms.Label fontColorLabel;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.TrackBar arrowTrackBar;
        private System.Windows.Forms.GroupBox arrowGroup;
        private System.Windows.Forms.Label arrowOpacityLabel;
        private System.Windows.Forms.Label arrowColorLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TrackBar fontTrackBar;
        private System.Windows.Forms.Label fontOpacityLabel;
        private System.Windows.Forms.CheckBox minimumLinesCheckBox;
        private System.Windows.Forms.NumericUpDown lineCountSpinner;
        private System.Windows.Forms.Label label1;
    }
}
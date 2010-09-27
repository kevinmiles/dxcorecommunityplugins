//using System;
//using DevExpress.CodeRush.Core;

//namespace DX_StudioSizer
//{
//    partial class Options1
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        public Options1()
//        {
//            /// <summary>
//            /// Required for Windows.Forms Class Composition Designer support
//            /// </summary>
//            InitializeComponent();
//        }

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            this.label1 = new System.Windows.Forms.Label();
//            this.numWidth = new System.Windows.Forms.NumericUpDown();
//            this.numHeight = new System.Windows.Forms.NumericUpDown();
//            this.cmdAdd = new System.Windows.Forms.Button();
//            this.label2 = new System.Windows.Forms.Label();
//            this.label3 = new System.Windows.Forms.Label();
//            this.cmdRemove = new System.Windows.Forms.Button();
//            this.lstResolutions = new System.Windows.Forms.ListBox();
//            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).BeginInit();
//            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
//            this.SuspendLayout();
//            // 
//            // label1
//            // 
//            this.label1.AutoSize = true;
//            this.label1.Location = new System.Drawing.Point(14, 28);
//            this.label1.Name = "label1";
//            this.label1.Size = new System.Drawing.Size(141, 13);
//            this.label1.TabIndex = 1;
//            this.label1.Text = "Resolutions (Width x Height)";
//            // 
//            // numWidth
//            // 
//            this.numWidth.Increment = new decimal(new int[] {
//            100,
//            0,
//            0,
//            0});
//            this.numWidth.Location = new System.Drawing.Point(250, 45);
//            this.numWidth.Maximum = new decimal(new int[] {
//            1600,
//            0,
//            0,
//            0});
//            this.numWidth.Name = "numWidth";
//            this.numWidth.Size = new System.Drawing.Size(120, 20);
//            this.numWidth.TabIndex = 2;
//            this.numWidth.Value = new decimal(new int[] {
//            1024,
//            0,
//            0,
//            0});
//            // 
//            // numHeight
//            // 
//            this.numHeight.Increment = new decimal(new int[] {
//            100,
//            0,
//            0,
//            0});
//            this.numHeight.Location = new System.Drawing.Point(250, 71);
//            this.numHeight.Maximum = new decimal(new int[] {
//            1600,
//            0,
//            0,
//            0});
//            this.numHeight.Name = "numHeight";
//            this.numHeight.Size = new System.Drawing.Size(120, 20);
//            this.numHeight.TabIndex = 2;
//            this.numHeight.Value = new decimal(new int[] {
//            768,
//            0,
//            0,
//            0});
//            // 
//            // cmdAdd
//            // 
//            this.cmdAdd.Location = new System.Drawing.Point(250, 97);
//            this.cmdAdd.Name = "cmdAdd";
//            this.cmdAdd.Size = new System.Drawing.Size(75, 23);
//            this.cmdAdd.TabIndex = 3;
//            this.cmdAdd.Text = "Add";
//            this.cmdAdd.UseVisualStyleBackColor = true;
//            this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
//            // 
//            // label2
//            // 
//            this.label2.AutoSize = true;
//            this.label2.Location = new System.Drawing.Point(209, 47);
//            this.label2.Name = "label2";
//            this.label2.Size = new System.Drawing.Size(35, 13);
//            this.label2.TabIndex = 1;
//            this.label2.Text = "Width";
//            // 
//            // label3
//            // 
//            this.label3.AutoSize = true;
//            this.label3.Location = new System.Drawing.Point(206, 73);
//            this.label3.Name = "label3";
//            this.label3.Size = new System.Drawing.Size(38, 13);
//            this.label3.TabIndex = 1;
//            this.label3.Text = "Height";
//            // 
//            // cmdRemove
//            // 
//            this.cmdRemove.Location = new System.Drawing.Point(64, 193);
//            this.cmdRemove.Name = "cmdRemove";
//            this.cmdRemove.Size = new System.Drawing.Size(75, 23);
//            this.cmdRemove.TabIndex = 3;
//            this.cmdRemove.Text = "Remove";
//            this.cmdRemove.UseVisualStyleBackColor = true;
//            this.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);
//            // 
//            // lstResolutions
//            // 
//            this.lstResolutions.FormattingEnabled = true;
//            this.lstResolutions.Items.AddRange(new object[] {
//            "1600x1200",
//            "1600x1050",
//            "1280x1024",
//            "1280x720",
//            "1024x768",
//            "800x600"});
//            this.lstResolutions.Location = new System.Drawing.Point(17, 44);
//            this.lstResolutions.Name = "lstResolutions";
//            this.lstResolutions.Size = new System.Drawing.Size(171, 147);
//            this.lstResolutions.TabIndex = 4;
//            // 
//            // Options1
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
//            this.Controls.Add(this.lstResolutions);
//            this.Controls.Add(this.cmdRemove);
//            this.Controls.Add(this.cmdAdd);
//            this.Controls.Add(this.numHeight);
//            this.Controls.Add(this.label3);
//            this.Controls.Add(this.label2);
//            this.Controls.Add(this.numWidth);
//            this.Controls.Add(this.label1);
//            this.Name = "Options1";
//            this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.Options1_CommitChanges);
//            this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.Options1_PreparePage);
//            this.RestoreDefaults += new DevExpress.CodeRush.Core.OptionsPage.RestoreDefaultsEventHandler(this.Options1_RestoreDefaults);
//            ((System.ComponentModel.ISupportInitialize)(this.numWidth)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this.numHeight)).EndInit();
//            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }

//        #endregion

//        ///
//        /// Gets a DecoupledStorage instance for this options page.
//        ///
//        public static DecoupledStorage Storage
//        {
//            get
//            {
//                return DevExpress.CodeRush.Core.CodeRush.Options.GetStorage(GetCategory(), GetPageName());
//            }
//        }
//        ///
//        /// Returns the category of this options page.
//        ///
//        public override string Category
//        {
//            get
//            {
//                return Options1.GetCategory();
//            }
//        }
//        ///
//        /// Returns the page name of this options page.
//        ///
//        public override string PageName
//        {
//            get
//            {
//                return Options1.GetPageName();
//            }
//        }
//        ///
//        /// Returns the full path (Category + PageName) of this options page.
//        ///
//        public static string FullPath
//        {
//            get
//            {
//                return GetCategory() + "\\" + GetPageName();
//            }
//        }

//        ///
//        /// Displays the DXCore options dialog and selects this page.
//        ///
//        public new static void Show()
//        {
//            DevExpress.CodeRush.Core.CodeRush.Command.Execute("Options", FullPath);
//        }
//        private System.Windows.Forms.Label label1;
//        private System.Windows.Forms.NumericUpDown numWidth;
//        private System.Windows.Forms.NumericUpDown numHeight;
//        private System.Windows.Forms.Button cmdAdd;
//        private System.Windows.Forms.Label label2;
//        private System.Windows.Forms.Label label3;
//        private System.Windows.Forms.Button cmdRemove;
//        private System.Windows.Forms.ListBox lstResolutions;
//    }
//}
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;

namespace CR_DrawLinesBetweenMethods
{
	/// <summary>
	/// Summary description for OptDrawLinesBetweenMethods.
	/// </summary>
	public class OptDrawLinesBetweenMethods: OptionsPage
	{
		#region private fields...
		private System.Windows.Forms.CheckBox _fullWidthChk;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox _lineStyleLst;
		private System.Windows.Forms.ComboBox _lineWidthLst;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox _lineColorLst;
		private System.Windows.Forms.CheckBox _drawLineAtEndChk;
		private System.Windows.Forms.CheckBox _drawShadowChk;
		private System.ComponentModel.Container components = null;
		#endregion

		// constructor...
		#region OptDrawLinesBetweenMethods
		public OptDrawLinesBetweenMethods(): base()
		{
			/// <summary>
			/// Required for Windows.Forms Class Composition Designer support
			/// </summary>
			InitializeComponent();
		}
		#endregion

		#region CodeRush generated code
		protected override void Initialize()
		{
			base.Initialize();

			//
			// TODO: Add your initialization code here.
			//
		}

		public static string GetCategory()
		{
			return @"Editor\Painting";
		}

		public static string GetPageName()
		{
			return @"Draw Lines Between Methods";
		}

		public static DecoupledStorage Storage
		{
			get
			{
				return CodeRush.Options.GetStorage(GetCategory(), GetPageName());
			}
		}

		public override string Category
		{
			get
			{
				return OptDrawLinesBetweenMethods.GetCategory();
			}
		}

		public override string PageName
		{
			get
			{
				return OptDrawLinesBetweenMethods.GetPageName();
			}
		}

		public new static void Show()
		{
			CodeRush.Command.Execute("Options", FullPath);
		}

		public static string FullPath
		{
			get
			{
				return GetCategory() + "\\" + GetPageName();
			}
		}
		#endregion

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this._fullWidthChk = new System.Windows.Forms.CheckBox();
			this._lineStyleLst = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this._lineWidthLst = new System.Windows.Forms.ComboBox();
			this.label2 = new System.Windows.Forms.Label();
			this._lineColorLst = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this._drawLineAtEndChk = new System.Windows.Forms.CheckBox();
			this._drawShadowChk = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// _fullWidthChk
			// 
			this._fullWidthChk.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this._fullWidthChk.Location = new System.Drawing.Point(8, 8);
			this._fullWidthChk.Name = "_fullWidthChk";
			this._fullWidthChk.Size = new System.Drawing.Size(184, 24);
			this._fullWidthChk.TabIndex = 0;
			this._fullWidthChk.Text = "Draw line across full width of page";
			// 
			// _lineStyleLst
			// 
			this._lineStyleLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._lineStyleLst.Items.AddRange(new object[] {
															   "Solid",
															   "Dash",
															   "Dot"});
			this._lineStyleLst.Location = new System.Drawing.Point(80, 40);
			this._lineStyleLst.Name = "_lineStyleLst";
			this._lineStyleLst.Size = new System.Drawing.Size(121, 21);
			this._lineStyleLst.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label1.Location = new System.Drawing.Point(8, 40);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(72, 14);
			this.label1.TabIndex = 2;
			this.label1.Text = "Line style:";
			// 
			// _lineWidthLst
			// 
			this._lineWidthLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._lineWidthLst.Items.AddRange(new object[] {
															   "1",
															   "2",
															   "3",
															   "4",
															   "5"});
			this._lineWidthLst.Location = new System.Drawing.Point(80, 72);
			this._lineWidthLst.Name = "_lineWidthLst";
			this._lineWidthLst.Size = new System.Drawing.Size(121, 21);
			this._lineWidthLst.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label2.Location = new System.Drawing.Point(8, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 14);
			this.label2.TabIndex = 2;
			this.label2.Text = "Line width:";
			// 
			// _lineColorLst
			// 
			this._lineColorLst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this._lineColorLst.Items.AddRange(new object[] {
															   "Gray",
															   "Black",
															   "Blue",
															   "Green"});
			this._lineColorLst.Location = new System.Drawing.Point(80, 104);
			this._lineColorLst.Name = "_lineColorLst";
			this._lineColorLst.Size = new System.Drawing.Size(121, 21);
			this._lineColorLst.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.label3.Location = new System.Drawing.Point(8, 104);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 14);
			this.label3.TabIndex = 2;
			this.label3.Text = "Line color:";
			// 
			// _drawLineAtEndChk
			// 
			this._drawLineAtEndChk.Location = new System.Drawing.Point(8, 160);
			this._drawLineAtEndChk.Name = "_drawLineAtEndChk";
			this._drawLineAtEndChk.Size = new System.Drawing.Size(280, 24);
			this._drawLineAtEndChk.TabIndex = 3;
			this._drawLineAtEndChk.Text = "Draw line at end of method";
			// 
			// _drawShadowChk
			// 
			this._drawShadowChk.Location = new System.Drawing.Point(8, 136);
			this._drawShadowChk.Name = "_drawShadowChk";
			this._drawShadowChk.Size = new System.Drawing.Size(280, 24);
			this._drawShadowChk.TabIndex = 3;
			this._drawShadowChk.Text = "Shadow";
			// 
			// OptDrawLinesBetweenMethods
			// 
			this.Controls.Add(this._drawLineAtEndChk);
			this.Controls.Add(this.label1);
			this.Controls.Add(this._lineStyleLst);
			this.Controls.Add(this._fullWidthChk);
			this.Controls.Add(this._lineWidthLst);
			this.Controls.Add(this.label2);
			this.Controls.Add(this._lineColorLst);
			this.Controls.Add(this.label3);
			this.Controls.Add(this._drawShadowChk);
			this.Name = "OptDrawLinesBetweenMethods";
			this.Size = new System.Drawing.Size(344, 272);
			this.PreparePage += new DevExpress.CodeRush.Core.OptionsPage.PreparePageEventHandler(this.OptDrawLinesBetweenMethods_PreparePage);
			this.CommitChanges += new DevExpress.CodeRush.Core.OptionsPage.CommitChangesEventHandler(this.OptDrawLinesBetweenMethods_CommitChanges);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		// protected methods...
		#region Dispose
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}
		#endregion

		void initLineColorLst()
		{
			Array knownColors = Enum.GetValues(typeof(KnownColor));

			_lineColorLst.Items.Clear();
			foreach(KnownColor knownColor in knownColors)
			{
				_lineColorLst.Items.Add( Color.FromKnownColor(knownColor).Name );
			}
		}

		private void OptDrawLinesBetweenMethods_PreparePage(object sender, OptionsPageStorageEventArgs ea)
		{
			loadSettings();
		}

		private void OptDrawLinesBetweenMethods_CommitChanges(object sender, OptionsPageStorageEventArgs ea)
		{
			saveSettings();
		}

		#region Member variables
		bool _fullWidth = false;
		DashStyle _lineDashStyle = DashStyle.Solid;
		int _lineWidth = 1;
		Color _lineColor = Color.Gray;
		bool _drawLineAtEndOfMethod = false;
		bool _drawShadow = true;
		#endregion

		void loadSettings()
		{
			try
			{
				initLineColorLst();

				using (DecoupledStorage storage = OptDrawLinesBetweenMethods.Storage)
				{
					_fullWidth = storage.ReadBoolean("DrawLinesBetweenMethods", "FullWidth", _fullWidth);
					_lineDashStyle = (DashStyle)storage.ReadEnum("DrawLinesBetweenMethods", "LineDashStyle", typeof(DashStyle), _lineDashStyle);
					_lineWidth = storage.ReadInt32("DrawLinesBetweenMethods", "LineWidth", _lineWidth);
					_lineColor = storage.ReadColor("DrawLinesBetweenMethods", "LineColor", _lineColor);
					_drawLineAtEndOfMethod = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawLineAtEndOfMethod", _drawLineAtEndOfMethod);
					_drawShadow = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawShadow", _drawShadow);
				}

				_fullWidthChk.Checked = _fullWidth;
				_lineStyleLst.Text = _lineDashStyle.ToString();
				_lineWidthLst.Text = _lineWidth.ToString();
				_lineColorLst.Text = _lineColor.Name;
				_drawLineAtEndChk.Checked = _drawLineAtEndOfMethod;
				_drawShadowChk.Checked = _drawShadow;

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
			
		}

		void saveSettings()
		{
			try
			{
				_fullWidth = _fullWidthChk.Checked;
				_lineDashStyle = (DashStyle)Enum.Parse(typeof(DashStyle), _lineStyleLst.Text);
				_lineWidth = int.Parse(_lineWidthLst.Text);
				_lineColor = Color.FromName(_lineColorLst.Text);
				_drawLineAtEndOfMethod = _drawLineAtEndChk.Checked;
				_drawShadow = _drawShadowChk.Checked;

				using (DecoupledStorage storage = OptDrawLinesBetweenMethods.Storage)
				{
					storage.WriteBoolean("DrawLinesBetweenMethods", "FullWidth", _fullWidth);
					storage.WriteEnum("DrawLinesBetweenMethods", "LineDashStyle", _lineDashStyle);
					storage.WriteInt32("DrawLinesBetweenMethods", "LineWidth", _lineWidth);
					storage.WriteColor("DrawLinesBetweenMethods", "LineColor", _lineColor);
					storage.WriteBoolean("DrawLinesBetweenMethods", "DrawLineAtEndOfMethod", _drawLineAtEndOfMethod);
					storage.WriteBoolean("DrawLinesBetweenMethods", "DrawShadow", _drawShadow);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());				
			}
		}

	}
}
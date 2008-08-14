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
	public partial class OptDrawLinesBetweenMethods : OptionsPage
	{
		public OptDrawLinesBetweenMethods() : base()
		{
			InitializeComponent();
		}

		private void OptDrawLinesBetweenMethods_PreparePage(object sender, OptionsPageStorageEventArgs ea)
		{
			loadSettings();
		}

		private void OptDrawLinesBetweenMethods_CommitChanges(object sender, OptionsPageStorageEventArgs ea)
		{
			saveSettings();
		}


		bool _fullWidth = false;
		DashStyle _lineDashStyle = DashStyle.Solid;
		int _lineWidth = 1;
		Color _lineColor = Color.Silver;
		bool _drawLineAtEndOfMethod = false;
		bool _drawShadow = true;
		bool _enabled = true;

		void loadSettings()
		{
			try
			{

				using (DecoupledStorage storage = OptDrawLinesBetweenMethods.Storage)
				{
					_fullWidth = storage.ReadBoolean("DrawLinesBetweenMethods", "FullWidth", _fullWidth);
					_lineDashStyle = (DashStyle)storage.ReadEnum("DrawLinesBetweenMethods", "LineDashStyle", typeof(DashStyle), _lineDashStyle);
					_lineWidth = storage.ReadInt32("DrawLinesBetweenMethods", "LineWidth", _lineWidth);
					_lineColor = storage.ReadColor("DrawLinesBetweenMethods", "LineColor", _lineColor);
					_drawLineAtEndOfMethod = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawLineAtEndOfMethod", _drawLineAtEndOfMethod);
					_drawShadow = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawShadow", _drawShadow);
					_enabled = storage.ReadBoolean("DrawLinesBetweenMethods", "Enabled", _enabled);
				}

				_fullWidthChk.Checked = _fullWidth;
				_lineStyleLst.Text = _lineDashStyle.ToString();
				_lineWidthLst.Text = _lineWidth.ToString();
				_lineColorBtn.BackColor = _lineColor;
				_drawLineAtEndChk.Checked = _drawLineAtEndOfMethod;
				_drawShadowChk.Checked = _drawShadow;
				_enabledChk.Checked = _enabled;

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
				_lineColor = _lineColorBtn.BackColor;
				_drawLineAtEndOfMethod = _drawLineAtEndChk.Checked;
				_drawShadow = _drawShadowChk.Checked;
				_enabled = _enabledChk.Checked;

				using (DecoupledStorage storage = OptDrawLinesBetweenMethods.Storage)
				{
					storage.WriteBoolean("DrawLinesBetweenMethods", "FullWidth", _fullWidth);
					storage.WriteEnum("DrawLinesBetweenMethods", "LineDashStyle", _lineDashStyle);
					storage.WriteInt32("DrawLinesBetweenMethods", "LineWidth", _lineWidth);
					storage.WriteColor("DrawLinesBetweenMethods", "LineColor", _lineColor);
					storage.WriteBoolean("DrawLinesBetweenMethods", "DrawLineAtEndOfMethod", _drawLineAtEndOfMethod);
					storage.WriteBoolean("DrawLinesBetweenMethods", "DrawShadow", _drawShadow);
					storage.WriteBoolean("DrawLinesBetweenMethods", "Enabled", _enabled);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private void _lineColorBtn_Click(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog();
			dlg.AnyColor = true;
			dlg.FullOpen = true;
			dlg.Color = _lineColorBtn.BackColor;

			if (dlg.ShowDialog() == DialogResult.OK)
				_lineColorBtn.BackColor = dlg.Color;
		}

		private void OptDrawLinesBetweenMethods_Load(object sender, EventArgs e)
		{
			// Enable controls with Enabled checkbox
			_mainPanel.DataBindings.Add("Enabled", _enabledChk, "Checked");
		}

	}
}
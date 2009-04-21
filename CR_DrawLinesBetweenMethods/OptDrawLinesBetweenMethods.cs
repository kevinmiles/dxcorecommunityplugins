using System;
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
        public OptDrawLinesBetweenMethods()
        {
            InitializeComponent();
        }

        private void OptDrawLinesBetweenMethods_PreparePage(object sender, OptionsPageStorageEventArgs ea)
        {
            loadSettings();
        }

        private void OptDrawLinesBetweenMethods_CommitChanges(object sender, CommitChangesEventArgs ea)
        {
            saveSettings();
        }

        bool _fullWidth;
        DashStyle _lineDashStyle = DashStyle.Solid;
        int _lineWidth = 1;
        Color _lineColor = Color.Silver;
        bool _drawLineAtStartOfMethod = true;
        bool _drawLineAtEndOfMethod;
        bool _drawShadow = true;
        bool _enabled = true;
        bool _enableOnClass = true;
        bool _enableOnProperty = true;
        bool _enableOnMethod = true;
        bool _enableOnEnum = true;
        int _lineSpacer;
        int _shadowHeight = 5;

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
                    _drawLineAtStartOfMethod = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawLineAtStartOfMethod", _drawLineAtStartOfMethod);
                    _drawLineAtEndOfMethod = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawLineAtEndOfMethod", _drawLineAtEndOfMethod);
                    _drawShadow = storage.ReadBoolean("DrawLinesBetweenMethods", "DrawShadow", _drawShadow);
                    _enableOnClass = storage.ReadBoolean("DrawLinesBetweenMethods", "EnableOnClass", _enableOnClass);
                    _enableOnProperty = storage.ReadBoolean("DrawLinesBetweenMethods", "EnableOnProperty", _enableOnProperty);
                    _enableOnMethod = storage.ReadBoolean("DrawLinesBetweenMethods", "EnableOnMethod", _enableOnMethod);
                    _enableOnEnum = storage.ReadBoolean("DrawLinesBetweenMethods", "EnableOnEnum", _enableOnEnum);
                    _lineSpacer = storage.ReadInt32("DrawLinesBetweenMethods", "LineSpacer", _lineSpacer);
                    _shadowHeight = storage.ReadInt32("DrawLinesBetweenMethods", "ShadowHeight", _shadowHeight);
                    _enabled = storage.ReadBoolean("DrawLinesBetweenMethods", "Enabled", _enabled);
                }

                _fullWidthChk.Checked = _fullWidth;
                _lineStyleLst.Text = _lineDashStyle.ToString();
                _lineWidthLst.Text = _lineWidth.ToString();
                _lineColorBtn.BackColor = _lineColor;
                _drawLineAtStartChk.Checked = _drawLineAtStartOfMethod;
                _drawLineAtEndChk.Checked = _drawLineAtEndOfMethod;
                _drawShadowChk.Checked = _drawShadow;
                _enableOnMemberCheckList.SetItemChecked(0, _enableOnClass);
                _enableOnMemberCheckList.SetItemChecked(1, _enableOnProperty);
                _enableOnMemberCheckList.SetItemChecked(2, _enableOnMethod);
                _enableOnMemberCheckList.SetItemChecked(3, _enableOnEnum);
                _lineSpaceNUD.Value = _lineSpacer;
                _shadowHeightNUD.Value = _shadowHeight;
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
                _drawLineAtStartOfMethod = _drawLineAtStartChk.Checked;
                _drawLineAtEndOfMethod = _drawLineAtEndChk.Checked;
                _drawShadow = _drawShadowChk.Checked;
                _enableOnClass = _enableOnMemberCheckList.GetItemChecked(0);
                _enableOnProperty = _enableOnMemberCheckList.GetItemChecked(1);
                _enableOnMethod = _enableOnMemberCheckList.GetItemChecked(2);
                _enableOnEnum = _enableOnMemberCheckList.GetItemChecked(3);
                _enabled = _enabledChk.Checked;
                _lineSpacer = (int)_lineSpaceNUD.Value;
                _shadowHeight = (int)_shadowHeightNUD.Value;

                using (DecoupledStorage storage = OptDrawLinesBetweenMethods.Storage)
                {
                    storage.WriteBoolean("DrawLinesBetweenMethods", "FullWidth", _fullWidth);
                    storage.WriteEnum("DrawLinesBetweenMethods", "LineDashStyle", _lineDashStyle);
                    storage.WriteInt32("DrawLinesBetweenMethods", "LineWidth", _lineWidth);
                    storage.WriteColor("DrawLinesBetweenMethods", "LineColor", _lineColor);
                    storage.WriteBoolean("DrawLinesBetweenMethods", "DrawLineAtStartOfMethod", _drawLineAtStartOfMethod);
                    storage.WriteBoolean("DrawLinesBetweenMethods", "DrawLineAtEndOfMethod", _drawLineAtEndOfMethod);
                    storage.WriteBoolean("DrawLinesBetweenMethods", "DrawShadow", _drawShadow);
                    storage.WriteBoolean("DrawLinesBetweenMethods", "EnableOnClass", _enableOnClass);
                    storage.WriteBoolean("DrawLinesBetweenMethods", "EnableOnProperty", _enableOnProperty);
                    storage.WriteBoolean("DrawLinesBetweenMethods", "EnableOnMethod", _enableOnMethod);
                    storage.WriteBoolean("DrawLinesBetweenMethods", "Enabled", _enabled);
                    storage.WriteInt32("DrawLinesBetweenMethods", "LineSpacer", _lineSpacer);
                    storage.WriteInt32("DrawLinesBetweenMethods", "ShadowHeight", _shadowHeight);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }


        private void _lineColorBtn_Click(object sender, EventArgs e)
        {
            using (ColorDialog dlg = new ColorDialog { AnyColor = true, FullOpen = true, Color = _lineColorBtn.BackColor })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                    _lineColorBtn.BackColor = dlg.Color;
            }
        }

        private void OptDrawLinesBetweenMethods_Load(object sender, EventArgs e)
        {
            // Enable controls with Enabled checkbox
            _mainPanel.DataBindings.Add("Enabled", _enabledChk, "Checked");
        }

        private void _drawShadowChk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cbx = (CheckBox)sender;
            _shadowHeightNUD.Enabled = cbx.Checked;
        }

    }
}
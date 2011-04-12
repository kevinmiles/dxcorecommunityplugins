using System;
using System.Windows.Forms;
using System.Globalization;

namespace Refactor_Comments
{
	public partial class MultiLineCommentOptionsControl : UserControl
	{
		private string _languageId = "";
		private string _lineLeaderText = "";
		private Func<MultiLineCommentOptions, string> _previewGenerator = null;

		public MultiLineCommentOptionsControl()
		{
			InitializeComponent();
			this.LanguageId = "C#";
			this.LineLeaderText = "*";
		}

		public string LanguageId
		{
			get
			{
				return this._languageId;
			}
			set
			{
				this._languageId = value;
				this.optionGroup.Text = String.Format(CultureInfo.CurrentUICulture, Properties.Resources.MultiLineCommentOptionsControl_GroupBox, this._languageId);
			}
		}

		public string LineLeaderText
		{
			get
			{
				return this._lineLeaderText;
			}
			set
			{
				this._lineLeaderText = value;
				this.useLineLeaders.Text = String.Format(CultureInfo.CurrentUICulture, Properties.Resources.MultiLineCommentOptionsControl_LineLeadersLabel, this._lineLeaderText);
			}
		}

		public MultiLineCommentOptions Options
		{
			get
			{
				var options = new MultiLineCommentOptions
				{
					AddLineLeaderString = this.useLineLeaders.Checked,
					TerminatorOnLastCommentLine = this.sameLineTerminator.Checked
				};
				return options;
			}
			set
			{
				var options = value ?? new MultiLineCommentOptions();
				this.useLineLeaders.Checked = options.AddLineLeaderString;
				this.sameLineTerminator.Checked = options.TerminatorOnLastCommentLine;
			}
		}

		public Func<MultiLineCommentOptions, string> PreviewGenerator
		{
			get
			{
				return this._previewGenerator;
			}
			set
			{
				this._previewGenerator = value;
				this.RebuildPreview();
			}
		}

		public void RebuildPreview()
		{
			if (this.PreviewGenerator == null)
			{
				return;
			}

			var options = this.Options;
			var preview = this.PreviewGenerator(options);
			this.exampleBox.Text = preview;
		}

		private void sameLineTerminator_CheckedChanged(object sender, EventArgs e)
		{
			this.RebuildPreview();
		}

		private void useLineLeaders_CheckedChanged(object sender, EventArgs e)
		{
			this.RebuildPreview();
		}
	}
}

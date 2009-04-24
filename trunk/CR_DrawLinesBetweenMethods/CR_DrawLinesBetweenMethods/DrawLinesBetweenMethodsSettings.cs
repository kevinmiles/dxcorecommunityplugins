using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using DevExpress.CodeRush.Core;

namespace CR_DrawLinesBetweenMethods
{
	class DrawLinesBetweenMethodsSettings
	{
		public DrawLinesBetweenMethodsSettings()
		{
			FullWidth = false;
			LineDashStyle = DashStyle.Solid;
			LineWidth = 1;
			DrawLineAtEndOfMethod = false;
			DrawShadow = true;
		}

		private void LoadFromStorage(DecoupledStorage storage)
		{
			
		}

		public bool FullWidth { get; set; }
		public DashStyle LineDashStyle { get; set; }
		public int LineWidth { get; set; }
		public Color LineColor { get; set; }
		public bool DrawLineAtEndOfMethod { get; set; }
		public bool DrawShadow { get; set; }

	}
}

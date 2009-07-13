using System.Collections.Generic;
using System.Drawing;

namespace CR_NCover
{
	public class CoverageOverlay
	{
		public SolidBrush NotVisitedColor
		{
			get { return new SolidBrush(Color.FromArgb(77, Color.Red)); }
		}

		public SolidBrush VisitedColor
		{
			get { return new SolidBrush(Color.FromArgb(77, Color.Green)); }
		}

		public void Show(IList<CoverageResult> results)
		{
			Update(results);
		}

		public void Update(IList<CoverageResult> results)
		{
			foreach (var r in results)
				OverlayResult(r);
		}

		private void OverlayResult(CoverageResult result)
		{
			Rectangle area = GetAreaFrom(result);
			VisualStudio.FillRectangle(GetBrush(result), area);
		}

		public void Hide()
		{
			VisualStudio.Invalidate();
		}

		private SolidBrush GetBrush(CoverageResult result)
		{
			return result.VisitCount > 0 ? VisitedColor : NotVisitedColor;
		}

		private static Rectangle GetAreaFrom(CoverageResult result)
		{
			return VisualStudio.GetRectangleFromRange(result.Line, result.Column, result.LineEnd, result.ColumnEnd);
		}
	}
}
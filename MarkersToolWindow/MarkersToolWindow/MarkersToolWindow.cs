using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.CodeRush.Core;
using DevExpress.CodeRush.PlugInCore;
using DevExpress.CodeRush.StructuralParser;

namespace MarkersToolWindow
{
	[Title("Markers")]
	public partial class MarkersToolWindow : ToolWindowPlugIn
	{
		private int _StartLine;
    private IMarker _SelectedMarker;
    // DXCore-generated code...
		#region InitializePlugIn
		public override void InitializePlugIn()
		{
			base.InitializePlugIn();
			lstMarkers.Font = CodeRush.VSSettings.Font;

			//
			// TODO: Add your initialization code here.
			//
		}
		#endregion
		#region FinalizePlugIn
		public override void FinalizePlugIn()
		{
			//
			// TODO: Add your finalization code here.
			//

			base.FinalizePlugIn();
		}
		#endregion

		private void lstMarkers_DrawItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();
			Rectangle bounds = e.Bounds;
			if (e.Index >= 0 && e.Index < lstMarkers.Items.Count)
				using (Brush brush = new SolidBrush(e.ForeColor))
				{
					IMarker marker = lstMarkers.Items[e.Index] as IMarker;
					TextDocument activeTextDocument = marker.TextDocument;
					if (activeTextDocument == null)
						return;
					string codeLine = activeTextDocument.GetLine(marker.Line);
					codeLine = codeLine.Trim();
					e.Graphics.DrawString(codeLine, lstMarkers.Font, brush, bounds.Left + 2, bounds.Top);
				}
			if (e.Index == lstMarkers.SelectedIndex)
				e.DrawFocusRectangle();
		}

		private void lstMarkers_DoubleClick(object sender, EventArgs e)
		{
			if (lstMarkers.SelectedIndex < 0 || lstMarkers.SelectedIndex >= lstMarkers.Items.Count)
				return;
			IMarker marker = lstMarkers.Items[lstMarkers.SelectedIndex] as IMarker;
			if (marker == null)
				return;
			marker.RestorePosition();
			using (LocatorBeacon locatorBeacon = new LocatorBeacon())
			{
				locatorBeacon.Color = DevExpress.DXCore.Platform.Drawing.Color.FromArgb(0x6F, 0x62, 0xBD);
				locatorBeacon.Duration = 2500;
				locatorBeacon.Start(marker.Line, marker.Column);
			}
		}

		private void lstMarkers_SelectedIndexChanged(object sender, EventArgs e)
		{
			_SelectedMarker = null;
			if (lstMarkers.SelectedIndex < 0 || lstMarkers.SelectedIndex >= lstMarkers.Items.Count)
				return;
			IMarker marker = lstMarkers.Items[lstMarkers.SelectedIndex] as IMarker;
			if (marker == null)
				return;
			_SelectedMarker = marker;
			if (_SelectedMarker == null)
				return;

			TextDocument activeTextDocument = _SelectedMarker.TextDocument;
			if (activeTextDocument == null)
				return;

			int linesToShow = codePreview.Height / codePreview.LineHeight;
			_StartLine = _SelectedMarker.Line - linesToShow / 2;
			if (_StartLine < 1)
				_StartLine = 1;
			int endLine = _StartLine + linesToShow;
			if (endLine > activeTextDocument.LineCount)
				endLine = activeTextDocument.LineCount;
			string code = activeTextDocument.GetText(_StartLine, endLine);
			codePreview.ShowCode(code, activeTextDocument.Language);
		}

		private void codePreview_Painted(object sender, PaintEventArgs e)
		{
			if (_SelectedMarker == null)
				return;

			int yOffset = (_SelectedMarker.Line - _StartLine) * codePreview.LineHeight + codePreview.LineHeight / 2;
			TextDocument activeTextDocument = _SelectedMarker.TextDocument;
			if (activeTextDocument == null)
				return;
			TextView activeView = activeTextDocument.ActiveView;
			if (activeView == null)
				activeView = CodeRush.TextViews.Active;

			int columnWidth;
			if (activeView == null)
				columnWidth = 12;
			else
				columnWidth = activeView.ColumnWidth;
			int xOffset = (_SelectedMarker.Column + 1) * columnWidth;
			int radius = 20;
			using (Pen outlinePen = new Pen(Color.FromArgb(0x42, 0xFF, 0x00, 0x00), 5))
			{
				e.Graphics.DrawEllipse(outlinePen, xOffset - radius, yOffset - radius, radius * 2, radius * 2);
			}
		}

		private void events_MarkerCollected(MarkerEventArgs ea)
		{
			UpdateMarkerList();
		}

		private void events_MarkerDropped(MarkerEventArgs ea)
		{
			UpdateMarkerList();
		}

		private void UpdateMarkerList()
		{
			_SelectedMarker = null;
			lstMarkers.Items.Clear();
			foreach (IMarker marker in CodeRush.Markers)
			{
				lstMarkers.Items.Add(marker);
			}
		}

		private void events_DocumentClosing(DocumentEventArgs ea)
		{
			UpdateMarkerList();
		}
	}
}
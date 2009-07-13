using System.Drawing;
using System.IO;
using DevExpress.CodeRush.Core;

namespace CR_NCover
{
	public class VisualStudio
	{
		public static bool HasASolutionOpened
		{
			get { return CodeRush.Solution.Active.IsOpen; }
		}

		public static TextView TextView
		{
			get { return CodeRush.TextViews.Active; }
		}

		public static string ActiveFilePath
		{
			get { return HasAnActiveTextView? TextView.FileNode.Name : null; }
		}

		public static string Solution
		{
			get { return Path.GetFileNameWithoutExtension(CodeRush.Solution.Active.FullName); }
		}

		public static bool HasAnActiveTextView
		{
			get { return TextView != null ? TextView.IsActive && TextView.FileNode != null && TextView.FileNode.Name != null : false; }
		}

		public static Rectangle GetRectangleFromRange(int line, int column, int lineEnd, int columnEnd)
		{
			return TextView.GetRectangleFromRange(line, column, lineEnd, columnEnd);
		}

		public static void FillRectangle(SolidBrush brush, Rectangle rectangle)
		{
			TextView.Graphics.FillRectangle(brush, rectangle);
		}

		public static void Invalidate()
		{
			TextView.Invalidate();
		}
	}
}
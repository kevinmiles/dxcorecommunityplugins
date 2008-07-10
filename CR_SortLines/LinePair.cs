using System;

namespace CR_SortLines
{
	/// <summary>
	/// Provides a sortable object with the original line and its sortable version.
	/// </summary>
	public class LinePair
	{
		private string _OriginalLine;
		public string OriginalLine {
			get {
				return _OriginalLine;
			}
			set {
				_OriginalLine = value;
			}
		}

		private string _SortableLine;
		public string SortableLine {
			get {
				return _SortableLine;
			}
			set {
				_SortableLine = value;
			}
		}

		public LinePair()
		{
		}
	}
}

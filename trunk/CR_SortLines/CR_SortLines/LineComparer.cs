using System;

namespace CR_SortLines
{
	/// <summary>
	/// Compares <see cref="LinePair"/> objects.
	/// </summary>
	public abstract class LineComparer : System.Collections.IComparer
	{
		/// <summary>
		/// Compares <see cref="LinePair"/> objects.
		/// </summary>
		/// <param name="x">The first <see cref="LinePair"/> to compare.</param>
		/// <param name="y">The second <see cref="LinePair"/> to compare.</param>
		/// <returns>
		/// <c>-1</c> if <paramref name="x" /> is less than <paramref name="y" />;
		/// <c>0</c> if <paramref name="x" /> equals <paramref name="y" />; <c>1</c> if
		/// <paramref name="x" /> is greater than <paramref name="y" />.
		/// </returns>
		/// <remarks>
		/// <para>
		/// Uses the <see cref="LinePair.SortableLine"/> property on each <see cref="LinePair"/>
		/// as the basis for comparison.
		/// </para>
		/// </remarks>
		/// <seealso cref="LinePair" />
		public abstract int Compare(object x, object y);
	}
}

using System;

namespace CR_SortLines
{
	/// <summary>
	/// Compares <see cref="LinePair"/> objects in a case-sensitive fashion.
	/// </summary>
	public class CaseSensitiveLineComparer : LineComparer
	{
		#region IComparer Members

		/// <summary>
		/// Compares <see cref="LinePair"/> objects in a case-sensitive fashion.
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
		public override int Compare(object x, object y) {
			LinePair xpair = x as LinePair;
			LinePair ypair = y as LinePair;

			if(xpair == null && ypair == null){
				return 0;
			}

			if(xpair == null && ypair != null){
				return -1;
			}

			if(xpair != null && ypair == null){
				return 1;
			}

			return String.Compare(xpair.SortableLine, ypair.SortableLine, false);
		}

		#endregion
	}
}

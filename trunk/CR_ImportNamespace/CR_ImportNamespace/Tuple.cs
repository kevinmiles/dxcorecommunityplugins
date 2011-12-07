using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CR_ImportNamespace
{
	/// <summary>
	/// Represents a container which contains three object
	/// </summary>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	/// <typeparam name="T3"></typeparam>
	public class Tuple<T1, T2, T3>
		: Tuple<T1, T2>, IEqualityComparer<Tuple<T1, T2, T3>>
	{
		/// <summary>
		/// Third item
		/// </summary>
		public T3 Third { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <param name="third"></param>
		public Tuple(T1 first, T2 second, T3 third)
			: base(first, second)
		{
			Third = third;
		}

		/// <summary>
		/// Checks for equality
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool Equals(Tuple<T1, T2, T3> x, Tuple<T1, T2, T3> y)
		{
			return EqualityComparer<T1>.Default.Equals(x.First, y.First) &&
		  EqualityComparer<T2>.Default.Equals(x.Second, y.Second) &&
		  EqualityComparer<T3>.Default.Equals(x.Third, y.Third);
		}

		/// <summary>
		/// Checks for equality of a specific object
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2, T3> && Equals(this, (Tuple<T1, T2, T3>)obj);
		}

		/// <summary>
		/// Returns the hash code of a specific object
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int GetHashCode(Tuple<T1, T2, T3> obj)
		{
			return EqualityComparer<T1>.Default.GetHashCode(First) ^
		  EqualityComparer<T2>.Default.GetHashCode(Second) ^
		  EqualityComparer<T3>.Default.GetHashCode(Third);
		}

		/// <summary>
		/// Overrides the == operator
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(Tuple<T1, T2, T3> left,
									   Tuple<T1, T2, T3> right)
		{
			if (((object)left) == null && ((object)right) == null)
			{
				return true;
			}

			return left.Equals(right);
		}

		/// <summary>
		/// Overrides the != operator
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(Tuple<T1, T2, T3> left,
									   Tuple<T1, T2, T3> right)
		{
			if (((object)left) == null && ((object)right) == null)
			{
				return false;
			}

			return !left.Equals(right);
		}
	}

	/// <summary>
	/// Represents a container with two objects
	/// </summary>
	/// <typeparam name="T1"></typeparam>
	/// <typeparam name="T2"></typeparam>
	public class Tuple<T1, T2> : IEqualityComparer<Tuple<T1, T2>>
	{
		/// <summary>
		/// First item
		/// </summary>
		public T1 First { get; private set; }

		/// <summary>
		/// Second item
		/// </summary>
		public T2 Second { get; private set; }

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="first"></param>
		/// <param name="second"></param>
		public Tuple(T1 first, T2 second)
		{
			First = first;
			Second = second;
		}

		/// <summary>
		/// Checks for equality
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <returns></returns>
		public bool Equals(Tuple<T1, T2> x, Tuple<T1, T2> y)
		{
			return EqualityComparer<T1>.Default.Equals(x.First, y.First)
		  && EqualityComparer<T2>.Default.Equals(x.Second, y.Second);
		}

		/// <summary>
		/// Checks for equality
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			return obj is Tuple<T1, T2> && Equals(this, (Tuple<T1, T2>)obj);
		}

		/// <summary>
		/// Returns the hash code of a object
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int GetHashCode(Tuple<T1, T2> obj)
		{
			return EqualityComparer<T1>.Default.GetHashCode(First) ^
		  EqualityComparer<T2>.Default.GetHashCode(Second);
		}

		/// <summary>
		/// Overrides the == operator
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator ==(Tuple<T1, T2> left, Tuple<T1, T2> right)
		{
			if (((object)left) == null && ((object)right) == null)
			{
				return true;
			}

			return left.Equals(right);
		}

		/// <summary>
		/// Overrides the != operator
		/// </summary>
		/// <param name="left"></param>
		/// <param name="right"></param>
		/// <returns></returns>
		public static bool operator !=(Tuple<T1, T2> left, Tuple<T1, T2> right)
		{
			if (((object)left) == null && ((object)right) == null)
			{
				return false;
			}

			return !left.Equals(right);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace TripLog.Extensions
{
	public static class EnumerableExtensions
	{
		public static bool None<T>(this IEnumerable<T> enumerable)
		{
			return !enumerable.Any();
		}

		public static bool None<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
		{
			return !enumerable.Any(predicate);
		}

		public static IEnumerable<T> Safe<T>(this IEnumerable<T> enumerable)
		{
			return enumerable ?? Enumerable.Empty<T>();
		}
	}
}

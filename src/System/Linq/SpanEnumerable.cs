using System.Numerics;

namespace System.Linq;

/// <summary>
/// Provides extension members on <see cref="ReadOnlySpan{T}"/>.
/// </summary>
/// <seealso cref="ReadOnlySpan{T}"/>
public static class SpanEnumerable
{
	/// <summary>
	/// Generates a sequence of integral numbers within a specified range.
	/// </summary>
	/// <param name="start">The value of the first integer in the sequence.</param>
	/// <param name="count">The number of sequential integers to generate.</param>
	/// <returns>
	/// A <see cref="ReadOnlySpan{T}"/> that contains a range of sequential integral numbers.
	/// </returns>
	public static ReadOnlySpan<int> Range(int start, int count)
	{
		var result = new int[count];
		for (var (i, value) = (0, start); i < count; i++, value++)
		{
			result[i] = value;
		}
		return result;
	}


	extension<TSource>(ReadOnlySpan<TSource> @this)
	{
		/// <inheritdoc cref="Enumerable.All{TSource}(IEnumerable{TSource}, Func{TSource, bool})"/>
		public bool All(Predicate<TSource> predicate)
		{
			foreach (var element in @this)
			{
				if (!predicate(element))
				{
					return false;
				}
			}
			return true;
		}

		/// <inheritdoc cref="Enumerable.Min{TSource, TResult}(IEnumerable{TSource}, Func{TSource, TResult})"/>
		public TResult Min<TResult>(Func<TSource, TResult> keySelector) where TResult : INumber<TResult>, IMinMaxValue<TResult>
		{
			var result = TResult.MaxValue;
			foreach (var element in @this)
			{
				if (keySelector(element) is var p && p <= result)
				{
					result = p;
				}
			}
			return result;
		}

		/// <inheritdoc cref="Enumerable.Max{TSource, TResult}(IEnumerable{TSource}, Func{TSource, TResult})"/>
		public TResult Max<TResult>(Func<TSource, TResult> keySelector) where TResult : INumber<TResult>, IMinMaxValue<TResult>
		{
			var result = TResult.MinValue;
			foreach (var element in @this)
			{
				if (keySelector(element) is var p && p >= result)
				{
					result = p;
				}
			}
			return result;
		}
	}

	/// <typeparam name="TSource">The type of source.</typeparam>
	/// <typeparam name="TResult">The type of result.</typeparam>
	/// <param name="this">The source collection.</param>
	extension<TSource, TResult>(scoped ReadOnlySpan<TSource> @this)
	{
		/// <inheritdoc cref="Enumerable.Select{TSource, TResult}(IEnumerable{TSource}, Func{TSource, TResult})"/>
		public ReadOnlySpan<TResult> Select(Func<TSource, TResult> selector)
		{
			var result = new TResult[@this.Length];
			for (var i = 0; i < @this.Length; i++)
			{
				result[i] = selector(@this[i]);
			}
			return result;
		}
	}
}

namespace Sudoku.Concepts;

/// <summary>
/// Represents an easy way to decompose a value into a triplet.
/// </summary>
public static class SizeDecomposer
{
	/// <summary>
	/// Decompose a number. This method can be used by calculating the number of rows and columns.
	/// </summary>
	/// <param name="size">The value.</param>
	/// <returns>
	/// A triplet of values <c>(M, N, P)</c>.
	/// Such values can be constructed into <paramref name="size"/> by <c>M * N + P</c>.
	/// </returns>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	/// <exception cref="InvalidOperationException"></exception>
	public static (int M, int N, int P) Decompose(int size)
	{
		if (size is <= 0 or >= 100)
		{
			throw new ArgumentOutOfRangeException(nameof(size), $"{nameof(size)} must be in [1, 99].");
		}

		var start = (int)Math.Floor(Math.Sqrt(size));
		for (var n = start; n >= 1; n--)
		{
			var m = size / n;
			if (m >= n)
			{
				var p = size - m * n;
				return (m, n, p);
			}
		}
		throw new InvalidOperationException("No valid decomposition found.");
	}
}

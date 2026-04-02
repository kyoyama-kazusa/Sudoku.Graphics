namespace Sudoku.Graphics;

/// <summary>
/// Represents a locator object.
/// </summary>
/// <typeparam name="TSelf">The type implementing this interface.</typeparam>
public interface ILocator<TSelf> : IEquatable<TSelf> where TSelf : unmanaged, ILocator<TSelf>
{
	/// <summary>
	/// Determine whether the current instance is on the specified side of the specified position.
	/// </summary>
	/// <param name="other">The other instance.</param>
	/// <param name="direction">The direction to be checked.</param>
	/// <param name="mapper">The point mapper instance.</param>
	/// <param name="isInStrictDirection">
	/// Indicates whether we should check both factors no matter what kind of direction we should check.
	/// If not, we only compare two indices, to determine which one has greater in value.
	/// </param>
	/// <returns>A <see cref="bool"/> result.</returns>
	/// <exception cref="ArgumentOutOfRangeException">
	/// Throws when <paramref name="direction"/> is not defined or <see cref="Direction4.None"/>.
	/// </exception>
	/// <exception cref="NotSupportedException">
	/// Throws when <paramref name="isInStrictDirection"/> is <see langword="false"/>,
	/// and <paramref name="direction"/> is either <see cref="Direction4.Left"/> or <see cref="Direction4.Right"/>
	/// (because such direction is not well-defined to compare indices).
	/// </exception>
	bool IsSideWith(TSelf other, Direction4 direction, PointMapper mapper, bool isInStrictDirection);


	/// <summary>
	/// Creates a locator measurer value that based on the current type of the locator.
	/// </summary>
	/// <param name="locator">The locator object.</param>
	/// <param name="cellSize">The cell size.</param>
	/// <returns>The result.</returns>
	static abstract float GetLocatorMeasurer(TSelf locator, float cellSize);
}

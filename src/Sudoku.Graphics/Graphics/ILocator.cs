namespace Sudoku.Graphics;

/// <summary>
/// Represents a locator object.
/// </summary>
/// <typeparam name="TSelf">The type implementing this interface.</typeparam>
public interface ILocator<TSelf> : IEquatable<TSelf> where TSelf : unmanaged, ILocator<TSelf>
{
	/// <summary>
	/// Creates a locator measurer value that based on the current type of the locator.
	/// </summary>
	/// <param name="locator">The locator object.</param>
	/// <param name="cellSize">The cell size.</param>
	/// <returns>The result.</returns>
	static abstract float GetLocatorMeasurer(TSelf locator, float cellSize);
}

namespace System.Windows;

/// <summary>
/// Provides extension members on <see cref="Point"/>.
/// </summary>
/// <seealso cref="Point"/>
public static class PointExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Point"/> instances.
	/// </summary>
	/// <param name="this">The current instance.</param>
	extension(Point @this)
	{
		/// <summary>
		/// Deconstruct the current instance into a pair of factors <see cref="Point.X"/> and <see cref="Point.Y"/>.
		/// </summary>
		/// <param name="x"><inheritdoc cref="Point.X" path="/summary"/></param>
		/// <param name="y"><inheritdoc cref="Point.Y" path="/summary"/></param>
		/// <seealso cref="Point.X"/>
		/// <seealso cref="Point.Y"/>
		public void Deconstruct(out double x, out double y) => (x, y) = (@this.X, @this.Y);
	}
}

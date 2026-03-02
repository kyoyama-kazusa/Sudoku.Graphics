namespace SkiaSharp;

/// <summary>
/// Provdies extension members on <see cref="SKPoint"/>.
/// </summary>
/// <seealso cref="SKPoint"/>
public static class SKPointExtensions
{
	/// <param name="this">The current instance.</param>
	extension(SKPoint @this)
	{
		/// <summary>
		/// Deconstruct instance into multiple values.
		/// </summary>
		public void Deconstruct(out float x, out float y) => (x, y) = (@this.X, @this.Y);
	}
}

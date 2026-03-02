namespace SkiaSharp;

/// <summary>
/// Provides extension members on type <see cref="SKRect"/>.
/// </summary>
/// <seealso cref="SKRect"/>
public static class SKRectExtensions
{
	/// <param name="this">The instance.</param>
	extension(SKRect @this)
	{
		/// <summary>
		/// Creates an <see cref="SKRect"/> instance with specified value as padding.
		/// </summary>
		/// <param name="uniform">The uniform value as padding.</param>
		/// <returns>An <see cref="SKRect"/> instance created.</returns>
		public SKRect Padding(float uniform)
		{
			var topLeft = new SKPoint(@this.Left + uniform, @this.Top + uniform);
			var bottomRight = new SKPoint(@this.Right - uniform, @this.Bottom - uniform);
			return SKRect.Create(topLeft, bottomRight);
		}


		/// <summary>
		/// Creates an <see cref="SKRect"/> instance via top-left and bottom-right points.
		/// </summary>
		/// <param name="topLeft">The top-left point.</param>
		/// <param name="bottomRight">The bottom-right point.</param>
		/// <returns>An <see cref="SKRect"/> instance created.</returns>
		/// <exception cref="ArgumentException">
		/// Throws when <paramref name="bottomRight"/> is less than <paramref name="topLeft"/> in logical position.
		/// </exception>
		public static SKRect Create(SKPoint topLeft, SKPoint bottomRight)
		{
			var width = bottomRight.X - topLeft.X;
			var height = bottomRight.Y - topLeft.Y;
			ArgumentException.Assert(width >= 0 && height >= 0);
			return SKRect.Create(topLeft.X, topLeft.Y, width, height);
		}
	}
}

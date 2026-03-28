namespace SkiaSharp;

/// <summary>
/// Provides extension members on <see cref="SKFont"/>.
/// </summary>
/// <seealso cref="SKFont"/>
public static class SKFontExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="SKFont"/> instances.
	/// </summary>
	/// <param name="this">The current instance.</param>
	extension(SKFont @this)
	{
		/// <summary>
		/// Sets the scaling X of text.
		/// </summary>
		/// <param name="textWidth">The width of text.</param>
		/// <param name="maxWidth">The maximum width that a text can be drawn.</param>
		public void SetScaleX(float textWidth, float maxWidth) => @this.ScaleX = textWidth > 0 ? MathF.Min(1, maxWidth / textWidth) : 1;
	}
}

namespace SkiaSharp;

/// <summary>
/// Provides extension members on type <see cref="SKSurface"/>.
/// </summary>
/// <seealso cref="SKSurface"/>
public static class SKSurfaceExtensions
{
	extension(SKSurface)
	{
		/// <summary>
		/// Creates an <see cref="SKSurface"/> instance via the specified <see cref="SKSizeI"/> value.
		/// </summary>
		/// <param name="size">The <see cref="SKSizeI"/> instance.</param>
		/// <returns>An <see cref="SKSurface"/> instance created.</returns>s
		public static SKSurface Create(SKSizeI size) => SKSurface.Create(new SKImageInfo(size.Width, size.Height));
	}
}

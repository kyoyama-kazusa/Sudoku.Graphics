namespace SkiaSharp;

/// <summary>
/// Provides extension members on type <see cref="SKImageInfo"/>.
/// </summary>
/// <seealso cref="SKImageInfo"/>
public static class SKImageInfoExtensions
{
	extension(SKImageInfo)
	{
		/// <summary>
		/// Creates a <see cref="SKImageInfo"/> instance via the specified <see cref="SKSizeI"/> value.
		/// </summary>
		/// <param name="size">The <see cref="SKSizeI"/> instance.</param>
		/// <returns>An <see cref="SKImageInfo"/> instance created.</returns>
		public static SKImageInfo Create(SKSizeI size) => new(size.Width, size.Height);
	}
}

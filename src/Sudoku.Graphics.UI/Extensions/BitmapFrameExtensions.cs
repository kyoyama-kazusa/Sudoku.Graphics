namespace System.Windows.Media.Imaging;

/// <summary>
/// Provides extension members on type <see cref="BitmapFrame"/>.
/// </summary>
/// <seealso cref="BitmapFrame"/>
public static class BitmapFrameExtensions
{
	extension(BitmapFrame)
	{
		/// <inheritdoc cref="BitmapFrame.Create(BitmapSource)"/>
		public static BitmapFrame Create(ImageSource? imageSource) => BitmapFrame.Create(imageSource as BitmapSource);
	}
}

namespace SkiaSharp;

/// <summary>
/// Provides extension members on <see cref="SKColors"/> type.
/// </summary>
/// <seealso cref="SKColors"/>
public static class SKColorsExtensions
{
	/// <summary>
	/// Provides extension properties of <see cref="SKColors"/>.
	/// </summary>
	extension(SKColors)
	{
		/// <summary>
		/// Represents color of I piece.
		/// </summary>
		public static SKColor Tetrimino_I => new(0, 253, 255);

		/// <summary>
		/// Represents color of O piece.
		/// </summary>
		public static SKColor Tetrimino_O => new(255, 255, 0);

		/// <summary>
		/// Represents color of T piece.
		/// </summary>
		public static SKColor Tetrimino_T => new(255, 0, 255);

		/// <summary>
		/// Represents color of L piece.
		/// </summary>
		public static SKColor Tetrimino_L => new(255, 129, 0);

		/// <summary>
		/// Represents color of J piece.
		/// </summary>
		public static SKColor Tetrimino_J => new(0, 0, 255);

		/// <summary>
		/// Represents color of S piece.
		/// </summary>
		public static SKColor Tetrimino_S => new(0, 255, 0);

		/// <summary>
		/// Represents color of Z piece.
		/// </summary>
		public static SKColor Tetrimino_Z => new(255, 0, 0);
	}
}

namespace Sudoku.ComponentModel.Suits;

/// <summary>
/// Provides extension members on <see cref="Suit"/>.
/// </summary>
/// <seealso cref="Suit"/>
public static class SuitExtensions
{
	/// <param name="this">The current instance.</param>
	extension(Suit @this)
	{
		/// <summary>
		/// Indicates fill color.
		/// </summary>
		public SKColor FillColor => @this switch { Suit.Heart or Suit.Diamond => SKColors.Red, _ => SKColors.Black };
	}
}

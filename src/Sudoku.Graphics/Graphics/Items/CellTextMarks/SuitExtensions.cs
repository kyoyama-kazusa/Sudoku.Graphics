namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Provides extension members on <see cref="Suit"/>.
/// </summary>
/// <seealso cref="Suit"/>
public static class SuitExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Suit"/> instances.
	/// </summary>
	/// <param name="this">The current instance.</param>
	extension(Suit @this)
	{
		/// <summary>
		/// Indicates the equivalent character of the suit.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="this"/> is not defined.</exception>
		public char EquivalentChar
			=> @this switch
			{
				Suit.Spade => '\u2660',
				Suit.Heart => '\u2665',
				Suit.Club => '\u2663',
				Suit.Diamond => '\u2666',
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};

		/// <summary>
		/// Indicates fill color.
		/// </summary>
		public SKColor FillColor => @this switch { Suit.Heart or Suit.Diamond => SKColors.Red, _ => SKColors.Black };
	}
}

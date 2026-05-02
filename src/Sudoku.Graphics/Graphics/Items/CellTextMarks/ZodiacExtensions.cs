namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Provides extensions for type <see cref="Zodiac"/>.
/// </summary>
/// <seealso cref="Zodiac"/>
public static class ZodiacExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="Zodiac"/> instances.
	/// </summary>
	/// <param name="this">The current instance.</param>
	extension(Zodiac @this)
	{
		/// <summary>
		/// Indicates the emoji string representation of this symbol.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="this"/> is not defined.</exception>
		public string EmojiString
			=> @this switch
			{
				Zodiac.Rat => "\uD83D\uDC2D",
				Zodiac.Ox => "\uD83D\uDC2E",
				Zodiac.Tiger => "\uD83D\uDC2F",
				Zodiac.Rabbit => "\uD83D\uDC30",
				Zodiac.Dragon => "\uD83D\uDC32",
				Zodiac.Snake => "\uD83D\uDC0D",
				Zodiac.Horse => "\uD83D\uDC34",
				Zodiac.Sheep => "\uD83D\uDC11",
				Zodiac.Monkey => "\uD83D\uDC35",
				Zodiac.Rooster => "\uD83D\uDC14",
				Zodiac.Dog => "\uD83D\uDC36",
				Zodiac.Pig => "\uD83D\uDC37",
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};
	}
}

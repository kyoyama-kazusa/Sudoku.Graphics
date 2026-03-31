namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Provides extensions for type <see cref="ZodiacSymbol"/>.
/// </summary>
/// <seealso cref="ZodiacSymbol"/>
public static class ZodiacSymbolExtensions
{
	/// <summary>
	/// Provides extension members on <see cref="ZodiacSymbol"/> instances.
	/// </summary>
	/// <param name="this">The current instance.</param>
	extension(ZodiacSymbol @this)
	{
		/// <summary>
		/// Indicates the emoji string representation of this symbol.
		/// </summary>
		/// <exception cref="ArgumentOutOfRangeException">Throws when <paramref name="this"/> is not defined.</exception>
		public string EmojiString
			=> @this switch
			{
				ZodiacSymbol.Rat => "\uD83D\uDC2D",
				ZodiacSymbol.Ox => "\uD83D\uDC2E",
				ZodiacSymbol.Tiger => "\uD83D\uDC2F",
				ZodiacSymbol.Rabbit => "\uD83D\uDC30",
				ZodiacSymbol.Dragon => "\uD83D\uDC32",
				ZodiacSymbol.Snake => "\uD83D\uDC0D",
				ZodiacSymbol.Horse => "\uD83D\uDC34",
				ZodiacSymbol.Sheep => "\uD83D\uDC11",
				ZodiacSymbol.Monkey => "\uD83D\uDC35",
				ZodiacSymbol.Rooster => "\uD83D\uDC14",
				ZodiacSymbol.Dog => "\uD83D\uDC36",
				ZodiacSymbol.Pig => "\uD83D\uDC37",
				_ => throw new ArgumentOutOfRangeException(nameof(@this))
			};
	}
}

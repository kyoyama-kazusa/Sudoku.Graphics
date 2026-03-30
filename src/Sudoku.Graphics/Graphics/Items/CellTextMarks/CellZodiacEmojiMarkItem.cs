namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents a cell zodiac emoji mark item.
/// </summary>
public sealed record CellZodiacEmojiMarkItem : CellTextMarkItem
{
	/// <summary>
	/// Indicates the zodiac animal.
	/// </summary>
	public required ZodiacAnimal Zodiac { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellText_ZodiacEmoji;

	/// <inheritdoc/>
	protected override string PrintingText
		=> Zodiac switch
		{
			ZodiacAnimal.Rat => "\uD83D\uDC2D",
			ZodiacAnimal.Ox => "\uD83D\uDC2E",
			ZodiacAnimal.Tiger => "\uD83D\uDC2F",
			ZodiacAnimal.Rabbit => "\uD83D\uDC30",
			ZodiacAnimal.Dragon => "\uD83D\uDC32",
			ZodiacAnimal.Snake => "\uD83D\uDC0D",
			ZodiacAnimal.Horse => "\uD83D\uDC34",
			ZodiacAnimal.Sheep => "\uD83D\uDC11",
			ZodiacAnimal.Monkey => "\uD83D\uDC35",
			ZodiacAnimal.Rooster => "\uD83D\uDC14",
			ZodiacAnimal.Dog => "\uD83D\uDC36",
			ZodiacAnimal.Pig => "\uD83D\uDC37",
			_ => throw new InvalidOperationException("The specified zodiac is not defined or invalid.")
		};
}

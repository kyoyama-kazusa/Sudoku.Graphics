namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents a cell zodiac emoji mark item.
/// </summary>
public sealed record CellZodiacEmojiMarkItem : CellTextMarkItem
{
	/// <summary>
	/// Indicates the zodiac symbol.
	/// </summary>
	public required ZodiacSymbol Symbol { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellText_ZodiacEmoji;

	/// <inheritdoc/>
	protected override string PrintingText => Symbol.EmojiString;
}

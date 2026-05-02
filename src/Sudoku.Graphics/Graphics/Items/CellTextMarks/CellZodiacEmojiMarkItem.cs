namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents a cell zodiac emoji mark item.
/// </summary>
public sealed record CellZodiacEmojiMarkItem : CellTextMarkItem
{
	/// <summary>
	/// Indicates the zodiac.
	/// </summary>
	public required Zodiac Zodiac { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellText_ZodiacEmoji;

	/// <inheritdoc/>
	protected override string PrintingText => Zodiac.EmojiString;
}

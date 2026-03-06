namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents emoji mark item.
/// </summary>
public sealed record CellEmojiMarkItem : CellTextMarkItem
{
	/// <summary>
	/// Indicates the emoji.
	/// </summary>
	public required string Emoji { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_EmojiText;

	/// <inheritdoc/>
	protected override string PrintingText => Emoji;
}

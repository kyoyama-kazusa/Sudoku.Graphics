namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents emoji mark item.
/// </summary>
public sealed class CellEmojiMarkItem : CellTextMarkItem
{
	/// <summary>
	/// Indicates the emoji.
	/// </summary>
	public required string Emoji { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_Emoji;

	/// <inheritdoc/>
	protected override string PrintingText => Emoji;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellEmojiMarkItem);
}

namespace Sudoku.Graphics.Items.CellPairTextMarks;

/// <summary>
/// Represents a cell pair raw text mark item.
/// </summary>
public sealed record CellPairRawTextMarkItem : CellPairTextMarkItem
{
	/// <summary>
	/// Indicates the text.
	/// </summary>
	public required string Text { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPairText_Raw;

	/// <inheritdoc/>
	protected override string PrintingText => Text;
}

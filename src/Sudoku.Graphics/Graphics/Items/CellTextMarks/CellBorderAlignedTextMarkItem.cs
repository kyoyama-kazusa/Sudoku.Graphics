namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents a cell border-aligned text mark item.
/// </summary>
public sealed record CellBorderAlignedTextMarkItem : CellTextMarkItem
{
	/// <summary>
	/// Indicates text.
	/// </summary>
	public required string Text { get; init; }

	/// <inheritdoc/>
	public required override Direction8 AlignedDirection { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellText_BorderAligned;

	/// <inheritdoc/>
	protected override string PrintingText => Text;
}

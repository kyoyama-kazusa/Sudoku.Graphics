namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents a cell border-aligned text mark item.
/// </summary>
public sealed class CellBorderAlignedTextMarkItem : CellTextMarkItem
{
	/// <summary>
	/// Indicates text.
	/// </summary>
	public required string Text { get; init; }

	/// <inheritdoc/>
	public override required Direction8 AlignedDirection { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_BorderAlignedText;

	/// <inheritdoc/>
	protected override string PrintingText => Text;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellBorderAlignedTextMarkItem);
}

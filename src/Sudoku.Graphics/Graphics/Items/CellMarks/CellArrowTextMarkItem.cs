namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents cell arrow text mark item.
/// </summary>
public sealed class CellArrowTextMarkItem : CellTextMarkItem
{
	/// <inheritdoc/>
	public new required Direction8 Direction { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_ArrowText;

	/// <inheritdoc/>
	protected override string PrintingText => Direction.ArrowString;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellArrowTextMarkItem);
}

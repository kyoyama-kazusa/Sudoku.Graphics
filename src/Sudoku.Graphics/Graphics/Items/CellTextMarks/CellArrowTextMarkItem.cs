namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents cell arrow text mark item.
/// </summary>
public sealed record CellArrowTextMarkItem : CellTextMarkItem
{
	/// <summary>
	/// Indicates the direction of arrow text.
	/// </summary>
	public required Direction8 Direction { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.Cell_ArrowText;

	/// <inheritdoc/>
	protected override string PrintingText => Direction.ArrowString;
}

namespace Sudoku.Graphics.Items.CellPairTextMarks;

/// <summary>
/// Represents a cell pair arrow text mark item.
/// </summary>
public sealed record CellPairArrowTextMarkItem : CellPairTextMarkItem
{
	/// <summary>
	/// Indicates the direction.
	/// </summary>
	public required Direction8 Direction { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPairText_Arrow;

	/// <inheritdoc/>
	protected override string PrintingText => Direction.ArrowString;
}

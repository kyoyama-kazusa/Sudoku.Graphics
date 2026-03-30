namespace Sudoku.Graphics.Items.CellPairTextMarks;

/// <summary>
/// Represents cell pair number text mark item.
/// </summary>
public sealed record CellPairNumberTextMarkItem : CellPairTextMarkItem
{
	/// <summary>
	/// Indicates the value.
	/// </summary>
	public required int Value { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPairText_Number;

	/// <inheritdoc/>
	protected override string PrintingText => Value.ToString();
}

namespace Sudoku.Graphics.Items.CellPairTextMarks;

/// <summary>
/// Represents a cell pair comparison operator text mark item.
/// </summary>
public sealed record CellPairComparisonOperatorTextMarkItem : CellPairTextMarkItem
{
	/// <summary>
	/// Indicates the comparison operator.
	/// </summary>
	public required ComparisonOperator Operator { get; init; }

	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellPairText_ComparisonOperator;

	/// <inheritdoc/>
	protected override string PrintingText => Operator.Text;
}

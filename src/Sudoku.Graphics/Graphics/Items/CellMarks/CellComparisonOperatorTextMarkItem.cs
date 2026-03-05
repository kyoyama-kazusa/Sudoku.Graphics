namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a comparison operator text mark item.
/// </summary>
public sealed class CellComparisonOperatorTextMarkItem : CellMathSymbolTextMarkItem<ComparisonOperator>
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_ComparisonOperator;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellComparisonOperatorTextMarkItem);
}

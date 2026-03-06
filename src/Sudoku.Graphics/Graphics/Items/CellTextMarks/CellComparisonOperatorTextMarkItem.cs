namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents a comparison operator text mark item.
/// </summary>
public sealed record CellComparisonOperatorTextMarkItem : CellMathSymbolTextMarkItem<ComparisonOperator>
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_ComparisonOperatorText;
}

namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents an arithmetic operator text mark item.
/// </summary>
public sealed class CellArithmeticOperatorTextMarkItem : CellMathSymbolTextMarkItem<ArithmeticOperator>
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_ArithmeticOperator;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellArithmeticOperatorTextMarkItem);
}

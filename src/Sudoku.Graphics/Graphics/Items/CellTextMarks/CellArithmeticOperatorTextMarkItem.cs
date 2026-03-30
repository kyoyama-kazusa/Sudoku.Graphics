namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents an arithmetic operator text mark item.
/// </summary>
public sealed record CellArithmeticOperatorTextMarkItem : CellMathSymbolTextMarkItem<ArithmeticOperator>
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellText_ArithmeticOperator;
}

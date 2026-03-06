namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents a bitwise operator text mark item.
/// </summary>
public sealed record CellBitwiseOperatorTextMarkItem : CellMathSymbolTextMarkItem<BitwiseOperator>
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_BitwiseOperatorText;
}

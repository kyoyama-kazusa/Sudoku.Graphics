namespace Sudoku.Graphics.Items.CellTextMarks;

/// <summary>
/// Represents a bitwise operator text mark item.
/// </summary>
public sealed class CellBitwiseOperatorTextMarkItem : CellMathSymbolTextMarkItem<BitwiseOperator>
{
	/// <inheritdoc/>
	public override ItemType Type => ItemType.CellMark_BitwiseOperatorText;

	/// <inheritdoc/>
	protected override Type EqualityContract => typeof(CellBitwiseOperatorTextMarkItem);
}

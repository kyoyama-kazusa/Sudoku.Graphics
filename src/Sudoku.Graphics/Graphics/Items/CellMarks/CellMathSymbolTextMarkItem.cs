namespace Sudoku.Graphics.Items.CellMarks;

/// <summary>
/// Represents a text mark item in cell, for a math symbol.
/// </summary>
/// <typeparam name="TOperator">The type of operator symbol enumeration.</typeparam>
public abstract class CellMathSymbolTextMarkItem<TOperator> : CellTextMarkItem where TOperator : unmanaged, Enum
{
	/// <summary>
	/// Indicates the operator.
	/// </summary>
	public required TOperator Operator { get; init; }

	/// <inheritdoc/>
	protected sealed override string PrintingText => Operator.Text;
}

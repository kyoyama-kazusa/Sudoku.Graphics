namespace Sudoku.Graphics.Semantic;

/// <summary>
/// Provides extra information of event <see cref="SudokuGrid.DigitRemoved"/>.
/// </summary>
/// <param name="type">The type of digits being added.</param>
/// <param name="cell">The cell.</param>
/// <seealso cref="SudokuGrid.DigitRemoved"/>
public sealed class SudokuGridDigitRemovedEventArgs(DigitType type, Absolute cell) : SudokuGridRelatedEventArgs
{
	/// <summary>
	/// Indicates the cell type.
	/// </summary>
	public DigitType Type { get; } = type;

	/// <summary>
	/// Indicates the cell.
	/// </summary>
	public Absolute Cell { get; } = cell;
}

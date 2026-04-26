namespace Sudoku.Graphics.Semantic;

/// <summary>
/// Provides data of event <see cref="SudokuGrid.CellCleared"/>.
/// </summary>
/// <param name="cell">The cell.</param>
/// <seealso cref="SudokuGrid.CellCleared"/>
public sealed class SudokuGridCellRefreshedEventArgs(Absolute cell) : SudokuGridRelatedEventArgs
{
	/// <summary>
	/// Indicates the cell conflict.
	/// </summary>
	public Absolute Cell { get; } = cell;
}

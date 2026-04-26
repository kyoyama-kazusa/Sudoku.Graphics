namespace Sudoku.Graphics.Semantic;

/// <summary>
/// Provides extra information of event <see cref="SudokuGrid.DigitRemoving"/>.
/// </summary>
/// <param name="type">The type of digits being added.</param>
/// <param name="cell">The cell.</param>
/// <seealso cref="SudokuGrid.DigitRemoving"/>
public sealed class SudokuGridDigitRemovingEventArgs(DigitType type, Absolute cell) : SudokuGridRelatedEventArgs
{
	/// <summary>
	/// Indicates whether the operation should be prevented.
	/// </summary>
	public bool Handled { get; set; }

	/// <summary>
	/// Indicates the cell type.
	/// </summary>
	public DigitType Type { get; } = type;

	/// <summary>
	/// Indicates the cell.
	/// </summary>
	public Absolute Cell { get; } = cell;
}

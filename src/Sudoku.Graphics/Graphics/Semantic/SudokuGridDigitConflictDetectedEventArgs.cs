namespace Sudoku.Graphics.Semantic;

/// <summary>
/// Provides data of event <see cref="SudokuGrid.DigitConflictDetected"/>.
/// </summary>
/// <param name="type">The type of cell.</param>
/// <param name="cell">The cell.</param>
/// <param name="originalDigits">The original digits.</param>
/// <param name="newDigits">The new digits.</param>
/// <seealso cref="SudokuGrid.DigitConflictDetected"/>
public sealed class SudokuGridDigitConflictDetectedEventArgs(DigitType type, Absolute cell, int[] originalDigits, int[] newDigits) :
	SudokuGridRelatedEventArgs
{
	/// <summary>
	/// Indicates whether the operation should be prevented.
	/// </summary>
	public bool Handled { get; set; }

	/// <summary>
	/// Indicates the original digit filled in this cell.
	/// </summary>
	public int[] OriginalDigits { get; } = originalDigits;

	/// <summary>
	/// Indicates the new digit filled in this cell.
	/// </summary>
	public int[] NewDigits { get; } = newDigits;

	/// <summary>
	/// Indicates the conflict digit type (which type of the cell currently is).
	/// </summary>
	public DigitType Type { get; } = type;

	/// <summary>
	/// Indicates the cell conflict.
	/// </summary>
	public Absolute Cell { get; } = cell;
}

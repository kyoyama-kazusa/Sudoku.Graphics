namespace Sudoku.Graphics.Semantic;

/// <summary>
/// Provides extra information of event <see cref="SudokuGrid.DigitsAdding"/>.
/// </summary>
/// <param name="type">The type of digits being added.</param>
/// <param name="cell">The cell.</param>
/// <param name="digits">The digits being added.</param>
/// <seealso cref="SudokuGrid.DigitsAdding"/>
public sealed class SudokuGridDigitAddingEventArgs(DigitType type, Absolute cell, int[] digits) : SudokuGridRelatedEventArgs
{
	/// <summary>
	/// Indicates whether the operation should be prevented here or not.
	/// </summary>
	public bool Handled { get; set; }

	/// <summary>
	/// Indicates the digits added.
	/// </summary>
	public int[] Digits { get; } = digits;

	/// <summary>
	/// Indicates the type of the digits added.
	/// </summary>
	public DigitType Type { get; } = type;

	/// <summary>
	/// Indicates the cell.
	/// </summary>
	public Absolute Cell { get; } = cell;
}

namespace Sudoku.Graphics.Semantic;

/// <summary>
/// Provides data of event <see cref="SudokuGrid.DigitsAdded"/>.
/// </summary>
/// <param name="type">The type of digits added.</param>
/// <param name="cell">The cell.</param>
/// <param name="digits">The digits added.</param>
/// <seealso cref="SudokuGrid.DigitsAdded"/>
public sealed class SudokuGridDigitAddedEventArgs(DigitType type, Absolute cell, int[] digits) : SudokuGridRelatedEventArgs
{
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

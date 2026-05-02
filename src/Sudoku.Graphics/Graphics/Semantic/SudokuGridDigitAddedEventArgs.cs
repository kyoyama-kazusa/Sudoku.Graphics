namespace Sudoku.Graphics.Semantic;

/// <summary>
/// Provides data of event <see cref="SudokuGrid.DigitAdded"/>.
/// </summary>
/// <typeparam name="TLocator">The type of locator.</typeparam>
/// <param name="type">The type of digits added.</param>
/// <param name="locator">The cell.</param>
/// <param name="digit">The digit added.</param>
/// <seealso cref="SudokuGrid.DigitAdded"/>
public abstract class SudokuGridDigitAddedEventArgs<TLocator>(DigitType type, TLocator locator, int digit) : SudokuGridRelatedEventArgs
	where TLocator : ILocator<TLocator>
{
	/// <summary>
	/// Indicates the digit added.
	/// </summary>
	public int Digit { get; } = digit;

	/// <summary>
	/// Indicates the type of the digits added.
	/// </summary>
	public DigitType Type { get; } = type;

	/// <summary>
	/// Indicates the locator.
	/// </summary>
	public TLocator Locator { get; } = locator;
}

/// <summary>
/// Provides data of event <see cref="SudokuGrid.DigitAdded"/>.
/// </summary>
/// <param name="type">The type of digits added.</param>
/// <param name="locator">The cell.</param>
/// <param name="digit">The digit added.</param>
/// <seealso cref="SudokuGrid.DigitAdded"/>
public sealed class SudokuGridDigitAddedEventArgs(DigitType type, Absolute locator, int digit) :
	SudokuGridDigitAddedEventArgs<Absolute>(type, locator, digit);

/// <summary>
/// Provides data of event <see cref="SudokuGrid.CandidateAdded"/>.
/// </summary>
/// <param name="candidate">The candidate added.</param>
/// <seealso cref="SudokuGrid.CandidateAdded"/>
public sealed class SudokuGridCandidateAddedEventArgs(CandidatePosition candidate) :
	SudokuGridDigitAddedEventArgs<CandidatePosition>(DigitType.Candidate, candidate, candidate.InnerIndex);

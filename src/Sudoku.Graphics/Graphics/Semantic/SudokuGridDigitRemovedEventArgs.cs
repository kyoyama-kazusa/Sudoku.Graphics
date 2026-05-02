namespace Sudoku.Graphics.Semantic;

/// <summary>
/// Provides extra information of event <see cref="SudokuGrid.DigitRemoved"/>.
/// </summary>
/// <typeparam name="TLocator">The type of locator.</typeparam>
/// <param name="type">The type of digits being added.</param>
/// <param name="locator">The cell.</param>
/// <seealso cref="SudokuGrid.DigitRemoved"/>
public abstract class SudokuGridDigitRemovedEventArgs<TLocator>(DigitType type, TLocator locator) : SudokuGridRelatedEventArgs
	where TLocator : ILocator<TLocator>
{
	/// <summary>
	/// Indicates the cell type.
	/// </summary>
	public DigitType Type { get; } = type;

	/// <summary>
	/// Indicates the locator (cell or candidate).
	/// </summary>
	public TLocator Locator { get; } = locator;
}

/// <summary>
/// Provides extra information of event <see cref="SudokuGrid.DigitRemoved"/>.
/// </summary>
/// <param name="type">The type of digits being added.</param>
/// <param name="cell">The cell.</param>
/// <seealso cref="SudokuGrid.DigitRemoved"/>
public sealed class SudokuGridDigitRemovedEventArgs(DigitType type, Absolute cell) :
	SudokuGridDigitRemovedEventArgs<Absolute>(type, cell);

/// <summary>
/// Provides extra information of event <see cref="SudokuGrid.CandidateRemoved"/>.
/// </summary>
/// <param name="candidate">The candidate.</param>
/// <seealso cref="SudokuGrid.CandidateRemoved"/>
public sealed class SudokuGridCandidateRemovedEventArgs(CandidatePosition candidate) :
	SudokuGridDigitRemovedEventArgs<CandidatePosition>(DigitType.Candidate, candidate);

namespace Sudoku.ComponentModel.Crossmath;

/// <summary>
/// Represents a crossmath formula.
/// </summary>
public sealed record CrossmathFormula : IEqualityOperators<CrossmathFormula, CrossmathFormula, bool>
{
	/// <summary>
	/// Indicates the cell as start.
	/// </summary>
	public required Absolute Cell { get; init; }

	/// <summary>
	/// Indicates the number of cells occupied.
	/// </summary>
	public required Relative CellsCount { get; init; }

	/// <summary>
	/// Indicates expanding direction of formula.
	/// </summary>
	public required Direction ExpandingDirection { get; init; }
}

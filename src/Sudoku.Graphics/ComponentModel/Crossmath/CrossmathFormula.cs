namespace Sudoku.ComponentModel.Crossmath;

/// <summary>
/// Represents a crossmath formula.
/// </summary>
public sealed record CrossmathFormula : IEqualityOperators<CrossmathFormula, CrossmathFormula, bool>
{
	/// <summary>
	/// Indicates the value sequence that will be filled into the cells.
	/// </summary>
	public required string?[] ValueSequence { get; init; }

	/// <summary>
	/// Indicates the cell as start.
	/// </summary>
	public required Absolute Cell { get; init; }

	/// <summary>
	/// Indicates the number of cells occupied.
	/// </summary>
	public Relative CellsCount => ValueSequence.Length;

	/// <summary>
	/// Indicates expanding direction of formula.
	/// </summary>
	public required Direction ExpandingDirection { get; init; }


	/// <inheritdoc/>
	public override string ToString()
	{
		var formulaString = string.Concat(from value in ValueSequence select value ?? "?");
		return $"@{Cell}, {formulaString}, {ExpandingDirection}";
	}
}

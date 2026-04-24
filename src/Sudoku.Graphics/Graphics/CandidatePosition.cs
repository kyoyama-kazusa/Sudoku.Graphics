namespace Sudoku.Graphics;

/// <summary>
/// Represents a candidate position.
/// </summary>
/// <param name="Cell">Indicates the target cell.</param>
/// <param name="SubgridSize">Indicates subgrid size.</param>
/// <param name="InnerIndex">The internal absolute position index of the candidate.</param>
/// <remarks>
/// For more information, please visit <see cref="ILocator{TSelf}.GetCandidatesCountInEachRow"/> method.
/// </remarks>
/// <seealso cref="ILocator{TSelf}.GetCandidatesCountInEachRow"/>
public readonly record struct CandidatePosition(Absolute Cell, Relative SubgridSize, Absolute InnerIndex) :
	IEqualityOperators<CandidatePosition, CandidatePosition, bool>,
	ILocator<CandidatePosition>
{
	/// <inheritdoc/>
	public bool IsSideWith(CandidatePosition other, Direction4 direction, PointMapper mapper, bool isInStrictDirection)
		=> Cell.IsSideWith(other.Cell, direction, mapper, isInStrictDirection);

	/// <inheritdoc/>
	public float GetLocatorMeasurer(float cellSize) => cellSize / SubgridSize;

	private bool PrintMembers(StringBuilder builder)
	{
		builder.Append($"Position = {Cell}@({SubgridSize}x{SubgridSize}, {InnerIndex})");
		return true;
	}


	/// <inheritdoc/>
	public static bool IsAlignedAs(LocatorGridAlignment gridAlignment, CandidatePosition first, CandidatePosition second, PointMapper mapper)
		=> Absolute.IsAlignedAs(gridAlignment, first.Cell, second.Cell, mapper);


	/// <inheritdoc/>
	public Relative GetCandidatesCountInEachRow() => SubgridSize;
}

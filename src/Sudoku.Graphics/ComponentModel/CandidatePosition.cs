namespace Sudoku.ComponentModel;

/// <summary>
/// Represents a candidate position.
/// </summary>
/// <param name="Cell">Indicates the target cell.</param>
/// <param name="SubgridSize">Indicates subgrid size.</param>
/// <param name="InnerIndex">The internal absolute position index of the candidate.</param>
/// <remarks>
/// <para>
/// By design of candidate drawing system, we will split a cell into a square subgrid of size <i>n</i> by <i>n</i>,
/// where <i>n</i> is equal to value of property <see cref="SubgridSize"/>.
/// </para>
/// <para>
/// Then, we define an absolute internal index to describe a cell will be drawn, which is in range [0, <i>n</i> * <i>n</i>) -
/// property <see cref="InnerIndex"/>.
/// </para>
/// </remarks>
public readonly record struct CandidatePosition(Absolute Cell, Relative SubgridSize, Absolute InnerIndex) :
	IEqualityOperators<CandidatePosition, CandidatePosition, bool>
{
	private bool PrintMembers(StringBuilder builder)
	{
		builder.Append($"Position = {Cell}@({SubgridSize}x{SubgridSize}, {InnerIndex})");
		return true;
	}
}

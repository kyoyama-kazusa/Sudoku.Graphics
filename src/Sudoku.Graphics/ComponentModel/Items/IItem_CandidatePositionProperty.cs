namespace Sudoku.ComponentModel.Items;

/// <summary>
/// Represents a type that includes <see cref="CandidatePosition"/> property.
/// </summary>
public interface IItem_CandidatePositionProperty
{
	/// <summary>
	/// Indicates candidate position to be set.
	/// </summary>
	CandidatePosition CandidatePosition { get; init; }
}

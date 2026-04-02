namespace Sudoku.Graphics;

/// <summary>
/// Represents a case that a locator instance (<see cref="Absolute"/>, <see cref="Relative"/> or <see cref="CandidatePosition"/>)
/// should be aligned.
/// </summary>
/// <seealso cref="Absolute"/>
/// <seealso cref="Relative"/>
/// <seealso cref="CandidatePosition"/>
public enum LocatorGridAlignment
{
	/// <summary>
	/// Represents the placeholder of this type.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates the first row.
	/// </summary>
	FirstRow,

	/// <summary>
	/// Indicates the last row.
	/// </summary>
	LastRow,

	/// <summary>
	/// Indicates the first column.
	/// </summary>
	FirstColumn,

	/// <summary>
	/// Indicates the last column.
	/// </summary>
	LastColumn
}

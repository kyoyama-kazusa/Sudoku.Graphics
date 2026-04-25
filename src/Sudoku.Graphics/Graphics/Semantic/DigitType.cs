namespace Sudoku.Graphics.Semantic;

/// <summary>
/// Represents a type of a digit.
/// </summary>
public enum DigitType
{
	/// <summary>
	/// Indicates unknown type.
	/// </summary>
	Unknown = 0,

	/// <summary>
	/// Indicates the type is a given digit.
	/// </summary>
	Given,

	/// <summary>
	/// Indicates the type is a modifiable digit.
	/// </summary>
	Modifiable,

	/// <summary>
	/// Indicates the type is a candidate digit.
	/// </summary>
	Candidate
}

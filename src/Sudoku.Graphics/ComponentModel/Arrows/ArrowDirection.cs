namespace Sudoku.ComponentModel.Arrows;

/// <summary>
/// Represents a direction of arrow.
/// </summary>
public enum ArrowDirection
{
	/// <summary>
	/// Indicates placeholder of this type.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates north direction (up).
	/// </summary>
	N,

	/// <summary>
	/// Indicates north-east direction (right-up).
	/// </summary>
	NE,

	/// <summary>
	/// Indicates east direction (right).
	/// </summary>
	E,

	/// <summary>
	/// Indicates south-east direction (right-down).
	/// </summary>
	SE,

	/// <summary>
	/// Indicates south direction (down).
	/// </summary>
	S,

	/// <summary>
	/// Indicates south-west direction (left-down).
	/// </summary>
	SW,

	/// <summary>
	/// Indicates west direction (left).
	/// </summary>
	W,

	/// <summary>
	/// Indicates north west direction (left-up).
	/// </summary>
	NW
}

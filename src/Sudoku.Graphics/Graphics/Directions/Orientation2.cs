namespace Sudoku.Graphics.Directions;

/// <summary>
/// Represents an orientation, with 2 values.
/// </summary>
[Flags]
public enum Orientation2
{
	/// <summary>
	/// Represents placeholder of this type.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates horizontal.
	/// </summary>
	Horizontal = 1 << 0,

	/// <summary>
	/// Indicates vertical.
	/// </summary>
	Vertical = 1 << 1
}

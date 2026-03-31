namespace Sudoku.Graphics.Directions;

/// <summary>
/// Represents an orientation, with 4 values.
/// </summary>
[Flags]
public enum Orientation4
{
	/// <summary>
	/// Represents placeholder of this type.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates horizontal (-).
	/// </summary>
	Horizontal,

	/// <summary>
	/// Indicates vertical (|).
	/// </summary>
	Vertical,

	/// <summary>
	/// Indicates slash (/).
	/// </summary>
	Slash,

	/// <summary>
	/// Indicates backslash (\).
	/// </summary>
	Backslash
}

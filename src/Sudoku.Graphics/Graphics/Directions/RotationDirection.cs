namespace Sudoku.Graphics.Directions;

/// <summary>
/// Represents a rotation direction.
/// </summary>
public enum RotationDirection : byte
{
	/// <summary>
	/// Indicates placeholder of this type.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates rotation direction is clockwise.
	/// </summary>
	Clockwise,

	/// <summary>
	/// Indicates rotation direction is counterclockwise.
	/// </summary>
	Counterclockwise
}

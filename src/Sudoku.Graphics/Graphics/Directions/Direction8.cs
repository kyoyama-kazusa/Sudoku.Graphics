namespace Sudoku.Graphics.Directions;

/// <summary>
/// Represents a direction with 8 values.
/// </summary>
[Flags]
public enum Direction8
{
	/// <summary>
	/// Indicates the placeholder of this type.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates up direction.
	/// </summary>
	Up = 1 << 0,

	/// <summary>
	/// Indicates down direction.
	/// </summary>
	Down = 1 << 1,

	/// <summary>
	/// Indicates left direction.
	/// </summary>
	Left = 1 << 2,

	/// <summary>
	/// Indicates right direction.
	/// </summary>
	Right = 1 << 3,

	/// <summary>
	/// Indicates left up direction.
	/// </summary>
	LeftUp = 1 << 4,

	/// <summary>
	/// Indicates right up direction.
	/// </summary>
	RightUp = 1 << 5,

	/// <summary>
	/// Indicates left down direction.
	/// </summary>
	LeftDown = 1 << 6,

	/// <summary>
	/// Indicates right down direction.
	/// </summary>
	RightDown = 1 << 7
}

namespace Sudoku.Graphics;

/// <summary>
/// Represents a direction.
/// </summary>
public enum Direction : byte
{
	/// <summary>
	/// Indicates the placeholder of this type.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates up direction.
	/// </summary>
	Up,

	/// <summary>
	/// Indicates down direction.
	/// </summary>
	Down,

	/// <summary>
	/// Indicates left direction.
	/// </summary>
	Left,

	/// <summary>
	/// Indicates right direction.
	/// </summary>
	Right
}
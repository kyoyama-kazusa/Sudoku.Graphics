namespace Sudoku.Graphics;

/// <summary>
/// Represents a direction.
/// </summary>
[Flags]
public enum Direction : byte
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
	Right = 1 << 3
}

namespace Sudoku.ComponentModel;

/// <summary>
/// Represents an alignment type.
/// </summary>
public enum Alignment : byte
{
	/// <summary>
	/// Indicates the placeholder of this type.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates center point of container.
	/// </summary>
	Center,

	/// <summary>
	/// Indicates top-left point of container.
	/// </summary>
	TopLeft,

	/// <summary>
	/// Indicates top-right point of container.
	/// </summary>
	TopRight,

	/// <summary>
	/// Indicates bottom-left point of container.
	/// </summary>
	BottomLeft,

	/// <summary>
	/// Indicates bottom-right point of container.
	/// </summary>
	BottomRight
}

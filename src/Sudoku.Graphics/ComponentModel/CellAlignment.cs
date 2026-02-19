namespace Sudoku.ComponentModel;

/// <summary>
/// Represents a cell alignment type.
/// </summary>
public enum CellAlignment : byte
{
	/// <summary>
	/// Indicates the placeholder of this type.
	/// </summary>
	None = 0,

	/// <summary>
	/// Indicates center point of the cell.
	/// </summary>
	Center,

	/// <summary>
	/// Indicates top-left point of the cell.
	/// </summary>
	TopLeft,

	/// <summary>
	/// Indicates top-right point of the cell.
	/// </summary>
	TopRight,

	/// <summary>
	/// Indicates bottom-left point of the cell.
	/// </summary>
	BottomLeft,

	/// <summary>
	/// Indicates bottom-right point of the cell.
	/// </summary>
	BottomRight
}

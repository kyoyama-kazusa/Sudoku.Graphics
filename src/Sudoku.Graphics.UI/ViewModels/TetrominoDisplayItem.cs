namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a tetromino display item.
/// </summary>
public sealed class TetrominoDisplayItem
{
	/// <summary>
	/// Indicates the item type.
	/// </summary>
	public Tetromino Type { get; set; }

	/// <summary>
	/// Indicates rotation type.
	/// </summary>
	public TetrominoRotationType RotationType { get; set; }

	/// <summary>
	/// Indicates the icon to be diplayed.
	/// </summary>
	public ImageSource? Icon { get; set; }
}

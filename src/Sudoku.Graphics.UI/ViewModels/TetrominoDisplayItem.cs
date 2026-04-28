namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a tetromino display item.
/// </summary>
public sealed class TetrominoDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates the item type.
	/// </summary>
	public Tetromino Type { get; set; }

	/// <summary>
	/// Indicates rotation type.
	/// </summary>
	public TetrominoRotationType RotationType { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

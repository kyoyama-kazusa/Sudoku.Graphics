namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a tetromino display item.
/// </summary>
public sealed partial class TetrominoDisplayItem : ObservableObject
{
	/// <summary>
	/// Indicates the item type.
	/// </summary>
	[ObservableProperty]
	public partial Tetromino Type { get; set; }

	/// <summary>
	/// Indicates rotation type.
	/// </summary>
	[ObservableProperty]
	public partial TetrominoRotationType RotationType { get; set; }

	/// <summary>
	/// Indicates the icon to be diplayed.
	/// </summary>
	[ObservableProperty]
	public partial ImageSource? Icon { get; set; }
}

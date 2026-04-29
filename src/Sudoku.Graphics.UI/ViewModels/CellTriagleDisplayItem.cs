namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents direction display item.
/// </summary>
public sealed class CellTriagleDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates the direction.
	/// </summary>
	public Direction8 Direction { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

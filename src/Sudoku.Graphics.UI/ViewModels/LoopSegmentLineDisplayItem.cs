namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents loop segment line display item.
/// </summary>
public sealed class LoopSegmentLineDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates the direction.
	/// </summary>
	public Direction4 Direction { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

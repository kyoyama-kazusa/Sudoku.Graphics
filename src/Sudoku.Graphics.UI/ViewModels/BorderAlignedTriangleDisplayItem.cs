namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents border-aligned triangle display item.
/// </summary>
public sealed class BorderAlignedTriangleDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates the aligned direction.
	/// </summary>
	public Direction4 Direction { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

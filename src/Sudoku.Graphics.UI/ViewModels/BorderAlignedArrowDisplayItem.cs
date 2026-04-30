namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents border-aligned arrow display item.
/// </summary>
public sealed class BorderAlignedArrowDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates the aligned direction.
	/// </summary>
	public Direction4 Direction { get; set; }

	/// <summary>
	/// Indicates rotation direction.
	/// </summary>
	public RotationDirection RotationDirection { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

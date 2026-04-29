namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents cell apex corner triangle display item.
/// </summary>
public sealed class CellApexCornerTriangleDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates alignment.
	/// </summary>
	public Alignment Alignment { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

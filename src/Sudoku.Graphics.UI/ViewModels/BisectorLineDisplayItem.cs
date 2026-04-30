namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents bisector line display item.
/// </summary>
public sealed class BisectorLineDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates orientation.
	/// </summary>
	public Orientation4 Orientation { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

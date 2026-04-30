namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents hexagon display item.
/// </summary>
public sealed class HexagonDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates orientation.
	/// </summary>
	public Orientation2 Orientation { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

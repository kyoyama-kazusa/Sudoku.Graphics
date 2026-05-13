namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents connection line display item.
/// </summary>
public sealed class ConnectionLineDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates orientation.
	/// </summary>
	public Orientation4 Orientation { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

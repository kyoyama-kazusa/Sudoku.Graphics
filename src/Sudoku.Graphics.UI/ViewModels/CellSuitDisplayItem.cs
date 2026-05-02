namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents cell suit display item.
/// </summary>
public sealed class CellSuitDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates the suit.
	/// </summary>
	public Suit Suit { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

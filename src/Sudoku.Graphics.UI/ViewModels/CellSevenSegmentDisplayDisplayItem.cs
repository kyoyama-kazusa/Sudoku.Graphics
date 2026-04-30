namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents seven-segment display display item.
/// </summary>
public sealed class CellSevenSegmentDisplayDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates the value.
	/// </summary>
	public int Value { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

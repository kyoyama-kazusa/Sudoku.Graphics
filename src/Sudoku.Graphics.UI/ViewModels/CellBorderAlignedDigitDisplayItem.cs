namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents cell border-aligned digit display item.
/// </summary>
public sealed class CellBorderAlignedDigitDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates the digit.
	/// </summary>
	public int Digit { get; set; }

	/// <summary>
	/// Indicates the alignment.
	/// </summary>
	public Alignment Alignment { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

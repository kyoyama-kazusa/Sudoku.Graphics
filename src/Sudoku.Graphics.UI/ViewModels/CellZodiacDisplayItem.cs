namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents cell zodiac display item.
/// </summary>
public sealed class CellZodiacDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates zodiac.
	/// </summary>
	public Zodiac Zodiac { get; set; }

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

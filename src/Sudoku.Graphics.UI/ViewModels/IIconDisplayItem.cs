namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a display item that binds with an icon.
/// </summary>
public interface IIconDisplayItem
{
	/// <summary>
	/// Indicates the icon.
	/// </summary>
	ImageSource? Icon { get; set; }
}

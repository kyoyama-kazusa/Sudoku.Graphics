namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a display item that binds with an icon.
/// </summary>
public interface IIconDisplayItem : IDisplayItem
{
	/// <summary>
	/// Indicates the icon.
	/// </summary>
	ImageSource? Icon { get; set; }

	/// <inheritdoc/>
	[NotNullIfNotNull(nameof(Icon))]
	object? IDisplayItem.ValueToDisplay => Icon;
}

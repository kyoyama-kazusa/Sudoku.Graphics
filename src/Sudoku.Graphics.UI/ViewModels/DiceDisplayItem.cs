namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Represents a dice display item.
/// </summary>
public sealed class DiceDisplayItem : IIconDisplayItem
{
	/// <summary>
	/// Indicates the value.
	/// </summary>
	public int Value { get; set; }

	/// <summary>
	/// Indicates display string of value.
	/// </summary>
	public string ValueString => (Value + 1).ToString();

	/// <inheritdoc/>
	public ImageSource? Icon { get; set; }
}

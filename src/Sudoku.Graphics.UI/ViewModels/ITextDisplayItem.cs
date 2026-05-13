namespace Sudoku.Graphics.UI.ViewModels;

/// <summary>
/// Indicates text display item.
/// </summary>
public interface ITextDisplayItem : IDisplayItem
{
	/// <summary>
	/// Indicates the text.
	/// </summary>
	string Text { get; }

	/// <inheritdoc/>
	object IDisplayItem.ValueToDisplay => Text;
}

namespace Sudoku.Graphics.UI.Views.Controls;

/// <summary>
/// Event args for <see cref="ItemSelectorPanel.SelectedItemChanged"/>.
/// </summary>
/// <param name="selectedItem">The selected item.</param>
/// <param name="context">The context.</param>
public sealed class ItemSelectorPanelSelectedItemChangedEventArgs(object? selectedItem, OperationHandlerContext context) :
	ContextBasedEventArgs(context)
{
	/// <summary>
	/// Indicates the selected item.
	/// </summary>
	public object? SelectedItem { get; } = selectedItem;
}

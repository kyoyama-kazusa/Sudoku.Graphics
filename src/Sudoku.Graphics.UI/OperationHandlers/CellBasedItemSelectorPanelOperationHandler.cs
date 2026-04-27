namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces instances that can be selected using <see cref="ItemSelectorPanel"/>.
/// </summary>
/// <seealso cref="ItemSelectorPanel"/>
public abstract class CellBasedItemSelectorPanelOperationHandler : OperationHandler
{
	/// <summary>
	/// Indicates the item type supported.
	/// </summary>
	public abstract ItemType ItemType { get; }

	/// <summary>
	/// Represents a method that selects the target <see cref="ItemSelectorPanel"/> defined in the specified window.
	/// </summary>
	public abstract Func<MainWindow, ItemSelectorPanel> PanelSelector { get; }

	/// <summary>
	/// Represents a method that selects the target <see cref="Popup"/> defined in the specified window.
	/// </summary>
	public abstract Func<MainWindow, Popup> PopupSelector { get; }

	/// <summary>
	/// Represents a method that produces an item to add, or <see langword="null"/> if invalid.
	/// </summary>
	public abstract Func<object?, Absolute, Item?> ItemFactory { get; }

	/// <summary>
	/// Indicates the default changed button to be checked.
	/// </summary>
	public virtual MouseButton ChangedButton => MouseButton.Right;


	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonPressed(OperationHandlerContext context)
	{
		var panel = PanelSelector(context.OwnerWindow);
		panel.OperationHandlerContext = context;
		panel.SelectedItemChanged += Panel_SelectedItemChanged;
	}

	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonReleased(OperationHandlerContext context)
		=> PopupSelector(context.OwnerWindow).IsOpen = true;

	/// <inheritdoc/>
	protected internal sealed override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == ChangedButton;

	private void Panel_SelectedItemChanged(ItemSelectorPanel sender, ItemSelectorPanelSelectedItemChangedEventArgs e)
	{
		if (e.Context is not { OwnerWindow: var window } context)
		{
			return;
		}

		var popup = PopupSelector(window);
		var panel = PanelSelector(window);
		var selectedItem = e.SelectedItem;

		popup.IsOpen = false;

		var cell = context.GetCell();
		var item = ItemFactory(selectedItem, cell);
		UpdateItems(
			window,
			items =>
			{
				if (item is null)
				{
					items.Clear(cell, ItemType);
				}
				else
				{
					items.Add(item);
				}
			}
		);

		sender.SelectedItemChanged -= Panel_SelectedItemChanged;
		panel.OperationHandlerContext = null;
	}
}

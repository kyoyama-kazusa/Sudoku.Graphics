namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents a cell pair based item selector panel operation handler.
/// </summary>
public abstract class CellPairBasedItemSelectorPanelOperationHandler : OperationHandler
{
	/// <summary>
	/// Indicates the changed button.
	/// </summary>
	public virtual MouseButton ChangedButton => MouseButton.Right;

	/// <summary>
	/// Indicates the supported item type.
	/// </summary>
	public abstract ItemType ItemType { get; }

	/// <summary>
	/// Indicates display item factory.
	/// </summary>
	public abstract ReadOnlySpan<Func<ITextDisplayItem>> DisplayItemFactory { get; }

	/// <summary>
	/// Indicates the method that selects the item selector panel.
	/// </summary>
	public abstract Func<MainWindow, ItemSelectorPanel> PanelSelector { get; }

	/// <summary>
	/// Indicates the method that select popup control.
	/// </summary>
	public abstract Func<MainWindow, Popup> PopupSelector { get; }

	/// <summary>
	/// Indicates item factory.
	/// </summary>
	public abstract Func<ITextDisplayItem?, Absolute, Absolute, Item?> ItemFactory { get; }


	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonPressed(OperationHandlerContext context)
	{
		var panel = PanelSelector(context.OwnerWindow);
		panel.OperationHandlerContext = context;
		panel.SelectedItemChanged += Panel_SelectedItemChanged;

		panel.ItemsSource = (from factory in DisplayItemFactory select factory()).ToArray();
	}

	/// <inheritdoc/>
	protected internal sealed override void OnMouseButtonReleased(OperationHandlerContext context)
		=> PopupSelector(context.OwnerWindow).IsOpen = true;

	/// <inheritdoc/>
	protected internal sealed override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == ChangedButton;

	private void Panel_SelectedItemChanged(ItemSelectorPanel sender, ItemSelectorPanelSelectedItemChangedEventArgs e)
	{
		if (e.Context is not { OwnerWindow: { CurrentCanvas.Mapper: var mapper } window } context)
		{
			return;
		}

		var popup = PopupSelector(window);
		var panel = PanelSelector(window);
		var selectedItem = (ITextDisplayItem)e.SelectedItem!;

		popup.IsOpen = false;

		var (cell1, cell2) = context.GetBorder();
		var cell1Row = cell1 / mapper.AbsoluteColumnsCount;
		var cell1Column = cell1 % mapper.AbsoluteColumnsCount;
		if (cell1 == -1 || cell2 == -1)
		{
			return;
		}

		var item = ItemFactory(selectedItem, cell1, cell2);
		UpdateItems(
			window,
			items =>
			{
				if (item is null)
				{
					items.Clear(cell1, cell2, ItemType);
					return;
				}

				var found = items.Find(cell1, cell2, ItemType);
				if (found.Length != 0)
				{
					items.RemoveRange(found);
				}
				items.Add(item);
			}
		);

		sender.SelectedItemChanged -= Panel_SelectedItemChanged;
		panel.OperationHandlerContext = null;
	}
}

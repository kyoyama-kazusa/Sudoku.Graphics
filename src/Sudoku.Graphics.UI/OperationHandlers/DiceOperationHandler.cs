namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellDiceMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellDiceMarkItem"/>
[OperationHandler(ItemType.Cell_Dice)]
public sealed class DiceOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
		var panel = context.OwnerWindow.DiceSelectorPanel;
		panel.OperationHandlerContext = context;
		panel.SelectedItemChanged += Panel_SelectedItemChanged;
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		if (context.OwnerWindow is { DiceSelectorPopup: var popup })
		{
			popup.IsOpen = true;
		}
	}

	/// <inheritdoc/>
	protected internal override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == MouseButton.Right;

	private void Panel_SelectedItemChanged(ItemSelectorPanel sender, ItemSelectorPanelSelectedItemChangedEventArgs e)
	{
		if (e is not
			{
				SelectedItem: var selectedItem and (DiceDisplayItem or null),
				Context: { OwnerWindow: { DiceSelectorPopup: var popup, DiceSelectorPanel: var panel } window } context
			})
		{
			return;
		}

		popup.IsOpen = false;

		var cell = context.GetCell();
		var item = selectedItem switch
		{
			DiceDisplayItem { Value: var value } => ItemsFactory.Dice(cell, value),
			null => null,
			_ => throw new UnreachableException()
		};
		UpdateItems(
			window,
			items =>
			{
				if (item is null)
				{
					items.Clear(cell, ItemType.Cell_Dice);
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

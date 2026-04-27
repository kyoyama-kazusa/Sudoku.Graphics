namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellSurroundingTrianglesMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellSurroundingTrianglesMarkItem"/>
[OperationHandler(ItemType.Cell_SurroundingTriangles)]
public sealed class SurroundingTrianglesOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
		var panel = context.OwnerWindow.SurroundingTrianglesPanel;
		panel.OperationHandlerContext = context;
		panel.SelectedItemChanged += Panel_SelectedItemChanged;
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		if (context.OwnerWindow is { SurroundingTrianglesPopup: var popup })
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
				SelectedItem: var selectedItem and (SurroundingTrianglesDisplayItem or null),
				Context: { OwnerWindow: { SurroundingTrianglesPopup: var popup, SurroundingTrianglesPanel: var panel } window } context
			})
		{
			return;
		}

		popup.IsOpen = false;

		var cell = context.GetCell();
		var item = selectedItem switch
		{
			SurroundingTrianglesDisplayItem { Value: var value } => ItemsFactory.SurroundingTriangles(cell, value),
			null => null,
			_ => throw new UnreachableException()
		};
		UpdateItems(
			window,
			items =>
			{
				if (item is null)
				{
					items.Clear(cell, ItemType.Cell_SurroundingTriangles);
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

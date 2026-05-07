namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellPairNumberTextMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellPairNumberTextMarkItem"/>
[OperationHandler(ItemType.CellPairText_Number)]
public sealed class CellPairNumberOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		var popup = context.OwnerWindow.CellPairNumberPopup;
		popup.Tag = context;
		popup.Closed += Popup_Closed;

		popup.IsOpen = true;
	}

	/// <inheritdoc/>
	protected internal override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == MouseButton.Right;

	private void Popup_Closed(object? sender, EventArgs e)
	{
		if (sender is not Popup
			{
				Tag: OperationHandlerContext
				{
					OwnerWindow:
					{
						CellPairNumberBox.Value: var value,
						CurrentCanvas.Mapper: var mapper
					} window
				} context
			} popup)
		{
			return;
		}

		var (cell1, cell2) = context.GetBorder();
		var cell1Row = cell1 / mapper.AbsoluteColumnsCount;
		var cell1Column = cell1 % mapper.AbsoluteColumnsCount;
		if (cell1 == -1 || cell2 == -1)
		{
			return;
		}

		var item = ItemsFactory.CellPairNumber(cell1, cell2, value);
		UpdateItems(
			window,
			items =>
			{
				if (item is null)
				{
					items.Clear(cell1, cell2, ItemType.CellPairText_Number);
				}
				else
				{
					items.Add(item);
				}
			}
		);

		// Clear context.
		popup.Closed -= Popup_Closed;
		popup.Tag = null;
	}
}

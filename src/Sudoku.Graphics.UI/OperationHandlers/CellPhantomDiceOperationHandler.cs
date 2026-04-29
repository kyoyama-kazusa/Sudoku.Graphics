namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellPhantomDiceMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellPhantomDiceMarkItem"/>
[OperationHandler(ItemType.Cell_PhantomDice)]
public sealed class CellPhantomDiceOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
	{
		var popup = context.OwnerWindow.PhantomDicePopup;
		popup.Tag = context;
		popup.Closed += Popup_Closed;

		popup.IsOpen = true;
	}

	/// <inheritdoc/>
	protected internal override bool IsAvailable(OperationHandlerContext context)
		=> context is
		{
			MouseEventArgs.ChangedButton: MouseButton.Right,
			OwnerWindow.CurrentCanvas.Templates: [{ Mapper: { RowsCount: var rowsCount, ColumnsCount: var columnsCount } }]
		}
		&& rowsCount == columnsCount;

	private void Popup_Closed(object? sender, EventArgs e)
	{
		if (sender is not Popup
			{
				Tag: OperationHandlerContext
				{
					OwnerWindow:
					{
						PhantomDiceSubgridSizeBox.Value: var subgridSize,
						PhantomDiceStatesMatrixBox.Text: var statesString,
						CurrentGrid.RowsCount: var gridSize
					} window
				} context
			} popup)
		{
			return;
		}

		// Check state string.
		var validSeparators = LocalizationResources._ValidSeparators;
		var states = new BitArray(subgridSize * subgridSize);
		for (var (i, index) = (0, 0); i < statesString.Length; i++)
		{
			var ch = statesString[i];
			if (validSeparators.Contains(ch))
			{
				// Skip for separators.
				continue;
			}

			var nullableState = ch switch { '1' => true, '0' => false, _ => (bool?)null };
			if (nullableState is not { } state)
			{
				// Invalid characters encountered.
				return;
			}

			// Set on-off state.
			states[index++] = state;
		}

		var cell = context.GetCell();
		var item = ItemsFactory.PhantomDice(cell, subgridSize, states);
		UpdateItems(
			window,
			items =>
			{
				if (item is null)
				{
					items.Clear(cell, ItemType.Cell_PhantomDice);
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

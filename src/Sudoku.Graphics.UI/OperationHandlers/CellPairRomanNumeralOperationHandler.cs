namespace Sudoku.Graphics.UI.OperationHandlers;

/// <summary>
/// Represents an operation handler that produces <see cref="CellPairRomanNumeralTextMarkItem"/> instances.
/// </summary>
/// <seealso cref="CellPairRomanNumeralTextMarkItem"/>
[OperationHandler(ItemType.CellPairText_RomanNumeral)]
public sealed class CellPairRomanNumeralOperationHandler : OperationHandler
{
	/// <inheritdoc/>
	protected internal override void OnMouseButtonPressed(OperationHandlerContext context)
	{
		var panel = context.OwnerWindow.CellPairRomanNumeralPanel;
		panel.OperationHandlerContext = context;
		panel.SelectedItemChanged += Panel_SelectedItemChanged;

		var values = new List<RomanNumeralDisplayItem>();
		values.AddRange(
			[
				new() { Value = 1 }, new() { Value = 4 }, new() { Value = 5 },
				new() { Value = 6 }, new() { Value = 9 }, new() { Value = 10 },
				new() { Value = 11 }, new() { Value = 14 }, new() { Value = 15 },
				new() { Value = 16 }, new() { Value = 19 }, new() { Value = 20 }
			]
		);
		panel.ItemsSource = values;
	}

	/// <inheritdoc/>
	protected internal override void OnMouseButtonReleased(OperationHandlerContext context)
		=> context.OwnerWindow.CellPairRomanNumeralPopup.IsOpen = true;

	/// <inheritdoc/>
	protected internal override bool IsAvailable(OperationHandlerContext context)
		=> context.MouseEventArgs.ChangedButton == MouseButton.Right;

	private void Panel_SelectedItemChanged(ItemSelectorPanel sender, ItemSelectorPanelSelectedItemChangedEventArgs e)
	{
		if (e.Context is not { OwnerWindow: { CurrentCanvas.Mapper: var mapper } window } context)
		{
			return;
		}

		var popup = window.CellPairRomanNumeralPopup;
		var panel = window.CellPairRomanNumeralPanel;
		var selectedItem = e.SelectedItem;

		popup.IsOpen = false;

		var (cell1, cell2) = context.GetBorder();
		var cell1Row = cell1 / mapper.AbsoluteColumnsCount;
		var cell1Column = cell1 % mapper.AbsoluteColumnsCount;
		if (cell1 == -1 || cell2 == -1)
		{
			return;
		}

		var item = ItemsFactory.CellPairRomanNumeral(cell1, cell2, ((RomanNumeralDisplayItem)selectedItem!).Value);
		UpdateItems(
			window,
			items =>
			{
				if (item is null)
				{
					items.Clear(cell1, cell2, ItemType.CellPairText_RomanNumeral);
					return;
				}

				var found = items.Find(cell1, cell2, ItemType.CellPairText_RomanNumeral);
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
